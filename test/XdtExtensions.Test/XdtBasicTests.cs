using System.IO;

using Xunit;

namespace XdtExtensions.Test
{
    public class XdtBaseTests
    {
        [Theory]
        [InlineData("Insert")]
        [InlineData("Replace")]
        public void Insert(string dir)
        {
            AssertionHelper.AssertResults(dir, assertOnOuterXml: true);
        }
    }
}
