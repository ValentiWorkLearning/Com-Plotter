using Microsoft.Win32;
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
        public SettingsUserControl( ISerialServicesFacade _serialServicesProvider)
        {
            InitializeComponent();

            SerialServices = _serialServicesProvider;

            RestoreInternal();
        }

        private void Button_ApplyClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SerialServices.SerialController.Configure(
                        SerialName
                    ,   SerialBaudrate
                    ,   SerialStopBits
                    ,   SerialParity
                );
            }
            catch (InvalidOperationException)
            {
                    MessageBox.Show(
                        Properties.Resources.Error_FailureWithOpenSerialConnection
                    ,   Properties.Resources.Error_Caption_FailureWithOpenSerialConnection
                    ,   MessageBoxButton.OK
                    ,   MessageBoxImage.Error
                  );
            }
        }

        private void Button_ConnectClick(object sender, RoutedEventArgs e)
        {
            try
            {
                SerialServices.SerialController.Connect();
            }
            catch ( InvalidOperationException )
            {
                MessageBox.Show(
                        Properties.Resources.Error_FailureWithOpenSerialConnection
                    ,   Properties.Resources.Error_Caption_FailureWithOpenSerialConnection
                    ,   MessageBoxButton.OK
                    ,   MessageBoxImage.Error
                );
            }
        }

        private void Button_DisconnectClick(object sender, RoutedEventArgs e)
        {
            SerialServices.SerialController.Disconnect();
        }

        private void Button_RefreshClick(object sender, RoutedEventArgs e)
        {
            SerialServices.SerialController.RefreshState();
        }

        private void ComName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SerialName = ComName.SelectedItem.ToString();

            Properties.Settings.Default.SelectedSerial = SerialName;
        }

        private void BaudrateList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConvertSelectionToStringProperty(BaudrateList, ref SerialBaudrate);

            Properties.Settings.Default.SerialBaudRate = BaudrateList.SelectedIndex;
        }

        private void StopBitsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConvertSelectionToStringProperty(StopBitsList , ref SerialStopBits);

            Properties.Settings.Default.SerialStopBits = StopBitsList.SelectedIndex;
        }

        private void ParityList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConvertSelectionToStringProperty( ParityList , ref SerialParity );

            Properties.Settings.Default.SerialParity = ParityList.SelectedIndex;
        }

        void ConvertSelectionToStringProperty(ComboBox _selectionList , ref string _property)
        {
            ComboBoxItem _item = _selectionList.SelectedItem as ComboBoxItem;
            _property = _item.Content.ToString();
        }

        void RestoreInternal()
        {
     
            // restore internal properties
            SerialBaudrate  = BaudrateList.Items[ Properties.Settings.Default.SerialBaudRate ].ToString();
            SerialStopBits  =   StopBitsList.Items[ Properties.Settings.Default.SerialStopBits ].ToString();
            SerialParity    =   ParityList.Items[ Properties.Settings.Default.SerialParity ].ToString();

            // restore view indexes
            BaudrateList.SelectedIndex  =   Properties.Settings.Default.SerialBaudRate;
            StopBitsList.SelectedIndex  =   Properties.Settings.Default.SerialStopBits;
            ParityList.SelectedIndex    =   Properties.Settings.Default.SerialParity;

            // restore current serial property & view
            ComName.Items.Clear();
            foreach (string serialName in SerialServices.SerialController.AvaliableSerials)
            {
                ComName.Items.Add(serialName);
            }

            int selectedIndex = ComName.Items.IndexOf(Properties.Settings.Default.SelectedSerial);

            if (selectedIndex != -1)
            {
                ComName.SelectedIndex = selectedIndex;
                SerialName = ComName.Items[selectedIndex].ToString();
            }
            else
            {
                SerialName = null;
            }

        }

        private void Button_SelectAllStreamFileClick(object sender, RoutedEventArgs e)
        {
            SetFileNameToWrite( SerialServices.FileManager.AllDataFileStream );
        }

        private void Button_SelectSpecialStreamFileClick(object sender, RoutedEventArgs e)
        {
            SetFileNameToWrite(SerialServices.FileManager.AdditionalFileStream);
        }

        void SetFileNameToWrite( AbstractFileWriter _writer )
        {
            OpenFileDialog Dialog = new OpenFileDialog
            {
                    Filter = Properties.Resources.FilesExtensionsFilter
                ,   CheckFileExists = true
                ,   Multiselect = true
            };

            if (Dialog.ShowDialog() == true)
            {
                _writer.SetFileToWrite(Dialog.FileName);
            }
        }

        private void Button_SelectReceivedBlockSizeClick(object sender, RoutedEventArgs e)
        {
            SerialServices.FileManager.AdditionalFileStream.SetReceivedBlockSize(ReceivedBlockSize.Value.Value);
        }

        string SerialName;
        string SerialBaudrate;
        string SerialStopBits;
        string SerialParity;

        ISerialServicesFacade SerialServices;

    }
}
