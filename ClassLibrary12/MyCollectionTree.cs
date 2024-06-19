using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace ClassLibrary12
{
    public class MyCollectionTree<T> : MyTree<T>, IEnumerable<T>, ICollection<T> where T : IInit, ICloneable, IComparable, ISummable, new()
    {
        public MyCollectionTree() : base() { }

        public MyCollectionTree(int data) : base(data) { }

        public MyCollectionTree(T[] collection) : base(collection) { }

        // Реализация интерфейса - IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            return new MyTreeEnumerator(this);
        }

        // Реализация интерфейса - IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Реализация интерфейса - ICollection<T>
        public int Count => base.Count;

        public bool IsReadOnly => false;

        // Добавление эл. коллекции
        public void Add(T item)
        {
            base.Add(item);
        }

        // Очистка коллекции
        public void Clear()
        {
            base.Clear();
        }

        public bool Contains(T item)
        {
            return base.Find(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentException("Ваш массив пуст!");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Индекс массива должен быть неотрицательным");
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Данный массив недостаточно велик для размещения элементов");

            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }

        // Удаление эл. коллекции
        public bool Remove(T item)
        {
            return base.RemoveISBD(item);
        }

        // Внутренний класс для реализации IEnumerator<T>
        private class MyTreeEnumerator : IEnumerator<T>
        {
            private TreePoint<T>? root;
            private TreePoint<T>? current;
            private Stack<TreePoint<T>> stack;

            public MyTreeEnumerator(MyCollectionTree<T> collection)
            {
                root = collection.root;
                current = null;
                stack = new Stack<TreePoint<T>>();
            }

            public T Current => current!.Data;

            object? IEnumerator.Current => Current;

            public void Dispose()
            {
                // Оставляем пустым
            }

            public bool MoveNext()
            {
                if (stack.Count == 0 && root != null && current == null)
                {
                    stack.Push(root);
                }

                while (stack.Count > 0)
                {
                    var node = stack.Pop();
                    if (node != null)
                    {
                        current = node;
                        stack.Push(node.Right);
                        stack.Push(node.Left);
                        return true;
                    }
                }

                return false;
            }

            public void Reset()
            {
                current = null;
                stack.Clear();
                if (root != null)
                {
                    stack.Push(root);
                }
            }
        }

        // Индексатор для доступа к элементам коллекции
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне допустимого диапазона.");
                }
                return GetElementAtIndex(root, index);
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне допустимого диапазона.");
                }
                SetElementAtIndex(root, index, value);
            }
        }

        private T GetElementAtIndex(TreePoint<T> node, int index)
        {
            var elements = new List<T>();
            InOrderTraversal(node, elements);
            return elements[index];
        }

        private void SetElementAtIndex(TreePoint<T> node, int index, T value)
        {
            var elements = new List<TreePoint<T>>();
            InOrderTraversal(node, elements);
            elements[index].Data = value;
        }

        private void InOrderTraversal(TreePoint<T> node, List<T> elements)
        {
            if (node == null) return;
            InOrderTraversal(node.Left, elements);
            elements.Add(node.Data);
            InOrderTraversal(node.Right, elements);
        }

        private void InOrderTraversal(TreePoint<T> node, List<TreePoint<T>> elements)
        {
            if (node == null) return;
            InOrderTraversal(node.Left, elements);
            elements.Add(node);
            InOrderTraversal(node.Right, elements);
        }

        // Свойство Length (только для чтения)
        public int Length => Count;
    }
}
