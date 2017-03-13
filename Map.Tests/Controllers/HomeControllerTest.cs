using System.Web.Mvc;
using Map;
using Map.Controllers;
using NUnit.Framework;
using Map.Tests.Services;

namespace Map.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void HomeIndexPage()
        {
			SmallUrlTestService smallUrlService = new SmallUrlTestService();
			CampusTestService campusService = new CampusTestService();

			// Arrange
			HomeController controller = new HomeController(smallUrlService, campusService);

            // Act
            ViewResult result = controller.Index(new string[] { }, "") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Pullman", result.ViewBag.city);
        }
    }
}
