using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter
{
    interface ISerialController : IDisposable
    {
        void Connect();

        void SetName( string _name );

        void SetBaudrate(string _baudrate);

        void SetStopBits(string _bits);

        void SetParity(string _parity);

        void RefreshState();

        void Disconnect();
    }
}
