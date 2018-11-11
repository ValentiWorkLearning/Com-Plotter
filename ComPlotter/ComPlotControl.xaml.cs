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

using LiveCharts;
using LiveCharts.Wpf;

using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ComPlotter.Algorithms;

namespace ComPlotter
{
    /// <summary>
    /// Interaction logic for ComPlotControl.xaml
    /// </summary>
    public partial class ComPlotControl : UserControl
    {
        public ComPlotControl(ISerialServicesFacade _serialServicesFacade )
        {
            InitializeComponent();

            m_chartValues = new ChartValues<int> { 1, 1, 1, 1, 1,1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            DataContext = this;

            DisplayChart.AnimationsSpeed = TimeSpan.FromMilliseconds(100);

            DisplayChart.DataTooltip = null;

            SerialServices = _serialServicesFacade;

            SerialServices.SerialController.SerialData.CollectionChanged += CollectionChanged;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < m_chartValues.Count; i++)
            {
                m_chartValues[i] = 0;
            }
        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DensityDistribution density = new DensityDistribution( m_chartValues.Count );

            byte toAdd = ((byte)e.NewItems[0]);

            int index = density.getIntervalIndexOfValue( toAdd );

            m_chartValues[index]++;
        }

        public ChartValues<int> m_chartValues { get; set; }

        ISerialServicesFacade SerialServices;
    }
}
