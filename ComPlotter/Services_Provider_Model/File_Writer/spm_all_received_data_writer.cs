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
            if( m_fileStream!=null )
                m_fileStream.WriteLine( ((byte)e.NewItems[0]).ToString() );
        }
    }
}
