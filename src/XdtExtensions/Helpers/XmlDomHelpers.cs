using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using XdtExtensions.Microsoft.Web.XmlTransform;

namespace XdtExtensions.Helpers
{
    public static class XmlDomHelpers
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

        public static IReadOnlyCollection<XmlNode> FindTargetsFromXPathArg(IList<string> arguments, XmlNode targetNode, string typeName)
        {
            if (arguments == null || arguments.Count == 0)
            {
                throw new XmlTransformationException(string.Format("{0} requires an XPath argument", typeName));
            }
            else if (arguments.Count > 1)
            {
                throw new XmlTransformationException(string.Format("Too many arguments to {0}", typeName));
            }
            else
            {
                string xpath = arguments[0];
                return targetNode.SelectNodes(xpath).ToCollection();
            }
        }
    }
}
