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
            m_data = new ConcurrentQueue<byte>();
            m_isFirstThreadLaunch = true;
        }

        public void Connect()
        {
            if (!m_serialPort.IsOpen)
            {
                m_serialPort.Open();

                if (m_isFirstThreadLaunch)
                {
                    m_readerThread.Start();
                    m_isFirstThreadLaunch = false;
                }
                else
                {
                    m_threadGuard.ReleaseMutex();
                }
            }
        }

        public void Disconnect()
        {
            if (m_serialPort.IsOpen )
            {
                m_threadGuard.WaitOne();
                m_serialPort.Close();
            }
        }

        public void RefreshState()
        {
            if (m_serialPort.IsOpen )
                this.Disconnect();

            this.Connect();
        }

        public void SetBaudrate(string _baudrate)
        {
            m_serialPort.BaudRate = int.Parse(_baudrate);
        }

        public void SetName(string _name)
        {
            m_serialPort.PortName = _name;
        }

        public void SetParity(string _parity)
        {
            m_serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), _parity, true);
        }

        public void SetStopBits(string _bits)
        {
            m_serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), _bits, true);
        }

        private void ReadTask()
        {
            while (true)
            {
                try
                {
                    m_threadGuard.WaitOne();

                    m_data.Enqueue( (byte)m_serialPort.ReadByte() );

                    m_threadGuard.ReleaseMutex();

                }
                catch (System.TimeoutException)
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

        ConcurrentQueue<byte> m_data;

        bool m_isFirstThreadLaunch;

        Thread m_readerThread;

        Mutex m_threadGuard;

        SerialPort m_serialPort;

    }
}
