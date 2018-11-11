using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace ComPlotter
{
    class AllReceivedDataWriter : AbstractFileWriter
    {
        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //m_fileStream.WriteLineAsync((char)e.NewItems[0]);
           // Console.WriteLine( "All data writer ");
        }
    }
}
