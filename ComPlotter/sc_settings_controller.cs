using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter
{
    class SettingsController : ISettingsController
    {
        public SettingsController()
        {
            m_serialController = new SerialController();
        }

        public void ConfigureSerial(string _serialName, string _baudrate, string _stopBits, string _parity)
        {
            m_serialController.SetName(_serialName);
            m_serialController.SetStopBits(_stopBits);
            m_serialController.SetBaudrate(_baudrate);
            m_serialController.SetParity(_parity);
        }

        public void ConnectToSerial()
        {
            m_serialController.Connect();
        }

        public void DisconnectFromSerial()
        {
            m_serialController.Disconnect();
        }

        public void RefreshSerialState()
        {
            m_serialController.RefreshState();
        }

        public void Dispose()
        {
            m_serialController.Dispose();
        }

        ISerialController m_serialController;
    }
}
