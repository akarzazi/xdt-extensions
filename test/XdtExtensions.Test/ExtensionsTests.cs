using System.IO;

using Xunit;

namespace XdtExtensions.Test
{
    public class ExtensionsTests
    {
        [Theory]
        [InlineData("InsertExt_before")]
        [InlineData("InsertExt_after")]
        [InlineData("InsertAfterExt_before")]
        [InlineData("InsertAfterExt_after")]
        [InlineData("InsertBeforeExt_before")]
        [InlineData("InsertBeforeExt_after")]
        [InlineData("RemoveExt")]
        [InlineData("RemoveAllExt")]
        public void Extensions(string dir)
        {
            AssertionHelper.AssertResults(dir, assertOnOuterXml: true);
        }

        [Theory]
        [InlineData("Insert")]
        public void Insert(string dir)
        {
            AssertionHelper.AssertResults(dir, assertOnOuterXml: true);
        }
    }
}
