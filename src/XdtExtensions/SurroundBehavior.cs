using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using XdtExtensions.Helpers;

namespace XdtExtensions
{
    public class SurroundBehavior
    {
        public static IReadOnlyCollection<XmlNode> ExtractNodes(XmlNode TransformNode)
        {
            var beforeNodes = XmlExtHelpers.GetBeforeNodes(TransformNode);
            var afterNodes = XmlExtHelpers.GetAfterNodes(TransformNode);

            TransformNode.RemoveNodes(beforeNodes);
            TransformNode.RemoveNodes(afterNodes);

            if (TransformNode is XmlElement element)
            {
                if (string.IsNullOrWhiteSpace(element.InnerXml))
                    element.IsEmpty = true;
            }

            var nodes = new List<XmlNode>();
            foreach (XmlNode node in beforeNodes)
            {
                nodes.AddRange(node.ChildNodes.ToCollection());
            }

            nodes.Add(TransformNode);

            foreach (XmlNode node in afterNodes)
            {
                nodes.AddRange(node.ChildNodes.ToCollection());
            }

            return nodes;
        }
    }
}
