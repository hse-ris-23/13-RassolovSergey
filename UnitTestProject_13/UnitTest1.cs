using ClassLibrary13;
using ClassLibrary12;
using ClassLibraryLab10;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject_13
{
    [TestClass]
    public class JournalEntryTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            // Arrange
            string collectionName = "MyCollection";
            string changeType = "Add";
            Card card = new Card("0000 0000 0000 0000", "Test User", "01/25", 123);

            // Act
            JournalEntry entry = new JournalEntry(collectionName, changeType, card.ToString());

            // Assert
            Assert.IsNotNull(entry);
            Assert.AreEqual(collectionName, entry.CollectionName);
            Assert.AreEqual(changeType, entry.ChangeType);
            Assert.AreEqual(card.ToString(), entry.ObjectData);
        }

        [TestMethod]
        public void TestToStringMethod()
        {
            // Arrange
            string collectionName = "AnotherCollection";
            string changeType = "Remove";
            Card card = new Card("1111 1111 1111 1111", "Another User", "02/26", 456);
            JournalEntry entry = new JournalEntry(collectionName, changeType, card.ToString());

            // Act
            string entryString = entry.ToString();

            // Assert
            string expectedString = $"Коллекция: {collectionName}, Тип изменения: {changeType}, Данные объекта: {card.ToString()}";
            Assert.AreEqual(expectedString, entryString);
        }
    }

    [TestClass]
    public class CollectionHandlerEventArgsTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            // Arrange
            string changeType = "Add";
            Card card = new Card("0000 0000 0000 0000", "Test User", "01/25", 123);

            // Act
            CollectionHandlerEventArgs args = new CollectionHandlerEventArgs(changeType, card);

            // Assert
            Assert.IsNotNull(args);
            Assert.AreEqual(changeType, args.ChangeType);
            Assert.AreEqual(card, args.ChangedItem);
        }

        [TestMethod]
        public void TestProperties()
        {
            // Arrange
            string changeType = "Remove";
            Card card = new Card("1111 1111 1111 1111", "Another User", "02/26", 456);
            CollectionHandlerEventArgs args = new CollectionHandlerEventArgs();

            // Act
            args.ChangeType = changeType;
            args.ChangedItem = card;

            // Assert
            Assert.AreEqual(changeType, args.ChangeType);
            Assert.AreEqual(card, args.ChangedItem);
        }
    }
    [TestClass]
    public class MyObservableCollectionTests
    {
        [TestMethod]
        public void TestAdd()
        {
            // Arrange
            MyObservableCollection<Card> collection = new MyObservableCollection<Card>();
            Card card = new Card("0000 0000 0000 0000", "Test User", "01/25", 123);

            // Act
            collection.Add(card);

            // Assert
            Assert.AreEqual(1, collection.Count);
            Assert.IsTrue(collection.Contains(card));
        }

        [TestMethod]
        public void TestRemove()
        {
            // Arrange
            MyObservableCollection<Card> collection = new MyObservableCollection<Card>();
            Card card = new Card("1111 1111 1111 1111", "Another User", "02/26", 456);
            collection.Add(card);

            // Act
            bool removed = collection.Remove(card);

            // Assert
            Assert.IsTrue(removed);
            Assert.AreEqual(0, collection.Count);
            Assert.IsFalse(collection.Contains(card));
        }

        [TestMethod]
        public void TestIndexer()
        {
            // Arrange
            MyObservableCollection<Card> collection = new MyObservableCollection<Card>();
            Card card1 = new Card("2222 2222 2222 2222", "User One", "03/27", 789);
            Card card2 = new Card("3333 3333 3333 3333", "User Two", "04/28", 999);
            collection.Add(card1);
            collection.Add(card2);

            // Act
            collection[1] = new Card("4444 4444 4444 4444", "Updated User", "05/29", 111);

            // Assert
            Assert.AreEqual(2, collection.Count);
            Assert.AreEqual("4444 4444 4444 4444", collection[1].Id);
            Assert.AreEqual("Updated User", collection[1].Name);
        }

        [TestMethod]
        public void TestClear()
        {
            // Arrange
            MyObservableCollection<Card> collection = new MyObservableCollection<Card>();
            collection.Add(new Card("5555 5555 5555 5555", "User Three", "06/30", 222));
            collection.Add(new Card("6666 6666 6666 6666", "User Four", "07/31", 333));

            // Act
            collection.Clear();

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void TestEvents()
        {
            // Arrange
            MyObservableCollection<Card> collection = new MyObservableCollection<Card>();
            Card card = new Card("7777 7777 7777 7777", "User Five", "08/32", 444);
            bool collectionChangedEventRaised = false;
            bool elementChangedEventRaised = false;
            bool collectionCountChangedEventRaised = false;
            bool collectionReferenceChangedEventRaised = false;

            collection.CollectionChanged += (sender, args) => { collectionChangedEventRaised = true; };
            collection.ElementChanged += (sender, index) => { elementChangedEventRaised = true; };
            collection.CollectionCountChanged += (sender, args) => { collectionCountChangedEventRaised = true; };
            collection.CollectionReferenceChanged += (sender, args) => { collectionReferenceChangedEventRaised = true; };

            // Act
            collection.Add(card);
            collection.Remove(card);
            collection[0] = new Card("8888 8888 8888 8888", "Updated User", "09/33", 555);
            collection.Clear();

            // Assert
            Assert.IsTrue(collectionChangedEventRaised);
            Assert.IsTrue(elementChangedEventRaised);
            Assert.IsTrue(collectionCountChangedEventRaised);
            Assert.IsTrue(collectionReferenceChangedEventRaised);
        }
    }

    [TestClass]
    public class JournalTests
    {
        [TestMethod]
        public void TestAddEntry()
        {
            // Arrange
            Journal journal = new Journal();
            JournalEntry entry = new JournalEntry("TestCollection", "Add", "TestObject");

            // Act
            journal.AddEntry(entry);

            // Assert
            Assert.AreEqual(1, journal.GetEntriesCount());
        }

        [TestMethod]
        public void TestToString()
        {
            // Arrange
            Journal journal = new Journal();
            JournalEntry entry1 = new JournalEntry("Collection1", "Add", "Object1");
            JournalEntry entry2 = new JournalEntry("Collection2", "Remove", "Object2");
            journal.AddEntry(entry1);
            journal.AddEntry(entry2);

            // Act
            string result = journal.ToString();

            // Assert
            string expected = "Содержимое журнала:\n" +
                              "Коллекция: Collection1, Тип изменения: Add, Данные объекта: Object1\n" +
                              "Коллекция: Collection2, Тип изменения: Remove, Данные объекта: Object2\n";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestAddMultipleEntries()
        {
            // Arrange
            Journal journal = new Journal();
            JournalEntry entry1 = new JournalEntry("Collection1", "Add", "Object1");
            JournalEntry entry2 = new JournalEntry("Collection1", "Remove", "Object2");

            // Act
            journal.AddEntry(entry1);
            journal.AddEntry(entry2);

            // Assert
            Assert.AreEqual(2, journal.GetEntriesCount());
        }

        [TestMethod]
        public void TestEmptyJournalToString()
        {
            // Arrange
            Journal journal = new Journal();

            // Act
            string result = journal.ToString();

            // Assert
            Assert.AreEqual("Содержимое журнала:\n", result);
        }
    }
}
