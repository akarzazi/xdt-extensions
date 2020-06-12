using System.IO;

using Xunit;

namespace XdtExtensions.Test
{
    public class ExtensionsTests
    {
        [Theory]
        [InlineData("InsertExt_before")]
        [InlineData("InsertExt_after")] //this one is failing at formatting
        public void InsertExt(string dir)
        {
            var rootPath = Path.Combine("Assets", dir);
            AssertionHelper.AssertResults(rootPath);
        }

        [Theory]
        [InlineData("InsertAfterExt_before")]
        [InlineData("InsertAfterExt_after")]
        public void InsertAfterExt(string dir)
        {
            var rootPath = Path.Combine("Assets", dir);
            AssertionHelper.AssertResults(rootPath);
        }

        [Theory]
        [InlineData("InsertBeforeExt_before")]
        [InlineData("InsertBeforeExt_after")]
        public void InsertBeforeExt(string dir)
        {
            var rootPath = Path.Combine("Assets", dir);
            AssertionHelper.AssertResults(rootPath);
        }

        [Theory]
        [InlineData("RemoveExt")]
        public void RemoveExt(string dir)
        {
            var rootPath = Path.Combine("Assets", dir);
            AssertionHelper.AssertResults(rootPath);
        }

        [Theory]
        [InlineData("RemoveAllExt")]
        public void RemoveAllExt(string dir)
        {
            var rootPath = Path.Combine("Assets", dir);
            AssertionHelper.AssertResults(rootPath);
        }

        // fork impact
        [Theory]
        [InlineData("Insert")]
        // [InlineData("InsertExt_after")] this one is failing at formatting
        public void Insert(string dir)
        {
            var rootPath = Path.Combine("Assets", dir);
            AssertionHelper.AssertResults(rootPath);
        }
    }
}
