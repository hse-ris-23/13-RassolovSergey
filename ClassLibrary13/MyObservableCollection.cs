using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ClassLibrary13;
using ClassLibraryLab10;

namespace ClassLibrary12
{
    // Определение делегата для обработки событий
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class MyObservableCollection<T> : Collection<T> where T : IInit, ICloneable, new()
    {
        // Поле для хранения имени коллекции
        public string Name { get; private set; }

        // Конструкторы
        public MyObservableCollection(string name) : base()
        {
            Name = name;
        }

        public MyObservableCollection(string name, int size) : base(new T[size])
        {
            Name = name;
        }

        public MyObservableCollection(string name, T[] collection) : base(new List<T>(collection))
        {
            Name = name;
        }

        // События
        public event CollectionHandler CountChanged;
        public event CollectionHandler ReferenceChanged;

        // Метод для вызова события CountChanged
        protected virtual void OnCountChanged(object source, CollectionHandlerEventArgs args)
        {
            CountChanged?.Invoke(this, args);
        }

        // Метод для вызова события ReferenceChanged
        protected virtual void OnReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            ReferenceChanged?.Invoke(this, args);
        }

        // Переопределим метод Add для вызова события CountChanged
        public new void Add(T item)
        {
            base.Add(item);
            OnCountChanged(this, new CollectionHandlerEventArgs("добавление", item));
        }

        // Переопределим метод RemoveAt для вызова события CountChanged
        public new void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                var item = this[index];
                OnCountChanged(this, new CollectionHandlerEventArgs("удаление", item));
                base.RemoveAt(index);
            }
        }

        // Переопределим индексатор для вызова события ReferenceChanged
        public new T this[int index]
        {
            get => base[index];
            set
            {
                var oldItem = base[index];
                base[index] = value;
                OnReferenceChanged(this, new CollectionHandlerEventArgs("замена", oldItem));
            }
        }

        public new bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                OnCountChanged(this, new CollectionHandlerEventArgs("удаление", item));
                base.RemoveAt(index);
                return true;
            }
            return false;
        }
    }
}
