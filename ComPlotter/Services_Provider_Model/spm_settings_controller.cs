using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ComPlotter.Services_Provider_Model.File_Writer;

namespace ComPlotter
{
    class SerialServicesProvider: ISerialServicesFacade
    {
        public SerialServicesProvider()
        {
            SerialController = new SerialController();

            Application.Current.Exit += new ExitEventHandler(Application_ApplicationExit);

            FileManager = new FilesManager();

            foreach (AbstractFileWriter file in FileManager)
            {
                SerialController.SerialData.CollectionChanged += file.CollectionChanged;
            }
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Dispose()
        {
            SerialController.Dispose();
            FileManager.Dispose();
        }

        public ISerialController SerialController { get; }
        public IFilesManager FileManager { get; }
    }
}
