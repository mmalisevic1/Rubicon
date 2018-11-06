using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rubicon.Services;

namespace Rubicon.Tests.Methods
{
    [TestClass]
    public class SlugResolverTest
    {
        [TestMethod]
        public void TestRemoveDiacritics()
        {
            // Arrange
            string slug1 = "this-that-other-outré-collection";
            string slug2 = "ćevapčići-at-džidžikovac";

            // Act
            var result1 = SlugResolver.RemoveDiacritics(slug1);
            var result2 = SlugResolver.RemoveDiacritics(slug2);

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(string));
            Assert.AreEqual("this-that-other-outre-collection", result1);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(string));
            Assert.AreEqual("cevapcici-at-dzidzikovac", result2);
        }

        [TestMethod]
        public void TestCreatingSlug()
        {
            // Arrange
            string title1 = "Augmented Reality iOS Application";
            string title2 = "Augmented Reality iOS Application 2";
            string title3 = "Internet Trends 2018";
            string title4 = "React Why and How?";
            string title5 = "  ;?]  uLTImaté must-do TEST,  . \"";

            // Act
            var result1 = SlugResolver.GetSlugFromTitle(title1);
            var result2 = SlugResolver.GetSlugFromTitle(title2);
            var result3 = SlugResolver.GetSlugFromTitle(title3);
            var result4 = SlugResolver.GetSlugFromTitle(title4);
            var result5 = SlugResolver.GetSlugFromTitle(title5);

            // Assert
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(string));
            Assert.AreEqual("augmented_reality_ios_application", result1);
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(string));
            Assert.AreEqual("augmented_reality_ios_application_2", result2);
            Assert.IsNotNull(result3);
            Assert.IsInstanceOfType(result3, typeof(string));
            Assert.AreEqual("internet_trends_2018", result3);
            Assert.IsNotNull(result4);
            Assert.IsInstanceOfType(result4, typeof(string));
            Assert.AreEqual("react_why_and_how", result4);
            Assert.IsNotNull(result5);
            Assert.IsInstanceOfType(result5, typeof(string));
            Assert.AreEqual("ultimate_mustdo_test", result5);
        }
    }
}
