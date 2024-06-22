using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab12_4
{
    internal class MyCollection<T> : MyTree<T>, ICollection<T>, IEnumerable<T> where T : IInit, IComparable, ICloneable, new()
    {
        // Конструктор
        public MyCollection() : base() {}
        public MyCollection(int length) : base(length) {}
        public MyCollection(T[] collection)
        {
            CopyTo(collection, 0);
        }


        // Реализация интерфейса ICollection<T>
        public bool IsReadOnly => false;

        public int Count => count;

        public void Add(T item)
        {
            // Добавление элемента в дерево
            AddPoint(item);
        }

        public void Clear()
        {
            // Очистка дерева
            root = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            // Проверка наличия элемента в дереве
            // Это упрощенная реализация, которая требует полного обхода дерева
            Point<T>? current = root;
            while (current != null)
            {
                if (current.Data.CompareTo(item) == 0)
                    return true;
                else if (current.Data.CompareTo(item) > 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            // Копирование элементов дерева в массив
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < count)
                throw new ArgumentException("Недостаточно места в целевом массиве");

            int currentIndex = arrayIndex;
            foreach (T item in this)
            {
                array[currentIndex++] = item;
            }
        }

        public bool Remove(T item)
        {
            // Удаление элемента из дерева
            // Это упрощенная реализация, которая не учитывает перебалансировку дерева
            Point<T>? current = root, parent = null;
            int result;
            while (current != null)
            {
                result = current.Data.CompareTo(item);
                if (result == 0)
                {
                    // Элемент найден
                    if (current.Left == null || current.Right == null)
                    {
                        Point<T>? newChild = current.Left ?? current.Right;
                        if (parent == null)
                            root = newChild;
                        else if (parent.Left == current)
                            parent.Left = newChild;
                        else
                            parent.Right = newChild;
                    }
                    else
                    {
                        // У элемента есть оба потомка
                        Point<T>? successor = current.Right;
                        while (successor.Left != null)
                            successor = successor.Left;
                        T successorData = successor.Data;
                        Remove(successorData);
                        current.Data = successorData;
                    }
                    count--;
                    return true;
                }
                parent = current;
                if (result > 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return false;
        }

        // Реализация интерфейса IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            // Возвращение перечислителя для обхода дерева
            return InOrderTraversal(root).GetEnumerator();
        }

        private IEnumerable<T> InOrderTraversal(Point<T>? point)
        {
            if (point != null)
            {
                foreach (var left in InOrderTraversal(point.Left))
                    yield return left;

                yield return point.Data;

                foreach (var right in InOrderTraversal(point.Right))
                    yield return right;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // Возвращение перечислителя для обхода дерева (необобщенная версия)
            return GetEnumerator();
        }
    }
}