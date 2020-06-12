using System.Xml;

using XdtExtensions.Microsoft.Web.XmlTransform;

using XdtExtensions.Helpers;

namespace XdtExtensions
{
    public class InsertExt : Transform
    {
        public InsertExt()
                   : base(TransformFlags.UseParentAsTargetNode, MissingTargetMessage.Error)
        {
        }

        protected override void Apply()
        {
            TargetNode.AppendChildren(InsertBehavior.InsertExtension(TransformNode));
            Log.LogMessage(MessageType.Verbose, "Inserted '{0}' element", TransformNode.Name);
        }
    }

    public class InsertIfMissingExt : InsertExt
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
                        throw new XmlTransformationException(string.Format("{0} requires an XPath argument", GetType().Name));
                    }
                    else if (Arguments.Count > 1)
                    {
                        throw new XmlTransformationException(string.Format("Too many arguments to {0}", GetType().Name));
                    }
                    else
                    {
                        string xpath = Arguments[0];
                        XmlNodeList siblings = TargetNode.SelectNodes(xpath);
                        if (siblings.Count == 0)
                        {
                            throw new XmlTransformationException(string.Format("No element in the source document matches '{0}'", xpath));
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
            var nodes = InsertBehavior.InsertExtension(TransformNode);
            XmlNode reference = SiblingElement;
            foreach (var node in nodes)
            {
                SiblingElement.ParentNode.InsertAfter(node, reference);
                reference = node;
            }


            Log.LogMessage(MessageType.Verbose, string.Format("Inserted '{0}' element", TransformNode.Name));
        }
    }

    public class InsertBeforeExt : InsertBase
    {
        protected override void Apply()
        {
            var nodes = InsertBehavior.InsertExtension(TransformNode);
            foreach (var node in nodes)
            {
                SiblingElement.ParentNode.InsertBefore(node, SiblingElement);
            }

            Log.LogMessage(MessageType.Verbose, string.Format("Inserted '{0}' element", TransformNode.Name));
        }
    }
}
