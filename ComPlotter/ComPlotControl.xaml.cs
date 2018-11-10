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
            m_timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            m_timer.Start();

            m_chartValues = new ChartValues<decimal> { 1,2,3,4,5,6,7,8,9,10 };

            DataContext = this;

            DisplayChart.AnimationsSpeed = TimeSpan.FromMilliseconds(100);

            DisplayChart.DataTooltip = null;

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            m_chartValues.Add(random.Next(0,10));
            if (m_chartValues.Count > 100)
            {
                m_chartValues.RemoveAt(0);

            }
         }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            m_chartValues[0] = 4;
            m_chartValues[1] = 4;
            m_chartValues[3] = 4;
        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Hello");
        }

        public ChartValues<decimal> m_chartValues { get; set; }

        DispatcherTimer m_timer;

    }
}
