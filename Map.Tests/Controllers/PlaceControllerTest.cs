using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using NUnit.Framework;
using Map.Models;
using Map.Controllers;
using Map.Data.Services;
using Map.Tests.Services;

namespace Map.Tests.Controllers
{
    [TestFixture]
    public class PlaceControllerTest
    {
        [Test]
        public void PlaceAPIGetAllPlaces()
        {
			// Arrange
			IPlaceService placeService = new PlaceTestService();
			PlaceController controller = new PlaceController(placeService);

            // Act
            IEnumerable<place> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
			Assert.IsTrue(result.Count<place>() == 4);
        }

        [Test]
        public void PlaceAPIGetPlaceById()
        {
			// Arrange
			IPlaceService placeService = new PlaceTestService();
			PlaceController controller = new PlaceController(placeService);

			// Act
			place result = controller.Get(3);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.id);
			Assert.AreEqual("CUB", result.prime_name);
		}

		[Test]
		public void PlaceAPISearchPlace()
		{
			// Arrange
			IPlaceService placeService = new PlaceTestService();
			PlaceController controller = new PlaceController(placeService);

			// Act
			IEnumerable<searchPlace> results = controller.Search("CUB");

			// Assert
			Assert.IsNotNull(results);
			Assert.AreEqual(1, results.Count<searchPlace>());
			Assert.AreEqual(3, results.ToArray<searchPlace>()[0].place_id);
			Assert.AreEqual("CUB", results.ToArray<searchPlace>()[0].label);
		}

		/*
		[Test]
        public void Post()
        {
			// Arrange
			IPlaceService placeService = new PlaceTestService();
			PlaceController controller = new PlaceController(placeService);

			// Act
			//controller.Post("value");

			// Assert
		}

        [Test]
        public void Put()
        {
			// Arrange
			IPlaceService placeService = new PlaceTestService();
			PlaceController controller = new PlaceController(placeService);

			// Act
			// controller.Put(5, "value");

			// Assert
		}

        [Test]
        public void Delete()
        {
			// Arrange
			IPlaceService placeService = new PlaceTestService();
			PlaceController controller = new PlaceController(placeService);

			// Act
			controller.Delete(5);

            // Assert
        }*/
    }
}
