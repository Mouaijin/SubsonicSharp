using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubsonicSharp.SubTypes
{
    public class Directory : BasicItem
    {
        public int? Parent { get; set; }
        public DateTime? Starred { get; set; }
        public int? UserRating { get; set; }
        public int? AverageRating { get; set; }
        public IEnumerable<Child> Children { get; set; }

        public static Directory Create(XDocument xml)
        {
            Directory dir = new Directory();
            XElement root = GetDirectoryElement(xml);
            dir.Parent = root.HasAttribute("parent")
                ? Convert.ToInt32(root.Attribute("parent").Value)
                : new int?();
            dir.UserRating = root.HasAttribute("userRating")
                ? Convert.ToInt32(root.Attribute("userRating").Value)
                : new int?();
            dir.AverageRating = root.HasAttribute("averageRating")
                ? Convert.ToInt32(root.Attribute("averageRating").Value)
                : new int?();
            dir.Starred = root.HasAttribute("starred")
                ? DateTime.Parse(root.Attribute("starred").Value)
                : new DateTime?();
            dir.Id = Convert.ToInt32(root.Attribute("id").Value);
            dir.Name = root.Attribute("name").Value;
            dir.Kind = ItemType.Directory;
            dir.Children = ReadChildren(root);
            return dir;
        }

        private static XElement GetDirectoryElement(XDocument xml) => xml.Root?.Elements().First();

        private static IEnumerable<Child> ReadChildren(XElement xml)
        {
            return xml.Elements().Where(x => x.Name.LocalName == "child").Select(Child.Create);
        }
    }
}