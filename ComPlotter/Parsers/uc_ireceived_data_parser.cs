using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter.Parsers
{
    public interface IReceivedDataParser
    {
        void tryParseString( string _toParse );
        float getTemperature();
        float getPressure();
    }
}
