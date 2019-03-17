using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Controls;
using ComPlotter.Parsers;
using LiveCharts;

namespace ComPlotter
{
    /// <summary>
    /// Interaction logic for PressureTempPlotter.xaml
    /// </summary>
    /// 
    public class MeasureModel
    {
        public DateTime DateTime { get; set; }
        public float Value { get; set; }
    }

    public partial class PressureTempPlotter : UserControl
    {
        public PressureTempPlotter( ISerialServicesFacade _serialServicesFacade , IReceivedDataParser _parser )
        {
            InitializeComponent();
            InitChartView();

            SerialServices = _serialServicesFacade;
            DataParser = _parser;

            SerialServices.SerialController.SerialDataString.CollectionChanged += CollectionChanged;
            SerialServices.SerialController.SetReceivingPolicy( ReceivingPolicy.StringToEndline );
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            string toProcess = ( ( string )e.NewItems[ 0 ] );

            DataParser.tryParseString( toProcess );

            float pressure = DataParser.getPressure();
            float temp = DataParser.getTemperature();

            ChartValues.Add( temp );
            if (ChartValues.Count > 40)
                ChartValues.Clear();

        }

        private void InitChartView()
        {
            DataContext = this;

            ChartValues = new ChartValues<float>();
            DisplayChart.AnimationsSpeed = TimeSpan.FromMilliseconds( 100 );
            DisplayChart.DataTooltip = null;
        }

        public ChartValues< float > ChartValues { get; set; }

        ISerialServicesFacade SerialServices;
        IReceivedDataParser DataParser;
    }
}
