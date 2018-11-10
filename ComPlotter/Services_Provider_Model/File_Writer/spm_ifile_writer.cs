using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;

namespace ComPlotter
{
    abstract class AbstractFileWriter : IDisposable
    {
        public void SetFileToWrite(string _pathName)
        {
            if (File.Exists(_pathName))
            {
                File.Delete(_pathName);
            }
            m_fileStream = File.CreateText(_pathName);
        }

        public virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new InvalidOperationException();
        }

        public void Dispose()
        {
            m_fileStream.Dispose();
        }

        protected StreamWriter m_fileStream;
    }
}
