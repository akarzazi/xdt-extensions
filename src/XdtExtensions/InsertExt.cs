using System.Xml;

using XdtExtensions.Microsoft.Web.XmlTransform;

using XdtExtensions.Helpers;
using XdtExtensions.Microsoft.Web.XmlTransform.Properties;
using System.Globalization;

namespace XdtExtensions
{
    internal class InsertExt : Transform
    {
        public InsertExt()
                   : base(TransformFlags.UseParentAsTargetNode, MissingTargetMessage.Error)
        {
        }

        protected override void Apply()
        {
            CommonErrors.ExpectNoArguments(Log, TransformNameShort, ArgumentString);

            TargetNode.AppendChildren(SurroundBehavior.ExtractNodes(TransformNode));

            Log.LogMessage(MessageType.Verbose, Resources.XMLTRANSFORMATION_TransformMessageInsert, TransformNode.Name);
        }
    }

    internal class InsertAllExt : Transform
    {
        public InsertAllExt()
            : base(TransformFlags.UseParentAsTargetNode, MissingTargetMessage.Error)
        {
            ApplyTransformToAllTargetNodes = true;
        }

        protected override void Apply()
        {
            CommonErrors.ExpectNoArguments(Log, TransformNameShort, ArgumentString);

            TargetNode.AppendChildren(SurroundBehavior.ExtractNodes(TransformNode));

            Log.LogMessage(MessageType.Verbose, Resources.XMLTRANSFORMATION_TransformMessageInsert, TransformNode.Name);
        }
    }

    internal class InsertIfMissingExt : InsertExt
    {
        public InsertIfMissingExt()
                   : base()
        {
        }

        protected override void Apply()
        {
            if (this.TargetChildNodes == null || this.TargetChildNodes.Count == 0)
            {
                base.Apply();
                Log.LogMessage(MessageType.Verbose, Resources.XMLTRANSFORMATION_TransformMessageInsert, TransformNode.Name);
            }
        }
    }

    public abstract class InsertBase : Transform
    {
        public InsertBase()
            : base(TransformFlags.UseParentAsTargetNode, MissingTargetMessage.Error)
        {
        }

        private XmlNode siblingElement = null;

        protected XmlNode SiblingElement
        {
            get
            {
                if (siblingElement == null)
                {
                    if (Arguments == null || Arguments.Count == 0)
                    {
                        throw new XmlTransformationException(string.Format(CultureInfo.CurrentCulture, Resources.XMLTRANSFORMATION_InsertMissingArgument, GetType().Name));
                    }
                    else if (Arguments.Count > 1)
                    {
                        throw new XmlTransformationException(string.Format(CultureInfo.CurrentCulture, Resources.XMLTRANSFORMATION_InsertTooManyArguments, GetType().Name));
                    }
                    else
                    {
                        string xpath = Arguments[0];
                        XmlNodeList siblings = TargetNode.SelectNodes(xpath);
                        if (siblings.Count == 0)
                        {
                            throw new XmlTransformationException(string.Format(CultureInfo.CurrentCulture, Resources.XMLTRANSFORMATION_InsertBadXPath, xpath));
                        }
                        else
                        {
                            siblingElement = siblings[0];
                        }
                    }
                }

                return siblingElement;
            }
        }
    }

    public class InsertAfterExt : InsertBase
    {
        protected override void Apply()
        {
            var nodes = SurroundBehavior.ExtractNodes(TransformNode);
            XmlNode reference = SiblingElement;
            foreach (var node in nodes)
            {
                SiblingElement.ParentNode.InsertAfter(node, reference);
                reference = node;
            }

            Log.LogMessage(MessageType.Verbose, string.Format(CultureInfo.CurrentCulture, Resources.XMLTRANSFORMATION_TransformMessageInsert, TransformNode.Name));
        }
    }

    public class InsertBeforeExt : InsertBase
    {
        protected override void Apply()
        {
            var nodes = SurroundBehavior.ExtractNodes(TransformNode);
            foreach (var node in nodes)
            {
                SiblingElement.ParentNode.InsertBefore(node, SiblingElement);
            }

            Log.LogMessage(MessageType.Verbose, string.Format(CultureInfo.CurrentCulture, Resources.XMLTRANSFORMATION_TransformMessageInsert, TransformNode.Name));
        }
    }
}
