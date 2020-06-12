using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XdtExtensions.Helpers
{
    public static class XmlNodeExtensions
    {
        public static void RemoveNodes(this XmlNode target, IEnumerable<XmlNode> nodes)
        {
            foreach (XmlNode node in nodes)
            {
                target.RemoveChild(node);
            }
        }

        public static void AppendChildren(this XmlNode target, XmlNodeList nodeList)
        {
            AppendChildren(target, nodeList.ToCollection());
        }

        public static void AppendChildren(this XmlNode target, IReadOnlyCollection<XmlNode> nodes)
        {
            foreach (XmlNode node in nodes)
            {
                target.AppendChild(node);
            }
        }

        public static IReadOnlyCollection<XmlNode> ToCollection(this XmlNodeList list)
        {
            return list.Cast<XmlNode>().ToArray();
        }
    }
}
