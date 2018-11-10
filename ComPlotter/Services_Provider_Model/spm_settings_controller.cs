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
    class SerialServicesProvider: ISerialServicesFacade
    {
        public SerialServicesProvider()
        {
            m_serialController = new SerialController();

            Application.Current.Exit += new ExitEventHandler(Application_ApplicationExit);
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Dispose()
        {
            m_serialController.Dispose();
        }

        public ISerialController SerialController { get { return m_serialController; } }

        ISerialController m_serialController;
    }
}
