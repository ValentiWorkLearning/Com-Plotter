using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter.Services_Provider_Model.File_Writer
{
    public interface IFilesManager : IEnumerable , IEnumerator, IDisposable
    {
        AbstractFileWriter AllDataFileStream { get; }

        AbstractFileWriter AdditionalFileStream { get; }

    }
}

