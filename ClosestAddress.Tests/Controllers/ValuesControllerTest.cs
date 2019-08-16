using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClosestAddress;
using ClosestAddress.Controllers;

namespace ClosestAddress.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
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

    }
}
