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

        // Метод для добавления записи в журнал
        public void AddEntry(JournalEntry entry)
        {
            entries.Add(entry);
        }

        // Перегруженная версия метода ToString для вывода всех записей журнала
        public override string ToString()
        {
            string result = "Содержимое журнала:\n";
            foreach (var entry in entries)
            {
                result += entry.ToString() + "\n";
            }
            return result;
        }
    }
}
