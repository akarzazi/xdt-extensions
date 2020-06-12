using System.Xml;

namespace XdtExtensions
{
    public class DefaultNamespace
    {
        public static string Namespace = "xdt-extensions";
        public static string Prefix = "xdtExt";

        public static XmlNamespaceManager GetNamespaceManager()
        {
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace(Prefix, Namespace);
            return namespaceManager;
        }
    }
}
