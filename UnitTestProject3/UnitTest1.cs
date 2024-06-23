using ClassLibraryLab10;
using Lab12_4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VehicleExtensionsTests
{
    [TestClass]
    public class VehicleExtensionsTests
    {
        private List<Vehicle> vehicles;
        private Queue<List<Vehicle>> vehicleQueue;
        private List<Lab12_4.Group> groups;

        [TestInitialize]
        public void Setup()
        {
            // Инициализация данных для тестирования
            vehicles = new List<Vehicle>
            {
                new OffRoad { Brand = "Toyota", Price = 30000, AllWD = true, GroundType = "Sand" },
                new OffRoad { Brand = "Land Rover", Price = 60000, AllWD = false, GroundType = "Rock" },
                // Добавить другие транспортные средства
            };

            vehicleQueue = new Queue<List<Vehicle>>();
            vehicleQueue.Enqueue(vehicles);

            groups = new List<Lab12_4.Group>
            {
                new Lab12_4.Group("Toyota", "Japan"),
                new Lab12_4.Group("Land Rover", "UK"),
                // Добавить другие группы
            };
        }

        [TestMethod]
        public void Flatten_ShouldReturnFlattenedCollection()
        {
            // Act
            var result = vehicleQueue.Flatten();

            // Assert
            Assert.AreEqual(vehicles.Count, result.Count());
            Assert.IsTrue(vehicles.SequenceEqual(result));
        }

        [TestMethod]
        public void SelectOffRoadVehicles_ShouldReturnOffRoadInfo()
        {
            // Act
            var result = vehicles.SelectOffRoadVehicles().ToList();

            // Assert
            Assert.AreEqual(vehicles.OfType<OffRoad>().Count(), result.Count);
            foreach (var offRoad in result)
            {
                Assert.IsNotNull(offRoad.Name);
                Assert.IsNotNull(offRoad.Price);
                Assert.IsNotNull(offRoad.GroundType);
            }
        }

        [TestMethod]
        public void SelectVehiclesWithPriceQualityRatio_ShouldReturnVehiclesWithCalculatedRatio()
        {
            // Act
            var result = vehicles.SelectVehiclesWithPriceQualityRatio().ToList();

            // Assert
            Assert.AreEqual(vehicles.OfType<OffRoad>().Count(), result.Count);
            foreach (var vehicle in result)
            {
                Assert.IsTrue(vehicle.PriceQualityRatio > 0);
            }
        }

        [TestMethod]
        public void GroupByGroundType_ShouldReturnGroupedVehicles()
        {
            // Act
            var result = vehicles.GroupByGroundType().ToList();

            // Assert
            Assert.AreEqual(vehicles.OfType<OffRoad>().Select(v => v.GroundType).Distinct().Count(), result.Count);
            foreach (var group in result)
            {
                Assert.AreEqual(vehicles.OfType<OffRoad>().Count(v => v.GroundType == group.Key), group.Count());
            }
        }

        [TestMethod]
        public void JoinWithGroups_ShouldReturnJoinedCollection()
        {
            // Act
            var result = vehicles.JoinWithGroups((IEnumerable<Lab12_4.Group>)groups).ToList();

            // Assert
            Assert.AreEqual(vehicles.Count, result.Count);
            foreach (var vehicleGroupInfo in result)
            {
                Assert.IsNotNull(vehicleGroupInfo.Name);
                Assert.IsNotNull(vehicleGroupInfo.Country);
                Assert.IsTrue(vehicleGroupInfo.ID > 0);
            }
        }
    }
}