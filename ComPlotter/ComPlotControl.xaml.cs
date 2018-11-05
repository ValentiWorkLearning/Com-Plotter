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

namespace ComPlotter
{
    /// <summary>
    /// Interaction logic for ComPlotControl.xaml
    /// </summary>
    public partial class ComPlotControl : UserControl
    {
        public ComPlotControl()
        {
            InitializeComponent();
            m_timer = new DispatcherTimer();
            m_timer.Tick += new EventHandler(timer_Tick);
            m_timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            m_timer.Start();

            m_chartValues = new ChartValues<decimal> { 5, 6, 2, 7, 20, 3, 5, 3, 4 ,5,6,5,64,5,4,4,5,3,2,1,3,4,5,2,4,1,1,43,5,5,4,7,5,3,2,3,4,2,1,23,5,5,5,44,};

            m_columnSeries = new ColumnSeries { Values = m_chartValues };

            m_series = new SeriesCollection { m_columnSeries };

            m_series.CollectionChanged += M_series_CollectionChanged;
            DisplayChart.AnimationsSpeed = TimeSpan.FromMilliseconds(400);

            DisplayChart.Series = m_series;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            m_chartValues[0] += 5;
            m_chartValues[1] += 2;
            m_chartValues[3] += 4;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            m_chartValues[0] = 4;
            m_chartValues[1] = 4;
            m_chartValues[3] = 4;
        }

        private void M_series_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // throw new NotImplementedException();

            Console.WriteLine("Hello");
        }

        ChartValues<decimal> m_chartValues;

        ColumnSeries m_columnSeries;

        DispatcherTimer m_timer;

        SeriesCollection m_series;
    }
}
