using System.IO;

using Xunit;

namespace XdtExtensions.Test
{
    public class XdtLibTests
    {
        [Theory]
        [InlineData("XDT_Base_AttributeFormatting")]
        //[InlineData("XDT_Base_TagFormatting")] not the same, but better output
        public void XDT_Base(string dir)
        {
            AssertionHelper.AssertResults(dir);
        }
    }
}
