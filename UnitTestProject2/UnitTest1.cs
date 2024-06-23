using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lab12_4;
using ClassLibraryLab10;
using System.Collections.ObjectModel;

namespace UnitTestProject2
{
    [TestClass]
    public class MyObservableCollectionTests
    {
        private MyObservableCollection<Vehicle> collection;

        [TestInitialize]
        public void Initialize()
        {
            // Инициализация коллекции перед каждым тестом
            collection = new MyObservableCollection<Vehicle>();
        }

        [TestMethod]
        public void Add_Item_Should_Trigger_CollectionCountChanged()
        {
            // Arrange
            var vehicle = new Vehicle();
            bool eventFired = false;
            collection.CollectionCountChanged += (sender, args) => eventFired = true;

            // Act
            collection.Add(vehicle);

            // Assert
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void Remove_Item_Should_Trigger_CollectionCountChanged()
        {
            // Arrange
            var vehicle = new Vehicle();
            collection.Add(vehicle);
            bool eventFired = false;
            collection.CollectionCountChanged += (sender, args) => eventFired = true;

            // Act
            collection.Remove(vehicle);

            // Assert
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void Set_Item_Should_Trigger_CollectionReferenceChanged()
        {
            // Arrange
            var vehicle = new Vehicle();
            collection.Add(vehicle);
            bool eventFired = false;
            collection.CollectionReferenceChanged += (sender, args) => eventFired = true;

            // Act
            collection[0] = new Vehicle();

            // Assert
            Assert.IsTrue(eventFired);
        }

        [TestMethod]
        public void Collection_Should_Be_Empty_After_Clear()
        {
            // Arrange
            collection.Add(new Vehicle());
            collection.Add(new Vehicle());

            // Act
            collection.Clear();

            // Assert
            Assert.AreEqual(0, collection.Length);
        }

    }
}
