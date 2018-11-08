using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Rubicon.Controllers;
using Rubicon.Models;
using Rubicon.Services;

namespace Rubicon.Tests.Controllers
{
    [TestClass]
    public class BlogPostControllerTest
    {
        [TestMethod]
        public async Task CreateBlogPostReturnsCreatedResponse()
        {
            // Arrange
            BlogPost blogPost = new BlogPost
            {
                Title = "Augmented Reality iOS Application",
                Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                TagList = new string[] { "iOS", "AR" }
            };
            var mockRepository = new Mock<IBlogPostService>();
            mockRepository.Setup(s => s.CreateBlogPost(blogPost))
                          .Returns(Task.FromResult(new BlogPost
                          {
                              Slug = "augmented_reality_ios_application",
                              TagList = new string[] { "iOS", "AR" },
                              CreatedAt = DateTime.Now,
                              UpdatedAt = null
                          }));
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.CreateBlogPost(blogPost);
            var createdResult = actionResult as CreatedNegotiatedContentResult<BlogPost>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(BlogPost));
            Assert.AreEqual("augmented_reality_ios_application", createdResult.Content.Slug);
        }

        [TestMethod]
        public async Task GetBlogPostReturnsOkResponse()
        {
            // Arrange
            BlogPost blogPost = new BlogPost
            {
                Slug = "augmented_reality_iOS_application",
                Title = "Augmented Reality iOS Application",
                Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                TagList = new string[] { "iOS", "AR" }
            };
            var mockRepository = new Mock<IBlogPostService>();
            mockRepository.Setup(s => s.GetBlogPostBySlug(blogPost.Slug))
                          .Returns(Task.FromResult(blogPost));
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.GetBlogPost(blogPost.Slug);
            var createdResult = actionResult as OkNegotiatedContentResult<BlogPost>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(BlogPost));
            Assert.AreEqual("augmented_reality_iOS_application", blogPost.Slug);
        }

        [TestMethod]
        public async Task GetBlogPostReturnsOkNoResponseBody()
        {
            // Arrange
            var mockRepository = new Mock<IBlogPostService>();
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.GetBlogPost("internet_trends_2018");
            var createdResult = actionResult as OkNegotiatedContentResult<BlogPost>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNull(createdResult.Content);
        }

        [TestMethod]
        public void GetBlogPostsReturnsOkResponse()
        {
            // Arrange
            List<BlogPost> blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Slug = "augmented_reality_iOS_application",
                    Title = "Augmented Reality iOS Application",
                    Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    TagList = new string[] { "iOS", "AR" }
                },
                new BlogPost
                {
                    Slug = "augmented_reality_iOS_application_2",
                    Title = "Augmented Reality iOS Application 2",
                    Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                    Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                    TagList = new string[] { "iOS", "AR", "Gazzda" }
                }
            };
            var mockRepository = new Mock<IBlogPostService>();
            mockRepository.Setup(s => s.GetBlogPosts(""))
                          .Returns(blogPosts);
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetBlogPosts();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<BlogPost>>;
            var result = createdResult.Content;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(IEnumerable<BlogPost>));
            createdResult.Content.Should().NotContainNulls();
            createdResult.Content.Should().HaveCount(2);
            createdResult.Content.Should().Contain(c => c.Slug == "augmented_reality_iOS_application_2");
        }

        [TestMethod]
        public void GetBlogPostsReturnsOkNoResponseBody()
        {
            // Arrange
            var mockRepository = new Mock<IBlogPostService>();
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetBlogPosts();
            var createdResult = actionResult as OkNegotiatedContentResult<IEnumerable<BlogPost>>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            createdResult.Content.Should().BeEmpty();
        }

        [TestMethod]
        public async Task UpdateBlogPostReturnsOkResponse()
        {
            // Arrange
            BlogPost blogPost = new BlogPost
            {
                Slug = "augmented_reality_ios_application",
                Title = "Augmented Reality iOS Application",
                Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                TagList = new string[] { "iOS", "AR" }
            };
            var mockRepository = new Mock<IBlogPostService>();
            mockRepository.Setup(s => s.UpdateBlogPost(blogPost.Slug, blogPost))
                          .Returns(Task.FromResult(new BlogPost
                          {
                              Slug = "augmented_reality_ios_application_more",
                              Title = "Augmented Reality iOS Application More",
                              Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                              Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                              TagList = new string[] { "iOS", "AR" },
                              CreatedAt = DateTime.Now,
                              UpdatedAt = null
                          }));
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.UpdateBlogPost(blogPost.Slug, blogPost);
            var createdResult = actionResult as OkNegotiatedContentResult<BlogPost>;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(createdResult.Content);
            Assert.IsInstanceOfType(createdResult.Content, typeof(BlogPost));
            Assert.AreEqual("augmented_reality_ios_application_more", createdResult.Content.Slug);
        }

        [TestMethod]
        public async Task UpdateBlogPostReturnsNotFound()
        {
            // Arrange
            BlogPost blogPost = new BlogPost
            {
                Slug = "augmented_reality_ios_application",
                Title = "Augmented Reality iOS Application",
                Description = "Rubicon Software Development and Gazzda furniture are proud to launch an augmented reality app.",
                Body = "The app is simple to use, and will help you decide on your best furniture fit.",
                TagList = new string[] { "iOS", "AR" }
            };
            var mockRepository = new Mock<IBlogPostService>();
            mockRepository.Setup(s => s.UpdateBlogPost(blogPost.Slug, blogPost))
                          .Throws(new ArgumentException());
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.UpdateBlogPost(blogPost.Slug, blogPost);

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteBlogPostReturnsOkResponse()
        {
            // Arrange
            var mockRepository = new Mock<IBlogPostService>();
            mockRepository.Setup(s => s.DeleteBlogPost("augmented_reality_ios_application"))
                          .Returns(Task.FromResult(default(object)));
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.DeleteBlogPost("augmented_reality_ios_application");

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public async Task DeleteBlogPostReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IBlogPostService>();
            mockRepository.Setup(s => s.DeleteBlogPost("augmented_reality_ios_application"))
                          .ThrowsAsync(new ArgumentException());
            var controller = new BlogPostController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = await controller.DeleteBlogPost("augmented_reality_ios_application");

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
