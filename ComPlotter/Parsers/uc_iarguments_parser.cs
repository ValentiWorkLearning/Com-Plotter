using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPlotter.Factories
{
    public interface IArgumentsParser
    {
        Enum TryParseValue( object _value );
    }
}
