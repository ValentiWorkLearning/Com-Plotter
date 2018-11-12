using ComPlotter.Services_Provider_Model.File_Writer;
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

        IFilesManager FileManager{ get; }
    }
}
