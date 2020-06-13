using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using XdtExtensions.Microsoft.Web.XmlTransform;

using Xunit;

namespace XdtExtensions
{
    public class AssertionHelper
    {
        public static void AssertResults(string dir, bool assertOnOuterXml = false)
        {
            var rootPath = Path.Combine("Assets", dir);

            var xmlPath = Path.Combine(rootPath, "xml.xml");
            var xdtPath = Path.Combine(rootPath, "xdt.xml");
            var resultPath = Path.Combine(rootPath, "xml-result.xml");

            var xml = File.ReadAllText(xmlPath);
            var xdt = File.ReadAllText(xdtPath);
            var expected = File.ReadAllText(resultPath);

            var result = "";
            using (XmlTransformableDocument document = new XmlTransformableDocument() { PreserveWhitespace = true })
            using (XmlTransformation transformation = new XmlTransformation(xdt, isTransformAFile: false, null))
            {
                document.Load(xmlPath);

                var success = transformation.Apply(document);
                if (!success)
                {
                    string message = $"There was an unknown error on the transform.";
                    throw new Exception(message);
                }
                using var ms = new MemoryStream();
                document.Save(ms);

                if (assertOnOuterXml)
                {
                    result = document.OuterXml;
                }
                else
                {
                    ms.Position = 0;
                    using (StreamReader sr = new StreamReader(ms, true))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }

            var expectedLines = expected.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var resultLines = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < expectedLines.Length; i++)
            {
                var expectedLine = expectedLines[i];
                var resultLine = resultLines[i];

                var trimmedExpected = expectedLine.Trim();
                if (trimmedExpected.Length == 0)
                {
                    Assert.Equal(trimmedExpected, resultLine.Trim());
                    continue;
                }

                Assert.Equal(expectedLine, resultLine);
            }
        }
    }
}
