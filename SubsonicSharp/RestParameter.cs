using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp
{
    //Basic wrapper for a method parameter
    public class RestParameter
    {
        public string Parameter { get; set; }
        public string Value { get; set; }

        public RestParameter()
        {
        }

        public RestParameter(string parameter, string value)
        {
            Parameter = parameter;
            Value = value;
        }

        public RestParameter(string parameter, int value)
        {
            Parameter = parameter;
            Value = value.ToString();
        }

        public override string ToString()
        {
            return $"{Parameter}={Value}";
        }
    }
}
