using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClosestAddress;
using ClosestAddress.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace ClosestAddress.Tests.Controllers
{
    [TestClass]
    class HomeControllerTest
    {
        [TestMethod]
        public void DistanceBetweenCoordinates_ShouldEqual()
        {
            // Arrange
            double expected = 779.58968145724282;
            double lat1 = -38.1580694, lon1 = 144.34695, lat2 = -33.868820190429688, lon2 = 151.20928955078125;

            // Act
            double actual = Helpers.Helper.GetDistanceBetweenPoints(lat1, lon1, lat2, lon2);
            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void Get()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }
    }

}
