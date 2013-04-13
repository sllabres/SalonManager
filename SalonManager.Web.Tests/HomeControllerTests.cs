using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SalonManager.Web.Controllers;

namespace SalonManager.Web.Tests
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void WhenCallingIndexThenViewReturned()
        {
            var homeController = new HomeController();
            var actionResult = homeController.Index();
            Assert.NotNull(actionResult);
        }
    }
}
