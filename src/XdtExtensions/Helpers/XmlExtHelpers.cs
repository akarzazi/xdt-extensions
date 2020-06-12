using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XdtExtensions.Helpers
{
    public class XmlExtHelpers
    {
        public static IReadOnlyCollection<XmlNode> GetBeforeNodes(XmlNode target)
        {
            return target.SelectNodes($"//{DefaultNamespace.Prefix}:before", DefaultNamespace.GetNamespaceManager())
                .ToCollection();
        }

        public static IReadOnlyCollection<XmlNode> GetAfterNodes(XmlNode target)
        {
            return target.SelectNodes($"//{DefaultNamespace.Prefix}:after", DefaultNamespace.GetNamespaceManager())
                .ToCollection();
        }
    }
}
