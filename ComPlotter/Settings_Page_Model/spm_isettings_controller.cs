using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter
{
    interface ISettingsController:IDisposable
    {
        ISerialController SerialController { get; }
    }
}
