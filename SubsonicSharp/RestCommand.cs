using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp
{
    public class RestCommand
    {
        public string MethodName { get; set; }
        public List<RestParameter> Parameters { get; set; } = new List<RestParameter>();

        public string ParameterString()
        {
            StringBuilder chain = new StringBuilder();
            foreach (RestParameter parameter in Parameters)
            {
                chain.Append(parameter + "&");
            }
            return chain.ToString();
        }
    }
}
