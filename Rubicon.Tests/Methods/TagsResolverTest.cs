using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rubicon.Data.Tables;
using Rubicon.Services;

namespace Rubicon.Tests.Methods
{
    [TestClass]
    public class TagsResolverTest
    {
        [TestMethod]
        public void CreateTagsFromTagArray()
        {
            // Arrange
            string[] tagArray = new string[] { "iOS", "AR", "Gazzda" };
            long blogPostId = 2;
            List<Tags> expectedTags = new List<Tags>
            {
                new Tags
                {
                    BlogPostId = blogPostId,
                    Tag = tagArray[0]
                },
                new Tags
                {
                    BlogPostId = blogPostId,
                    Tag = tagArray[1]
                },
                new Tags
                {
                    BlogPostId = blogPostId,
                    Tag = tagArray[2]
                }
            };

            // Act
            var result = TagsResolver.CreateTagsFromTagArray(tagArray, blogPostId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ICollection<Tags>));
            result.Should().NotContainNulls();
            result.Should().HaveCount(3);
            result.Should().Contain(c => c.Tag == expectedTags.FirstOrDefault().Tag);
            result.Select(s => s.Tag)
                  .Should().EndWith(expectedTags.Last().Tag);
        }

        [TestMethod]
        public void CreateTagsFromTagArrayReturnEmptyTagsCollection()
        {
            // Arrange
            string[] tagArray = new string[] {};
            long blogPostId = 2;

            // Act
            var result = TagsResolver.CreateTagsFromTagArray(tagArray, blogPostId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ICollection<Tags>));
            result.Should().HaveCount(0);
        }
    }
}
