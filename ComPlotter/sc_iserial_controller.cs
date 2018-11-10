using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ComPlotter
{
    interface ISerialController : IDisposable
    {
        void Connect();

        void Configure(
            string _name
        ,   string _baudrate
        ,   string _bits
        ,   string _parity
        );

        void RefreshState();

        void Disconnect();

        List<string> AvaliableSerials { get; }

        ConcurrentQueue<byte> SerialData { get; }
    }
}
