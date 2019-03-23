using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter.Parsers
{
    public class ReceivedDataParser
        : IReceivedDataParser
    {
        public float getPressure()
        {
                return Pressure;
        }

        public float getTemperature()
        {
            return Temperature;
        }

        public float getHumidity()
        {
            return Humidity;
        }

        public void tryParseString( string _toParse )
        {

            if (
                    ( _toParse.Contains( PressureBegin ) && _toParse.Contains( PressureEnd ) )
                &&  ( _toParse.Contains( TempBegin ) && _toParse.Contains( TempEnd ) )
                &&  ( _toParse.Contains( HumidityBegin ) && _toParse.Contains( HumidityEnd ) )
            )
            {
                try
                {
                    var pressureIndexes = getKeyIndexes( _toParse, PressureBegin, PressureEnd );
                    Pressure = getFloatFromRange( _toParse, pressureIndexes.Item1 , pressureIndexes.Item2 );

                    var temperatureIndexes = getKeyIndexes( _toParse, TempBegin, TempEnd );
                    Temperature = getFloatFromRange( _toParse , temperatureIndexes.Item1, temperatureIndexes.Item2 );

                    var humidityIndexes = getKeyIndexes( _toParse, HumidityBegin, HumidityEnd );
                    Humidity = getFloatFromRange( _toParse, humidityIndexes.Item1, humidityIndexes.Item2 );
                }
                catch( Exception _ex )
                {
                    throw new InvalidOperationException();
                }
            }
        }

        ( int,int ) getKeyIndexes( string _dataSource, string _searchKeyBegin, string _searchKeyEnd )
        {
            int keyBegin = _dataSource.IndexOf(_searchKeyBegin, 0) + _searchKeyBegin.Length;
            int keyEnd = _dataSource.IndexOf( _searchKeyEnd, 0 );

            return ( keyBegin, keyEnd );
        }

        float getFloatFromRange( string _dataSource, int keyBegin, int keyEnd )
        {
            return float.Parse(
                    _dataSource.Substring( keyBegin, keyEnd - keyBegin )
                ,   System.Globalization.CultureInfo.InvariantCulture
            );
        }

        const string PressureBegin = "PRESSURE_BEGIN";
        const string PressureEnd = "PRESSURE_END";
        const string TempBegin = "TEMP_BEGIN";
        const string TempEnd = "TEMP_END";
        const string HumidityBegin = "HUMIDITY_BEGIN";
        const string HumidityEnd = "HUMIDITY_END";

        float Pressure;
        float Temperature;
        float Humidity;
    }
}
