using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComPlotter.Factories
{
    class WindowTypesParser: IArgumentsParser
    {
        public Enum TryParseValue(object _value)
        {
            switch (((ListViewItem)((ListView)_value).SelectedItem).Name )
            {
                case "ItemSettings":
                    return ChildWindowType.Settings;

                case "ItemChart":
                    return ChildWindowType.ChartPage;

                case "ItemGeneral":
                    return ChildWindowType.AboutPage;

                default:
                    throw new InvalidOperationException();
            }
        }

        public enum ChildWindowType
        {
                Settings
           ,    ChartPage
           ,    AboutPage
        }
    }
}
