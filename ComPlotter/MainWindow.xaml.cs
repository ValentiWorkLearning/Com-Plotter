using ComPlotter.Factories;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SerialFacade = new SerialServicesProvider();
            controlsFactory = new UserControlsFactory();
            currentControl = new UserControl();
        }

        private void ButtonPopUpQuit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonPopUpAbout_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BodyArea.Children.Clear();

            controlsFactory.ArgumentsParser = new WindowTypesParser();

            currentControl = controlsFactory.CreateDialog(sender, SerialFacade);

            BodyArea.Children.Add(currentControl);

        }

        ISerialServicesFacade SerialFacade;
        UserControlsFactory controlsFactory;
        UserControl currentControl;
    }
}
