using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter
{
    public interface ISerialServicesFacade : IDisposable
    {
        ISerialController SerialController { get; }
    }
}
