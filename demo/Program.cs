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
                    string message = $"There was an unknown error trying while trying to apply the transform.";
                    throw new Exception(message);
                }

                using var ms = new MemoryStream();
                document.Save(ms);

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
