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

            m_fileWriters = new List<AbstractFileWriter> { new AdditionalWriter(), new AllReceivedDataWriter() };

            foreach (var writer in m_fileWriters)
            {
                m_serialController.SerialData.CollectionChanged += writer.CollectionChanged;
            }
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

        public List<AbstractFileWriter> FileWriters { get { return m_fileWriters; } }

        ISerialController m_serialController;
        List<AbstractFileWriter> m_fileWriters;
    }
}
