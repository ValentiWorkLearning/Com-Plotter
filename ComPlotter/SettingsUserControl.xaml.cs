using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComPlotter
{
    /// <summary>
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
        }

        private static ISettingsController m_settingController = new SettingsController();

        private void Button_ApplyClick(object sender, RoutedEventArgs e)
        {
            m_settingController.ConfigureSerial(
                    SerialName
                ,   SerialBaudrate
                ,   SerialStopBits
                ,   SerialParity
            );
        }

        private void Button_ConnectClick(object sender, RoutedEventArgs e)
        {
            m_settingController.ConnectToSerial();
        }

        private void Button_DisconnectClick(object sender, RoutedEventArgs e)
        {
            m_settingController.DisconnectFromSerial();
        }

        private void Button_RefreshClick(object sender, RoutedEventArgs e)
        {
            m_settingController.RefreshSerialState();
        }

        public string SerialName { get; set; }
        public string SerialBaudrate { get; set; }
        public string SerialStopBits { get; set; }
        public string SerialParity { get; set; }
    }
}
