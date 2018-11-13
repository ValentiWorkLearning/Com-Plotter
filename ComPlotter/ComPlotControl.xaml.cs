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

            SerialServices = _serialServicesFacade;

            InitChartView();
            ResetChartValues();

            SerialServices.SerialController.SerialData.CollectionChanged += CollectionChanged;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            FillChartWithValue(1);
        }

        public void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            byte toAdd = ((byte)e.NewItems[0]);

            int index = m_intervalsProvider.GetIntervalIndexOfValue( toAdd );

            m_chartValues[index]++;
        }

        private void BarList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem _item = BarList.SelectedItem as ComboBoxItem;

            Properties.Settings.Default.BarsNumber = BarList.SelectedIndex;

            string tempString = _item.Content.ToString();

            BarsNumber = Int32.Parse(tempString);

            SerialServices.SerialController.SerialData.CollectionChanged -= CollectionChanged;

            ResetChartValues();

            SerialServices.SerialController.SerialData.CollectionChanged += CollectionChanged;
        }

        void ResetChartValues()
        {
            m_chartValues.Clear();

            for (int i = 0; i < BarsNumber; i++)
            {
                m_chartValues.Add(1);
            }

            m_intervalsProvider = new DensityDistribution(m_chartValues.Count);
        }

        void FillChartWithValue(int _value)
        {
            for (int i = 0; i < m_chartValues.Count; i++)
            {
                m_chartValues[i] = _value;
            }
        }

        void InitChartView()
        {
            DataContext = this;

            int barIndex = Properties.Settings.Default.BarsNumber;
            var barsValue = BarList.Items[barIndex] as ComboBoxItem;
            var stringToParse = barsValue.Content.ToString();
            BarsNumber = Int32.Parse(stringToParse);

            if (BarsNumber == 0)
            {
                BarsNumber = 8;
            }

            m_chartValues = new ChartValues<int>();

            DisplayChart.AnimationsSpeed = TimeSpan.FromMilliseconds(100);
            DisplayChart.DataTooltip = null;

            BarList.SelectedIndex = Properties.Settings.Default.BarsNumber;
        }

        public ChartValues<int> m_chartValues { get; set; }

        DensityDistribution m_intervalsProvider;

        ISerialServicesFacade SerialServices;

        int BarsNumber;
    }
}
