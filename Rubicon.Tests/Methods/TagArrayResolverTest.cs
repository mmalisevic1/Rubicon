using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rubicon.Data.Tables;
using Rubicon.Services;

namespace Rubicon.Tests.Methods
{
    [TestClass]
    public class TagArrayResolverTest
    {
        [TestMethod]
        public void CreateTagArrayFromTags()
        {
            // Arrange
            IEnumerable<Tags> tags = new List<Tags>
            {
                new Tags { Tag = "iOS"},
                new Tags { Tag = "AR"}
            };

            // Act
            var result = TagArrayResolver.CreateTagArrayFromTags(tags);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string[]));
            CollectionAssert.AreEqual(new string[] { "iOS", "AR" }, result);
        }

        [TestMethod]
        public void CreateTagArrayFromTagsReturnsEmptyArray()
        {
            // Arrange
            IEnumerable<Tags> tags = new List<Tags>();

            // Act
            var result = TagArrayResolver.CreateTagArrayFromTags(tags);

            // Assert
            Assert.AreEqual(0, result.Length);
            Assert.IsInstanceOfType(result, typeof(string[]));
        }
    }
}
