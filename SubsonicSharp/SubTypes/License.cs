using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicSharp.SubTypes
{
    public class License
    {
        public bool Valid { get;}
        public string Email { get;}
        public DateTime Expires { get;}

        public License(string valid, string email, string date)
        {
            Valid = valid.Equals("true");
            Email = email;
            Expires = DateTime.Parse(date);
        }
    }
}
