using System.IO;

using Xunit;

namespace XdtExtensions.Test
{
    public class ExtensionsTests
    {
        [Theory]
        [InlineData("InsertAllExt_before")]
        [InlineData("InsertAllExt_after")]
        [InlineData("InsertExt_before")]
        [InlineData("InsertExt_after")]
        [InlineData("InsertAfterExt_before")]
        [InlineData("InsertAfterExt_after")]
        [InlineData("InsertBeforeExt_before")]
        [InlineData("InsertBeforeExt_after")]
        [InlineData("RemoveExt")]
        [InlineData("RemoveAllExt")]
        [InlineData("ReplaceExt_after")]
        [InlineData("ReplaceExt_before")]
        [InlineData("ReplaceAllExt")]
        [InlineData("ReplaceAllExt_after")]
        [InlineData("ReplaceAllExt_before")]
        public void Extensions(string dir)
        {
            AssertionHelper.AssertResults(dir, assertOnOuterXml: true);
        }
    }
}
