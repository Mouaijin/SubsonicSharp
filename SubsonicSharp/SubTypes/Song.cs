using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    //Just a wrapper for Typing
    public class Song : Child
    {
        public new static Song Create(XElement xml)
        {
            return (Song) Child.Create(xml);
        }

        public static Song Create(Child child)
        {
            return (Song) child;
        }
    }
}
