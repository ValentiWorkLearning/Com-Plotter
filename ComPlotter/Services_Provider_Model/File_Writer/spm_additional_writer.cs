using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace ComPlotter
{
    class AdditionalWriter: AbstractFileWriter
    {
        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
           // Console.WriteLine("Special writer");
        }
    }
}
