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

        public void AddParameter(string name, string value)
        {
            Parameters.Add(new RestParameter(name, value));
        }

        public void AddParameter(string name, int value)
        {
            Parameters.Add(new RestParameter(name, value));
        }


        public void AddParameter(string name, bool value)
        {
            Parameters.Add(new RestParameter(name,value));
        }
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
