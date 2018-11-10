using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ComPlotter
{
    class SettingsController : ISettingsController
    {
        public SettingsController()
        {
            m_serialController = new SerialController();
            Application.Current.Exit += new ExitEventHandler(Application_ApplicationExit);
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void ConfigureSerial(string _serialName, string _baudrate, string _stopBits, string _parity)
        {
            m_serialController.Configure( _serialName, _baudrate, _stopBits, _parity );
        }

        public void ConnectToSerial()
        {
            try
            {
                m_serialController.Connect();
            }
            catch (InvalidOperationException _e)
            {
                MessageBox.Show("Error occured when trying to open Serial", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DisconnectFromSerial()
        {
            m_serialController.Disconnect();
        }

        public void RefreshSerialState()
        {
            m_serialController.RefreshState();
        }

        public void Dispose()
        {
            m_serialController.Dispose();
        }

        private static void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    Console.WriteLine("Добавлен новый объект: {0}", e.NewItems[0]);
                    break;
            }
        }

        public ISerialController SerialController { get { return m_serialController; } }

        ISerialController m_serialController;
    }
}
