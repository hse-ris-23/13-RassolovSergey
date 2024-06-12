using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ClassLibrary13;
using ClassLibraryLab10;

namespace ClassLibrary12
{
    // Определение класса MyObservableCollection<T>
    public class MyObservableCollection<T> : MyCollectionTree<T> where T : IInit, ICloneable, IComparable, ISummable, new()
    {
        // Событие для уведомления об изменении коллекции
        public event CollectionHandler? CollectionChanged;

        // Событие для уведомления об изменении элемента
        public event EventHandler<int>? ElementChanged;

        // События для уведомления об изменении количества элементов и изменения ссылки
        public event CollectionHandler? CollectionCountChanged;
        public event CollectionHandler? CollectionReferenceChanged;

        // Журнал для записи изменений
        public Journal Journal { get; set; }

        // Конструктор по умолчанию
        public MyObservableCollection() : base()
        {
            Journal = new Journal();
        }

        // Конструктор с начальным числом элементов
        public MyObservableCollection(int data) : base(data)
        {
            Journal = new Journal();
        }

        // Конструктор с массивом элементов
        public MyObservableCollection(T[] collection) : base(collection)
        {
            Journal = new Journal();
        }

        // Переопределение метода добавления элемента с уведомлением
        public new void Add(T item)
        {
            base.Add(item);
            OnCollectionChanged(new CollectionHandlerEventArgs("Add", item));
            OnCollectionCountChanged(new CollectionHandlerEventArgs("Added", item));
        }

        // Переопределение метода удаления элемента с уведомлением
        public new bool Remove(T item)
        {
            bool removed = base.Remove(item);
            if (removed)
            {
                OnCollectionChanged(new CollectionHandlerEventArgs("Remove", item));
                OnCollectionCountChanged(new CollectionHandlerEventArgs("Removed", item));
            }
            return removed;
        }

        // Переопределение метода очистки коллекции с уведомлением
        public new void Clear()
        {
            base.Clear();
            OnCollectionChanged(new CollectionHandlerEventArgs("Clear", null));
        }

        // Индексатор с уведомлением об изменении элемента
        public new T this[int index]
        {
            get
            {
                return GetElementAtIndex(index);
            }
            set
            {
                var oldValue = GetElementAtIndex(index);
                SetElementAtIndex(index, value);
                OnElementChanged(index, oldValue, value);
                OnCollectionReferenceChanged(new CollectionHandlerEventArgs("Replaced", new { Index = index, NewValue = value }));
            }
        }

        // Метод для вызова события CollectionChanged
        protected virtual void OnCollectionChanged(CollectionHandlerEventArgs e)
        {
            CollectionChanged?.Invoke(this, e);
            Journal.AddEntry(new JournalEntry(nameof(MyObservableCollection<T>), e.ChangeType, e.ChangedItem?.ToString() ?? "null"));
        }

        // Метод для вызова события ElementChanged
        protected virtual void OnElementChanged(int index, T oldValue, T newValue)
        {
            ElementChanged?.Invoke(this, index);
            OnCollectionChanged(new CollectionHandlerEventArgs("Replace", new { Index = index, OldValue = oldValue, NewValue = newValue }));
        }

        // Метод для вызова события CollectionCountChanged
        protected virtual void OnCollectionCountChanged(CollectionHandlerEventArgs e)
        {
            CollectionCountChanged?.Invoke(this, e);
            Journal.AddEntry(new JournalEntry(nameof(MyObservableCollection<T>), e.ChangeType, e.ChangedItem?.ToString() ?? "null"));
        }

        // Метод для вызова события CollectionReferenceChanged
        protected virtual void OnCollectionReferenceChanged(CollectionHandlerEventArgs e)
        {
            CollectionReferenceChanged?.Invoke(this, e);
            Journal.AddEntry(new JournalEntry(nameof(MyObservableCollection<T>), e.ChangeType, e.ChangedItem?.ToString() ?? "null"));
        }

        // Вспомогательный метод для получения элемента по индексу
        private T GetElementAtIndex(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс находится вне допустимого диапазона.");
            }

            int currentIndex = 0;
            foreach (var item in this)
            {
                if (currentIndex == index)
                {
                    return item;
                }
                currentIndex++;
            }

            throw new InvalidOperationException("Элемент с указанным индексом не найден.");
        }

        // Вспомогательный метод для установки элемента по индексу
        private void SetElementAtIndex(int index, T value)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс находится вне допустимого диапазона.");
            }

            int currentIndex = 0;
            TreePoint<T> node = root;
            Stack<TreePoint<T>> stack = new Stack<TreePoint<T>>();

            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }

                node = stack.Pop();

                if (currentIndex == index)
                {
                    node.Data = value;
                    return;
                }

                currentIndex++;
                node = node.Right;
            }

            throw new InvalidOperationException("Элемент с указанным индексом не найден.");
        }
    }
}