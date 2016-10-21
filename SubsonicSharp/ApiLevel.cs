using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal class ApiLevel :Attribute
    {
        public int Level { get; set; } //Minimum required Server Api Version

        public ApiLevel(int level)
        {
            Level = level;
        }

        public ApiLevel()
        {
            Level = -1;
        }
    }
}
