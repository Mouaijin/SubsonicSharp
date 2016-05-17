using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    //Just a wrapper for Typing
    public class Video : Child
    {
        public new static Video Create(XElement xml)
        {
            return (Video) Child.Create(xml);
        }

        public static Video Create(Child child)
        {
            return (Video) child;
        }
    }
}
