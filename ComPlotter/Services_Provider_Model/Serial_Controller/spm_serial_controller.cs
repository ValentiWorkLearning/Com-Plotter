using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;

namespace ComPlotter
{
    class SerialController : ISerialController
    {
        public SerialController()
        {
            m_serialPort = new SerialPort
            {
                ReadTimeout = 1000,
                WriteTimeout = 1000
            };

            m_readerThread = new Thread( () => ReadTaskBytestream( ref m_isConfigured ) );
            m_threadGuard = new Mutex();
            SerialData = new ObservableCollection<byte>();
            SerialDataString = new ObservableCollection<string>();
            m_isFirstThreadLaunch = true;
            m_isConfigured = false;
        }

        public void Connect()
        {
            if ( m_isConfigured )
            {
                Console.WriteLine( m_serialPort.PortName );
                Console.WriteLine( m_serialPort.BaudRate );
                Console.WriteLine( m_serialPort.DataBits );
                Console.WriteLine( m_serialPort.StopBits );
                Console.WriteLine( m_serialPort.Parity );

                if ( !m_serialPort.IsOpen )
                {
                    try
                    {
                        m_serialPort.Open();

                        if ( m_isFirstThreadLaunch )
                        {
                            m_readerThread.Start();
                            m_isFirstThreadLaunch = false;
                        }
                        else
                        {
                            m_threadGuard.ReleaseMutex();
                        }
                    }
                    catch (Exception)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Disconnect()
        {
            if ( m_serialPort.IsOpen )
            {
                m_threadGuard.WaitOne();
                m_serialPort.Close();
            }
        }

        public void RefreshState()
        {
            if ( m_serialPort.IsOpen )
                this.Disconnect();

            this.Connect();
        }

        public void Configure(
                string _name
            ,   string _baudrate
            ,   string _bits
            ,   string _parity
            )
        {
            if(     string.IsNullOrEmpty( _name )
                ||  string.IsNullOrEmpty( _baudrate )
                ||  string.IsNullOrEmpty( _bits )
                ||  string.IsNullOrEmpty(_parity)
              )
                throw new InvalidOperationException();

            m_serialPort.PortName = _name;

            m_serialPort.BaudRate = int.Parse(_baudrate);
            m_serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), _parity, true);
            m_serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _bits, true);
            m_isConfigured = true;
        }

        private void ReadTaskBytestream( ref bool _isSerialValid )
        {
            while ( true )
            {
                try
                {
                    m_threadGuard.WaitOne();

                    string receivedString = m_serialPort.ReadLine();
                    foreach( char c in receivedString)
                    {
                        byte tempByte = (byte)c;
                        SerialData.Add( tempByte );
                        Console.WriteLine( tempByte );
                    }
                    m_threadGuard.ReleaseMutex();

                }
                catch ( System.TimeoutException )
                {
                    m_threadGuard.ReleaseMutex();
                }
                catch (System.IO.IOException _ex)
                {
                    m_serialPort.Close();

                    Console.WriteLine("Serial may be disconnected");
                    _isSerialValid = false;

                    m_threadGuard.ReleaseMutex();
                    Thread.CurrentThread.Abort();
                }
            }
        }

        private void ReadTaskReadline( ref bool _isSerialValid )
        {
            while (true)
            {
                try
                {
                    m_threadGuard.WaitOne();

                    string toAdd = m_serialPort.ReadLine();

                    SerialDataString.Add(toAdd);

                    Console.WriteLine(toAdd);
                    m_threadGuard.ReleaseMutex();
                }
                catch (System.TimeoutException _ex)
                {
                    m_threadGuard.ReleaseMutex();
                }
                catch (System.IO.IOException _ex)
                {
                    m_serialPort.Close();

                    Console.WriteLine("Serial may be disconnected");

                    m_isConfigured = false;
                    m_threadGuard.ReleaseMutex();
                    Thread.CurrentThread.Abort();
                }
            }
        }
        void IDisposable.Dispose()
        {
            m_threadGuard.WaitOne();
            m_readerThread.Abort();
            m_serialPort.Close();

            GC.SuppressFinalize(this);
        }

        public void SetReceivingPolicy( ReceivingPolicy _policy )
        {
            if (_policy != policy)
            {
                if ( this.m_isConfigured )
                {
                    policy = _policy;

                    m_threadGuard.WaitOne();
                    m_serialPort.Close();

                    m_readerThread.Abort();

                    if ( policy == ReceivingPolicy.ByteStream )
                    {
                        m_readerThread = new Thread( () => ReadTaskBytestream( ref m_isConfigured ) );
                    }
                    else if ( policy == ReceivingPolicy.StringToEndline )
                    {
                        m_readerThread = new Thread( () => ReadTaskReadline( ref m_isConfigured ) );
                    }

                    this.m_isFirstThreadLaunch = true;
                    this.Connect();

                    m_threadGuard.ReleaseMutex();
                }
            }
        }

        public ObservableCollection<byte> SerialData { get; }

        public List<string> AvaliableSerials {
            get {
                return SerialPort.GetPortNames().ToList();
            }
        }

        public ObservableCollection<string> SerialDataString { get; }

        bool m_isFirstThreadLaunch;
        bool m_isConfigured;

        ReceivingPolicy policy;

        Thread m_readerThread;
        Mutex m_threadGuard;
        SerialPort m_serialPort;

    }
}
