using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using XdtExtensions.Microsoft.Web.XmlTransform;
using XdtExtensions.Microsoft.Web.XmlTransform.Properties;
using System.Text.RegularExpressions;

namespace XdtExtensions
{
    internal class SetAttributeRegexExt : AttributeTransform
    {
        private string GetTargetAttributeName()
        {
            if (Arguments?.Count == 0 || Arguments.Count > 1)
            {
                throw new XmlTransformationException("Exactly one attibute must be specified as an argument. Example: SetAttributeExtRegex(attributeName)");
            }
            else
            {
                return Arguments[0];
            }
        }

        protected override void Apply()
        {
            var patternNode = TransformNode.SelectSingleNode($"//{DefaultNamespace.Prefix}:Pattern", DefaultNamespace.GetNamespaceManager());
            if (patternNode == null)
            {
                throw new XmlTransformationException($"{nameof(SetAttributeRegexExt)} requires a childnode {DefaultNamespace.Prefix}:Pattern");
            }

            var cDataSection = patternNode.ChildNodes.OfType<XmlCDataSection>().SingleOrDefault();
            if (cDataSection == null)
            {
                throw new XmlTransformationException($"{nameof(SetAttributeRegexExt)} The Pattern node must have an {nameof(XmlCDataSection)} containing the Regex pattern");
            }

            var pattern = cDataSection.Value;
            var replacementAttribute = patternNode.Attributes.GetNamedItem("Replacement");

            if (replacementAttribute == null)
            {
                throw new XmlTransformationException($"{nameof(SetAttributeRegexExt)} The Pattern node must have a 'Replacement' Attribute");
            }

            var replacement = replacementAttribute.Value;

            var targetAttributeName = GetTargetAttributeName();
            var targetAttribute = TargetNode.Attributes.GetNamedItem(targetAttributeName);

            if (targetAttribute == null)
            {
                Log.LogWarning($"No attribute {targetAttributeName} on target node");
                return;
            }

            try
            {
                targetAttribute.Value = Regex.Replace(targetAttribute.Value ?? "", pattern?.Trim(), replacement, RegexOptions.IgnorePatternWhitespace, TimeSpan.FromSeconds(1));
            }
            catch (Exception ex)
            {
                throw new XmlTransformationException($"{nameof(SetAttributeRegexExt)} Regex.Replace failed", ex);
            }
         
        }
    }
}
