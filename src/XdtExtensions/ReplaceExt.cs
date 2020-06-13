using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using XdtExtensions.Microsoft.Web.XmlTransform;
using XdtExtensions.Microsoft.Web.XmlTransform.Properties;

namespace XdtExtensions
{
    internal class ReplaceExt : Transform
    {
        public ReplaceExt()
        {
        }

        protected override void Apply()
        {
            CommonErrors.ExpectNoArguments(Log, TransformNameShort, ArgumentString);

            XmlNode parentNode = TargetNode.ParentNode;
            var nodes = SurroundBehavior.ExtractNodes(TransformNode);

            XmlNode reference = null;
            foreach (var node in nodes)
            {
                if (reference == null)
                {
                    parentNode.ReplaceChild(node, TargetNode);
                }
                else
                {
                    parentNode.InsertAfter(node, reference);
                }
                reference = node;
            }

            Log.LogMessage(MessageType.Verbose, Resources.XMLTRANSFORMATION_TransformMessageReplace, TargetNode.Name);
        }
    }

    internal class ReplaceAllExt : ReplaceExt
    {
        public ReplaceAllExt()
        {
            ApplyTransformToAllTargetNodes = true;
        }

        protected override void Apply()
        {
            CommonErrors.ExpectNoArguments(Log, TransformNameShort, ArgumentString);

            base.Apply();

            Log.LogMessage(MessageType.Verbose, Resources.XMLTRANSFORMATION_TransformMessageReplace, TargetNode.Name);
        }
    }
}
