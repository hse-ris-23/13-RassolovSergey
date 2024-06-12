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
            // Создание двух коллекций
            MyObservableCollection<Card> collection1 = new MyObservableCollection<Card>();
            MyObservableCollection<Card> collection2 = new MyObservableCollection<Card>();

            // Создание двух объектов типа Journal
            Journal journal1 = new Journal();
            Journal journal2 = new Journal();

            // Подписка journal1 на события CollectionCountChanged и CollectionReferenceChanged из первой коллекции
            collection1.CollectionCountChanged += (sender, e) => journal1.AddEntry(new JournalEntry("Коллекция 1", e.ChangeType, e.ChangedItem.ToString()));
            collection1.CollectionReferenceChanged += (sender, e) => journal1.AddEntry(new JournalEntry("Коллекция 1", e.ChangeType, e.ChangedItem.ToString()));

            // Подписка journal2 на события CollectionReferenceChanged из обеих коллекций
            collection1.CollectionReferenceChanged += (sender, e) => journal2.AddEntry(new JournalEntry("Коллекция 1", e.ChangeType, e.ChangedItem.ToString()));
            collection2.CollectionReferenceChanged += (sender, e) => journal2.AddEntry(new JournalEntry("Коллекция 2", e.ChangeType, e.ChangedItem.ToString()));

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("======== Главное меню ========");
                Console.WriteLine("1. Работа с коллекцией 1");
                Console.WriteLine("2. Работа с коллекцией 2");
                Console.WriteLine("3. Показать журнал 1");
                Console.WriteLine("4. Показать журнал 2");
                Console.WriteLine("0. Выйти");
                Console.WriteLine("==============================");

                int choice = (int)InputHelper.InputUintNumber("Выбранное действие: \t");

                switch (choice)
                {
                    case 1:
                        CollectionMenu(collection1, "Коллекция 1");
                        break;
                    case 2:
                        CollectionMenu(collection2, "Коллекция 2");
                        break;
                    case 3:
                        Console.WriteLine(journal1);
                        break;
                    case 4:
                        Console.WriteLine(journal2);
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void CollectionMenu(MyObservableCollection<Card> collection, string collectionName)
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine($"========== {collectionName} меню ==========");
                Console.WriteLine("1. Добавить элемент");
                Console.WriteLine("2. Удалить элемент");
                Console.WriteLine("3. Изменить элемент");
                Console.WriteLine("0. Назад");
                Console.WriteLine("================================");

                int choice = (int)InputHelper.InputUintNumber("Выбранное действие: \t");

                switch (choice)
                {
                    case 1:
                        AddElement(collection);
                        break;
                    case 2:
                        RemoveElement(collection);
                        break;
                    case 3:
                        ModifyElement(collection);
                        break;
                    case 0:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        static void AddElement(MyObservableCollection<Card> collection)
        {
            Card newCard = new Card();
            newCard.Init();
            collection.Add(newCard);
            Console.WriteLine("Элемент добавлен.");
        }

        static void RemoveElement(MyObservableCollection<Card> collection)
        {
            Console.WriteLine("Введите индекс элемента для удаления:");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < collection.Count)
            {
                collection.Remove(collection[index]);
                Console.WriteLine("Элемент удален.");
            }
            else
            {
                Console.WriteLine("Неверный индекс.");
            }
        }

        static void ModifyElement(MyObservableCollection<Card> collection)
        {
            Console.WriteLine("Введите индекс элемента для изменения:");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < collection.Count)
            {
                Card newCard = new Card();
                newCard.Init();
                collection[index] = newCard;
                Console.WriteLine("Элемент изменен.");
            }
            else
            {
                Console.WriteLine("Неверный индекс.");
            }
        }
    }
}
