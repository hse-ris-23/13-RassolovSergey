using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary13
{
    // Класс JournalEntry для записи информации о событии
    public class JournalEntry
    {
        // Свойство с названием коллекции, в которой произошло событие
        public string CollectionName { get; set; }

        // Свойство с информацией о типе изменений в коллекции
        public string ChangeType { get; set; }

        // Свойство с данными объекта, с которым связаны изменения
        public string ObjectData { get; set; }

        // Конструктор для инициализации полей класса
        public JournalEntry(string collectionName, string changeType, string objectData)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ObjectData = objectData;
        }

        // Перегруженная версия метода ToString
        public override string ToString()
        {
            return $"Коллекция: {CollectionName}, Тип изменения: {ChangeType}, Данные объекта: {ObjectData}";
        }
    }

}
