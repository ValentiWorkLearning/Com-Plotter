using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter
{
    interface ISettingsController:IDisposable
    {
        void ConfigureSerial(
                string _serialName
            ,   string _baudrate
            ,   string _stopBits
            ,   string _parity
        );

        void ConnectToSerial();

        void RefreshSerialState();

        void DisconnectFromSerial();
    }
}
