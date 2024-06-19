using ClassLibrary12;
using ClassLibrary13;
using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabWork13
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection1 = new MyObservableCollection<Card>("Collection 1");
            var collection2 = new MyObservableCollection<Card>("Collection 2");

            var journal1 = new Journal();
            var journal2 = new Journal();

            collection1.CountChanged += journal1.OnCountChanged;
            collection1.ReferenceChanged += journal1.OnReferenceChanged;

            collection2.CountChanged += journal2.OnCountChanged;
            collection2.ReferenceChanged += journal2.OnReferenceChanged;

            bool running = true;
            while (running)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Работа с коллекциями");
                Console.WriteLine("2. Работа с журналами");
                Console.WriteLine("0. Выход из программы");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        WorkWithCollectionsMenu(collection1, collection2);
                        break;
                    case "2":
                        WorkWithJournalsMenu(journal1, journal2);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void WorkWithCollectionsMenu(MyObservableCollection<Card> collection1, MyObservableCollection<Card> collection2)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Меню работы с коллекциями:");
                Console.WriteLine("1. Коллекция №1");
                Console.WriteLine("2. Коллекция №2");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CollectionMenu(collection1);
                        break;
                    case "2":
                        CollectionMenu(collection2);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void CollectionMenu(MyObservableCollection<Card> collection)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine($"Меню коллекции {collection.Name}:");
                Console.WriteLine("1. Добавить элемент в коллекцию");
                Console.WriteLine("2. Изменить элемент в коллекции");
                Console.WriteLine("3. Удалить элемент из коллекции");
                Console.WriteLine("4. Напечатать коллекцию");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCardToCollection(collection);
                        break;
                    case "2":
                        UpdateCardInCollection(collection);
                        break;
                    case "3":
                        RemoveCardFromCollection(collection);
                        break;
                    case "4":
                        PrintCollection(collection);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void WorkWithJournalsMenu(Journal journal1, Journal journal2)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Меню работы с журналами:");
                Console.WriteLine("1. Вывести информацию из журнала №1");
                Console.WriteLine("2. Вывести информацию из журнала №2");
                Console.WriteLine("0. Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintJournal(journal1);
                        break;
                    case "2":
                        PrintJournal(journal2);
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void AddCardToCollection(MyObservableCollection<Card> collection)
        {
            Console.WriteLine("1. Инициализировать вручную");
            Console.WriteLine("2. Инициализировать случайным образом");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            Card newCard = new Card();
            if (choice == "1")
            {
                newCard.Init();
            }
            else
            {
                newCard.RandomInit();
            }

            collection.Add(newCard);
            Console.WriteLine("Элемент добавлен.");
        }

        static void RemoveCardFromCollection(MyObservableCollection<Card> collection)
        {
            if (collection.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
            }
            else
            {
                Console.WriteLine("Введите данные карты для удаления:");
                Card cardToRemove = new Card();
                cardToRemove.Init();

                var foundCard = collection.FirstOrDefault(card =>
                    card.Id == cardToRemove.Id &&
                    card.Name == cardToRemove.Name &&
                    card.Time == cardToRemove.Time);

                if (foundCard != null)
                {
                    collection.Remove(foundCard);
                    Console.WriteLine("Элемент удален.");
                }
                else
                {
                    Console.WriteLine("Элемент не найден.");
                }
            }
        }

        static void UpdateCardInCollection(MyObservableCollection<Card> collection)
        {
            if (collection.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
            }
            else
            {
                Console.WriteLine("Введите данные карты для изменения:");
                Card cardToUpdate = new Card();
                cardToUpdate.Init();

                var foundCard = collection.FirstOrDefault(card =>
                    card.Id == cardToUpdate.Id &&
                    card.Name == cardToUpdate.Name &&
                    card.Time == cardToUpdate.Time);

                if (foundCard != null)
                {
                    Console.WriteLine("Введите новые данные для карты:");
                    Card newCardData = new Card();
                    newCardData.Init();

                    int index = collection.IndexOf(foundCard);
                    collection[index] = newCardData;
                    Console.WriteLine("Элемент изменен.");
                }
                else
                {
                    Console.WriteLine("Элемент не найден.");
                }
            }
        }

        static void PrintCollection(MyObservableCollection<Card> collection)
        {
            if (collection.Count == 0)
            {
                Console.WriteLine($"Коллекция {collection.Name} пуста.");
            }
            else
            {
                Console.WriteLine($"Содержимое коллекции {collection.Name}:");
                foreach (var card in collection)
                {
                    Console.WriteLine(card);
                }
            }
        }

        static void PrintJournal(Journal journal)
        {
            if (journal.GetEntriesCount() == 0)
            {
                Console.WriteLine("Ваш журнал пуст!");
            }
            else
            {
                Console.WriteLine(journal.ToString());
            }
        }
    }
}
