using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace ComPlotter.Settings_Page_Model.File_Writer
{
    class AdditionalWriter: AbstractFileWriter
    {
        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (decimal value in e.NewItems)
            {
                Console.WriteLine( value );
            }
        }
    }
}
