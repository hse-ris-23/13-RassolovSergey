using ClassLibrary12;
using ClassLibraryLab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary13
{
    // Класс Journal для хранения информации об изменениях в коллекции
    public class Journal
    {
        // Список объектов JournalEntry
        private List<JournalEntry> entries = new List<JournalEntry>();

        // Конструктор по умолчанию
        public Journal()
        {
            entries = new List<JournalEntry>();
        }

        // Метод для добавления записи в журнал
        public void AddEntry(JournalEntry entry)
        {
            entries.Add(entry);
        }

        // Метод для получения всех записей журнала
        public int GetEntriesCount()
        {
            return entries.Count;
        }

        // Метод-обработчик события CountChanged
        public void OnCountChanged(object source, CollectionHandlerEventArgs args)
        {
            // Получение имени коллекции из источника события
            string collectionName = ((MyObservableCollection<Card>)source).Name;

            // Создание новой записи журнала и добавление её в список
            AddEntry(new JournalEntry(collectionName, args.ChangeType, args.ChangedItem.ToString()));
        }

        // Метод-обработчик события ReferenceChanged
        public void OnReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            // Получение имени коллекции из источника события
            string collectionName = ((MyObservableCollection<Card>)source).Name;

            // Создание новой записи журнала и добавление её в список
            AddEntry(new JournalEntry(collectionName, args.ChangeType, args.ChangedItem.ToString()));
        }


        // Перегруженная версия метода ToString для вывода всех записей журнала
        public override string ToString()
        {
            // Инициализация строки для хранения результата
            string result = "Содержимое журнала:\n";

            // Проход по всем записям в списке и добавление их к строке результата
            foreach (var entry in entries)
            {
                result += entry.ToString() + "\n";
            }

            // Возвращение результирующей строки
            return result;
        }
    }
}
