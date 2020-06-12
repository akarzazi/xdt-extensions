using System.IO;

using Xunit;

namespace XdtExtensions.Test
{
    public class XdtBaseTests
    {
        [Theory]
        [InlineData("XDT_Base_AttributeFormatting")]
        public void XDT_Base(string dir)
        {
            var rootPath = Path.Combine("Assets", dir);
            AssertionHelper.AssertResults(rootPath);
        }
    }
}
