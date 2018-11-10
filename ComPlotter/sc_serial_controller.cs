using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Collections.Concurrent;

namespace ComPlotter
{
    class SerialController : ISerialController
    {
        public SerialController()
        {
            m_serialPort = new SerialPort
            {
                ReadTimeout = 500,
                WriteTimeout = 500
            };

            m_readerThread = new Thread(ReadTask);
            m_threadGuard = new Mutex();
            SerialData = new ConcurrentQueue<byte>();
            m_isFirstThreadLaunch = true;
        }

        public void Connect()
        {
            Console.WriteLine(m_serialPort.BaudRate);
            Console.WriteLine(m_serialPort.DataBits);
            Console.WriteLine(m_serialPort.StopBits);
            Console.WriteLine(m_serialPort.Parity);

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
                catch (Exception _e)
                {
                    throw new InvalidOperationException();
                }
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
            m_serialPort.PortName = _name;

            m_serialPort.BaudRate = int.Parse(_baudrate);
            m_serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), _parity, true);
            m_serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _bits, true);
        }

        private void ReadTask()
        {
            while ( true )
            {
                try
                {
                    m_threadGuard.WaitOne();

                    Random testRand = new Random();

                    SerialData.Enqueue( (byte) testRand.Next( 0 , 255 ) );

                    byte tempByte = (byte)m_serialPort.ReadByte();

                    SerialData.Enqueue(tempByte);

                    //Console.WriteLine(m_serialPort.ReadLine());

                    m_threadGuard.ReleaseMutex();

                }
                catch ( System.TimeoutException )
                {
                    m_threadGuard.ReleaseMutex();
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

        public ConcurrentQueue<byte> SerialData { get; }

        public List<string> AvaliableSerials {
            get {
                return SerialPort.GetPortNames().ToList();
            }
        }

        bool m_isFirstThreadLaunch;

        Thread m_readerThread;

        Mutex m_threadGuard;

        SerialPort m_serialPort;

    }
}
