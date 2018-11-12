using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter.Services_Provider_Model.File_Writer
{
    class FilesManager: IFilesManager
    {
        public FilesManager()
        {
            AllDataFileStream = new AllReceivedDataWriter();
            AdditionalFileStream = new AdditionalWriter();

            this.Reset();
        }

        public object Current { get { return itCurrentWriter;  } }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (itCurrentWriter == null)
            {
                itCurrentWriter = AllDataFileStream;
                return true;
            }
            if (itCurrentWriter == AllDataFileStream)
            {
                itCurrentWriter = AdditionalFileStream;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            itCurrentWriter = null;
        }

        public void Dispose()
        {
            AllDataFileStream.Dispose();
            AdditionalFileStream.Dispose();
        }

        public AbstractFileWriter AllDataFileStream { get; }

        public AbstractFileWriter AdditionalFileStream { get; }

        AbstractFileWriter itCurrentWriter;
    }
}
