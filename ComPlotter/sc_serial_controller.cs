using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace ComPlotter
{
    class SerialController : ISerialController
    {
        public SerialController()
        {
            m_serial = new SerialPort
            {
                ReadTimeout = 500,
                WriteTimeout = 500
            };

            m_readerThread = new Thread(Read);
            m_mutex = new Mutex();
            m_isFirstLaunch = true;
        }

        public void Connect()
        {
            if (!m_serial.IsOpen)
            {
                m_serial.Open();

                if (m_isFirstLaunch)
                {
                    m_readerThread.Start();
                    m_isFirstLaunch = false;
                }
                else
                {
                    m_mutex.ReleaseMutex();
                }
            }
        }

        public void Disconnect()
        {
            if ( m_serial.IsOpen )
            {
                m_mutex.WaitOne();
                m_serial.Close();
            }
        }

        public void RefreshState()
        {
            if ( m_serial.IsOpen )
                this.Disconnect();

            this.Connect();
        }

        public void SetBaudrate(string _baudrate)
        {
            m_serial.BaudRate = int.Parse(_baudrate);
        }

        public void SetName(string _name)
        {
            m_serial.PortName = _name;
        }

        public void SetParity(string _parity)
        {
            m_serial.Parity = (Parity)Enum.Parse(typeof(Parity), _parity, true);
        }

        public void SetStopBits(string _bits)
        {
            m_serial.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _bits, true);
        }

        private void Read()
        {
            while (true)
            {
                try
                {
                    m_mutex.WaitOne();

                    string message = m_serial.ReadLine();

                    m_mutex.ReleaseMutex();

                    Console.WriteLine(message);
                }
                catch (System.TimeoutException)
                {
                    m_mutex.ReleaseMutex();
                }
            }
        }

        void IDisposable.Dispose()
        {
            m_mutex.WaitOne();
            m_readerThread.Abort();
            m_serial.Close();
            GC.SuppressFinalize(this);
        }

        Thread m_readerThread;

        Mutex m_mutex;

        bool m_isFirstLaunch;

        SerialPort m_serial;

    }
}
