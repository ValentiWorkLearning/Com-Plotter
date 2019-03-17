using ComPlotter.Factories;
using ComPlotter.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static ComPlotter.Factories.WindowTypesParser;

namespace ComPlotter
{
    class UserControlsFactory
    {

        public UserControl CreateDialog( object sender , ISerialServicesFacade _facade, IReceivedDataParser _parser)
        {
            switch ( ArgumentsParser.TryParseValue(sender ) )
            {
                case ChildWindowType.Settings:
                    return new SettingsUserControl( _facade );

                case ChildWindowType.ChartPage:
                    return new ComPlotControl( _facade );

                case ChildWindowType.AboutPage:
                    return new AboutControl();

                case ChildWindowType.TemperaturePressure:
                    return new PressureTempPlotter( _facade , _parser );

                default:
                    throw new InvalidOperationException();
            }
        }

        public IArgumentsParser ArgumentsParser { get; set; }
    }
}
