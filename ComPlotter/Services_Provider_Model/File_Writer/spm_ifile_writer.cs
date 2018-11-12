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
    abstract public class AbstractFileWriter : IDisposable
    {
        public void SetFileToWrite(string _pathName)
        {
            m_fileStream = File.CreateText(_pathName);
        }

        public virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new InvalidOperationException();
        }

        public void Dispose()
        {
            if (m_fileStream != null)
            {
                m_fileStream.Close();
                m_fileStream.Dispose();
            }
        }

        protected StreamWriter m_fileStream;
    }
}
