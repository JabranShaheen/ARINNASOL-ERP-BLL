using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ARINNASOL_ERP_BLL_API;
using ARINNASOL_ERP_BLL_API.Controllers;

namespace ARINNASOL_ERP_BLL_API.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
