using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary13
{
    // Определение класса CollectionHandlerEventArgs
    public class CollectionHandlerEventArgs : EventArgs
    {
        // Свойство типа string с информацией о типе изменений в коллекции
        public string ChangeType { get; set; }

        // Свойство для ссылки на объект, с которым связаны изменения
        public object ChangedItem { get; set; }

        // Конструктор по умолчанию
        public CollectionHandlerEventArgs() { }

        // Конструктор для инициализации свойства ChangeType
        public CollectionHandlerEventArgs(string changeType)
        {
            ChangeType = changeType;
        }

        // Конструктор для инициализации свойства ChangedItem
        public CollectionHandlerEventArgs(object changedItem)
        {
            ChangedItem = changedItem;
        }

        // Конструктор с параметрами для инициализации свойств
        public CollectionHandlerEventArgs(string changeType, object changedItem)
        {
            ChangeType = changeType;
            ChangedItem = changedItem;
        }
    }
}