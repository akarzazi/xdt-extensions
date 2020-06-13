using System;
using System.IO;

using XdtExtensions.Microsoft.Web.XmlTransform;

namespace TestExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            var xml = File.ReadAllText("samples/source.xml");
            var xdt = File.ReadAllText("samples/transform.xml");

            using (XmlTransformableDocument document = new XmlTransformableDocument() { PreserveWhitespace = true })
            using (XmlTransformation transformation = new XmlTransformation(xdt, isTransformAFile: false, null))
            {
                document.LoadXml(xml);

                var success = transformation.Apply(document);
                if (!success)
                {
                    throw new Exception($"An error has occurred on apply transform, use IXmlTransformationLogger for more details.");
                }

                document.Save(new MemoryStream());

                if (xml == document.OuterXml)
                {
                    Console.WriteLine("File not changed");
                }
                else
                {
                    Console.WriteLine("File transformed");

                    Console.WriteLine("\n\nSource \n\n" + xml);
                    Console.WriteLine("\n\nResult \n\n" + document.OuterXml);
                }
            }
        }
    }
}
