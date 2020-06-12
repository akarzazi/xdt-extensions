using System.Collections.Generic;
using System.Xml;

using XdtExtensions.Microsoft.Web.XmlTransform;

using XdtExtensions.Helpers;

namespace XdtExtensions
{
    public class RemoveExt : Transform
    {
        public RemoveExt()
         : base(TransformFlags.UseParentAsTargetNode, MissingTargetMessage.Information)
        {
        }

        protected override void Apply()
        {
            var targets = FindTargets();

            foreach (var target in targets)
            {
                target.ParentNode?.RemoveChild(target);
                break;
            }
        }

        protected IReadOnlyCollection<XmlNode> FindTargets()
        {
            if (Arguments == null || Arguments.Count == 0)
            {
                throw new XmlTransformationException(string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0} requires an XPath argument", GetType().Name));
            }
            else if (Arguments.Count > 1)
            {
                throw new XmlTransformationException(string.Format(System.Globalization.CultureInfo.CurrentCulture, "Too many arguments to {0}", GetType().Name));
            }
            else
            {
                string xpath = Arguments[0];
                return TargetNode.SelectNodes(xpath).ToCollection();
            }
        }
    }

    internal class RemoveAllExt : RemoveExt
    {
        public RemoveAllExt()
        {
            ApplyTransformToAllTargetNodes = true;
        }

        protected override void Apply()
        {
            var targets = FindTargets();

            foreach (var target in targets)
            {
                target.ParentNode?.RemoveChild(target);
            }
        }
    }
}
