using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryLab10;
using ConsoleApp3;
using Lab12_2;
using Lab12_3;
using Lab12_4;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class MyListTests
    {
        //List
        [TestMethod]
        public void AddToBegin_AddNewItem_CountIncreases()
        {
            // Arrange
            var myList = new ConsoleApp3.MyList<Vehicle>();
            int initialCount = myList.Count;
            var newItem = new Vehicle();

            // Actual
            myList.AddToBegin(newItem);
            int newCount = myList.Count;

            // Assert
            Assert.AreEqual(initialCount + 1, newCount);
        }

        [TestMethod]
        public void AddToEnd_AddNewItem_CountIncreases()
        {
            // Arrange
            var myList = new ConsoleApp3.MyList<Vehicle>();
            int initialCount = myList.Count;
            var newItem = new Vehicle();

            // Actual
            myList.AddToEnd(newItem);
            int newCount = myList.Count;

            // Assert
            Assert.AreEqual(initialCount + 1, newCount);
        }

        [TestMethod]
        public void MakeCloneList_CloneList_CountIsEqual()
        {
            // Arrange
            var originalList = new ConsoleApp3.MyList<Vehicle>();
            originalList.AddToBegin(new Vehicle());
            int originalCount = originalList.Count;

            // Actual
            var cloneList = originalList.MakeCloneList();
            int cloneCount = cloneList.Count;

            // Assert
            Assert.AreEqual(originalCount, cloneCount);
        }
        [TestMethod]
        public void AddOddNumElems_NonEmptyList_AddsElementsCorrectly()
        {
            // Arrange
            var myList = new ConsoleApp3.MyList<Vehicle>(5);
            myList.AddToBegin(new Vehicle()); // Добавляем начальный элемент, чтобы список не был пустым
            int initialCount = myList.Count;

            // Act
            myList.AddOddNumElems();

            // Assert
            Assert.AreEqual((initialCount) * 2 + 1, myList.Count);
        }

        [TestMethod]
        public void RemoveItemsFromGiven_RemoveExistingItem_ReturnsTrueAndCountDecreases()
        {
            // Arrange
            var myList = new ConsoleApp3.MyList<Vehicle>();
            var item = new Vehicle { Brand = "TestBrand" };
            myList.AddToBegin(item);
            int initialCount = myList.Count;

            // Actual
            bool result = myList.RemoveItemsFromGiven("TestBrand");
            int newCount = myList.Count;

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(initialCount - 1, newCount);
        }

        [TestMethod]
        public void FindItem_FindExistingItem_ReturnsNotNullAndCorrectCount()
        {
            // Arrange
            var myList = new ConsoleApp3.MyList<Vehicle>();
            var item = new Vehicle { Brand = "TestBrand" };
            myList.AddToBegin(item);
            int count;

            // Actual
            var foundItem = myList.FindItem("TestBrand", out count);

            // Assert
            Assert.IsNotNull(foundItem);
            Assert.AreEqual(0, count); // Assuming the item is at the beginning
        }
        [TestMethod]
        public void ChangeDataOf1stElem_NonEmptyList_ChangesData()
        {
            // Arrange
            var myList = new ConsoleApp3.MyList<Vehicle>(4);
            Vehicle oldData = myList.beg.Data;

            // Act
            myList.ChangeDataOf1stElem();
            Vehicle newData = myList.beg.Data;

            // Assert
            Assert.AreNotEqual(oldData, newData, "Данные первого элемента не изменились.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Конструктор не сгенерировал исключение на пустом массиве.")]
        public void Constructor_EmptyArray_ThrowsException()
        {
            // Arrange & Act & Assert
            var myList = new ConsoleApp3.MyList<Vehicle>(new Vehicle[] { });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Конструктор не сгенерировал исключение на null массиве.")]
        public void Constructor_NullArray_ThrowsException()
        {
            // Arrange & Act & Assert
            var myList = new ConsoleApp3.MyList<Vehicle>(null);
        }

        [TestMethod]
        public void Constructor_NonEmptyArray_CreatesList()
        {
            // Arrange
            Random rnd = new Random();
            Vehicle[] array = new Vehicle[3];
            array[0] = new Vehicle();
            array[1] = new Vehicle();
            array[2] = new Vehicle();
            array[0].RandomInit(rnd);
            array[1].RandomInit(rnd);
            array[2].RandomInit(rnd);

            // Act
            var myList = new ConsoleApp3.MyList<Vehicle>(array);

            // Assert
            Assert.AreEqual(array.Length, myList.Count, "Количество элементов в списке не соответствует размеру массива.");
        }
        //HashTable
        [TestMethod]
        public void AddPoint_ValidData_AddsDataToTable()
        {
            // Arrange
            var hashTable = new MyHashTable<Vehicle>(5);
            var offRoad = new OffRoad();
            offRoad.RandomInit(new Random());
            var vehicle = new Vehicle();
            var pCar = new PassengerCar();
            vehicle = (Vehicle)offRoad.Clone();
            pCar = (PassengerCar)offRoad.Clone();

            // Act
            hashTable.AddPoint(vehicle);
            hashTable.AddPoint(pCar);
            hashTable.AddPoint(offRoad);
            bool contains = hashTable.Contains(offRoad, out int index);
            bool isRemoved = hashTable.RemoveData(offRoad);
            bool containsAfter = hashTable.Contains(offRoad, out int index2);
            // Assert
            Assert.IsTrue(contains, "Элемент не был добавлен в хеш-таблицу.");
            Assert.IsFalse(containsAfter, "Элемент не был добавлен в хеш-таблицу.");
            Assert.IsTrue(isRemoved, "Элемент не был добавлен в хеш-таблицу.");
        }

        [TestMethod]
        public void Contains_DataExists_ReturnsTrue()
        {
            // Arrange
            var hashTable = new MyHashTable<Vehicle>(5);
            var vehicle = new Vehicle();
            vehicle.RandomInit(new Random());
            hashTable.AddPoint(vehicle);

            // Act
            bool contains = hashTable.Contains(vehicle, out int index);

            // Assert
            Assert.IsTrue(contains, "Метод Contains не нашел существующий элемент.");
        }

        [TestMethod]
        public void RemoveData_ValidData_RemovesDataFromTable()
        {
            // Arrange
            var hashTable = new MyHashTable<Vehicle>(5);
            var vehicle = new Vehicle();
            vehicle.RandomInit(new Random());
            hashTable.AddPoint(vehicle);

            // Act
            bool removed = hashTable.RemoveData(vehicle);
            bool containsAfterRemove = hashTable.Contains(vehicle, out int index);

            // Assert
            Assert.IsTrue(removed, "Элемент не был удален из хеш-таблицы.");
            Assert.IsFalse(containsAfterRemove, "Удаленный элемент все еще присутствует в хеш-таблице.");
        }

        //Tree
        private MyTree<Vehicle> tree;

        [TestInitialize]
        public void Initialize()
        {
            // Инициализация дерева с некоторым количеством элементов
            tree = new MyTree<Vehicle>(10);
        }

        [TestMethod]
        public void Tree_Count_IsCorrect()
        {
            // Arrange
            int expectedCount = 10;

            // Act
            int actualCount = tree.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualCount, "Количество элементов в дереве не соответствует ожидаемому.");
        }

        [TestMethod]
        public void AddPoint_IncreasesCount()
        {
            // Arrange
            var initialCount = tree.Count;
            var vehicle = new Vehicle(); // Замените Vehicle на соответствующий тип данных
            vehicle.RandomInit(new Random());

            // Act
            tree.AddPoint(vehicle);
            var newCount = tree.Count;

            // Assert
            Assert.AreEqual(initialCount + 1, newCount, "Метод AddPoint не увеличил количество элементов в дереве.");
        }

        [TestMethod]
        public void FindMax_ReturnsMaxElement()
        {
            // Arrange
            // Для этого теста необходимо определить, как сравнивать элементы

            // Act
            var maxElement = tree.FindMax();

            // Assert
            Assert.IsNotNull(maxElement, "Метод FindMax не вернул максимальный элемент.");
            // Assert.AreEqual(expectedMaxId, maxElement.Data.ID, "Метод FindMax не вернул ожидаемый максимальный элемент.");
        }

        [TestMethod]
        public void TransformToFindTree_CreatesValidTree()
        {
            // Arrange

            // Act
            var findTree = tree.TransformToFindTree();

            // Assert
            Assert.IsNotNull(findTree, "Метод TransformToFindTree не создал дерево поиска.");
        }
    }
    [TestClass]
    public class MyCollectionTests
    {
        //MyCollection
        private MyCollection<Vehicle> collection;

        [TestInitialize]
        public void InitializeCollection()
        {
            // Инициализация коллекции с некоторым количеством элементов
            collection = new MyCollection<Vehicle>();
            for (int i = 0; i < 5; i++)
            {
                collection.Add(new Vehicle());
            }
        }

        [TestMethod]
        public void Collection_Add_IncreasesCount()
        {
            // Arrange
            var initialCount = collection.Count;
            var vehicle = new Vehicle();
            vehicle.RandomInit(new Random());

            // Act
            collection.Add(vehicle);
            var newCount = collection.Count;

            // Assert
            Assert.AreEqual(initialCount + 1, newCount, "Метод Add не увеличил количество элементов в коллекции.");
        }

        [TestMethod]
        public void Collection_Contains_ReturnsTrueForExistingItem()
        {
            // Arrange
            var vehicle = new Vehicle();
            vehicle.RandomInit(new Random());
            collection.Add(vehicle);

            // Act
            bool contains = collection.Contains(vehicle);

            // Assert
            Assert.IsTrue(contains, "Метод Contains не нашел существующий элемент.");
        }

        [TestMethod]
        public void Collection_Remove_DecreasesCount()
        {
            // Arrange
            var vehicle = new Vehicle();
            vehicle.RandomInit(new Random());
            collection.Add(vehicle);
            var initialCount = collection.Count;

            // Act
            bool removed = collection.Remove(vehicle);
            var newCount = collection.Count;

            // Assert
            Assert.IsTrue(removed, "Метод Remove не удалил элемент.");
            Assert.AreEqual(initialCount - 1, newCount, "Метод Remove не уменьшил количество элементов в коллекции.");
        }

        [TestMethod]
        public void Collection_Indexer_RetrievesCorrectItem()
        {
            // Arrange
            var vehicle = new Vehicle();
            int index = 0;
            vehicle.RandomInit(new Random());
            collection.Add(vehicle);
            collection.Add(vehicle);
            collection.Add(vehicle);
            foreach (var item in collection)
            {
                index = collection.IndexOf(vehicle);
            }

            // Act
            var retrievedVehicle = collection[index];

            // Assert
            Assert.AreEqual(vehicle, retrievedVehicle, "Индексатор не вернул ожидаемый элемент.");
        }

        [TestMethod]
        public void Collection_Clear_ResetsCountToZero()
        {
            // Arrange

            // Act
            collection.Clear();
            var newCount = collection.Count;

            // Assert
            Assert.AreEqual(0, newCount, "Метод Clear не обнулил количество элементов в коллекции.");
        }
    }
}
