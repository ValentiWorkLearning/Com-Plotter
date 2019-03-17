using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter.Parsers
{
    class ReceivedDataParser
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

        public void tryParseString( string _toParse )
        {
            int TempIndexBegin;
            int TempIndexEnd;
            int PressureIndexBegin;
            int PressureIndexEnd;

            if (
                    _toParse.Contains( PressureBegin ) && _toParse.Contains( PressureEnd )
                &&  _toParse.Contains( TempBegin ) && _toParse.Contains( TempEnd ) )
            {
                PressureIndexBegin = _toParse.IndexOf( PressureBegin, 0 ) + PressureBegin.Length;
                PressureIndexEnd = _toParse.IndexOf( PressureEnd, 0 );

                Pressure = float.Parse(
                        _toParse.Substring( PressureIndexBegin, PressureIndexEnd- PressureIndexBegin )
                    ,   System.Globalization.CultureInfo.InvariantCulture
                );

                TempIndexBegin = _toParse.IndexOf( TempBegin , 0 ) + TempBegin.Length;
                TempIndexEnd = _toParse.IndexOf( TempEnd, 0 );

                Temperature = float.Parse(
                        _toParse.Substring( TempIndexBegin, TempIndexEnd - TempIndexBegin)
                    ,   System.Globalization.CultureInfo.InvariantCulture
                );

            }
        }

        const string PressureBegin = "PRESSURE_BEGIN";
        const string PressureEnd = "PRESSURE_END";
        const string TempBegin = "TEMP_BEGIN";
        const string TempEnd = "TEMP_END";

        float Pressure;
        float Temperature;
    }
}
