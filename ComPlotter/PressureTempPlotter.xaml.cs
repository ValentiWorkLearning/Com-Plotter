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
            float humidity = DataParser.getHumidity();

            UpdateCollectionData( TemperatureValues , temp );
            UpdateCollectionData( PressureValues, pressure );
            UpdateCollectionData( HumidityValues, humidity );
        }

        private void InitChartView()
        {
            DataContext = this;

            TemperatureValues = new ChartValues<float>();
            DisplayChartPressure.AnimationsSpeed = TimeSpan.FromMilliseconds( 150 );

            PressureValues = new ChartValues<float>();
            DisplayChartTemperature.AnimationsSpeed = TimeSpan.FromMilliseconds( 150 );

            HumidityValues = new ChartValues<float>();
            DisplayChartHumidity.AnimationsSpeed = TimeSpan.FromMilliseconds( 150 );
        }

        private void UpdateCollectionData( ChartValues<float> _chartValues , float _receivedValue )
        {
            _chartValues.Add( _receivedValue );
            if ( _chartValues.Count > 20 )
            {
                _chartValues.RemoveAt( 0 );
            }
        }
   
        public ChartValues< float > TemperatureValues { get; set; }
        public ChartValues<float> PressureValues { get; set; }
        public ChartValues<float> HumidityValues { get; set; }

        ISerialServicesFacade SerialServices;
        IReceivedDataParser DataParser;
    }
}
