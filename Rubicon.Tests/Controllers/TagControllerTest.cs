using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rubicon.Controllers;
using Rubicon.Services;

namespace Rubicon.Tests.Controllers
{
    [TestClass]
    public class TagControllerTest
    {
        [TestMethod]
        public void GetTagsReturnsOkResponse()
        {
            // Arrange
            List<string> tags = new List<string>
            {
                "iOS", "AR", "Gazzda"
            };
            var mockRepository = new Mock<ITagService>();
            mockRepository.Setup(s => s.GetTags())
                          .Returns(tags);
            var controller = new TagController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetTags();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<string>>;
            var result = createdResult.Content;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(IEnumerable<string>));
            createdResult.Content.Should().NotContainNulls();
            createdResult.Content.Should().HaveCount(3);
            createdResult.Content.Should().Contain("AR");
        }

        [TestMethod]
        public void GetTagsReturnsOkNoResponseBody()
        {
            // Arrange
            var mockRepository = new Mock<ITagService>();
            var controller = new TagController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetTags();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<string>>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            createdResult.Content.Should().BeEmpty();
        }
    }
}
