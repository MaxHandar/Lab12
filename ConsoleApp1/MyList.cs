using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace Lab12_4
{
    public class MyList<T> where T : IInit, ICloneable, new()
    {
        public Point<T>? beg = null;
        public Point<T>? end = null;
        Random rnd = new Random();

        int count = 0;
        public int Count => count;
        public Point<T> MakeRandomData()
        {
            T data = new T();
            data.RandomInit(rnd);
            return new Point<T>(data);
        }
        public T MakeRandomItem()
        {
            T data = new T();
            data.RandomInit(rnd);
            return data;
        }
        public void AddToBegin(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (beg != null)
            {
                beg.Pred = newItem;
                newItem.Next = beg;
                beg = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }
        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            if (end != null)
            {
                end.Next = newItem;
                newItem.Pred = end;
                end = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }
        public void AddOddNumElems() //добавить элементы 1,3,5 и т.д.
        {
            if (beg == null)
                throw new Exception("массив пуст");
            T newItem = MakeRandomItem();
            AddToBegin(newItem);
            Point<T> pred = beg.Next;
            while (pred.Next != null)
            {
                Point<T> oddNumElem = MakeRandomData();
                oddNumElem.Pred = pred;
                oddNumElem.Next = pred.Next;
                pred.Next = oddNumElem;
                pred = oddNumElem.Next;
                pred.Pred = oddNumElem;
            }
            newItem = MakeRandomItem();
            AddToEnd(newItem);
            count = (count - 2) * 2 + 1;
        }
        public MyList() { count = 0; }
        public MyList(int size)
        {
            if (size <= 0) throw new Exception("size less zero");
            beg = MakeRandomData();
            end = beg;
            for (int i = 1; i < size; i++)
            {
                // Создаем экземпляр класса T
                T newItem = new T();

                // Если T - это базовый класс, создаем экземпляр базового класса
                if (typeof(T) == typeof(Vehicle))
                {
                    // Создаем случайный тип (0 - Vehicle, 1 - OffRoad, 2 - PassengerCar)
                    int type = rnd.Next(4);

                    // Создаем экземпляр соответствующего класса
                    if (type == 0)
                    {
                        Vehicle v = new Vehicle();
                        v.RandomInit(rnd);
                        newItem = (T)(object)v;
                    }
                    else if (type == 1)
                    {
                        PassengerCar p = new PassengerCar();
                        p.RandomInit(rnd);
                        newItem = (T)(object)p;
                    }
                    else if (type == 2)
                    {
                        OffRoad o = new OffRoad();
                        o.RandomInit(rnd);
                        newItem = (T)(object)o;
                    }
                    else
                    {
                        Truck truck = new Truck();
                        truck.RandomInit(rnd);
                        newItem = (T)(object)truck;
                    }
                }
                // Если T - это класс OffRoad, создаем экземпляр OffRoad или его потомка
                else if (typeof(T) == typeof(PassengerCar))
                {
                    // Создаем случайный тип (0 - OffRoad, 1 - PassengerCar)
                    int type = rnd.Next(2);

                    // Создаем экземпляр соответствующего класса
                    if (type == 0)
                        newItem = (T)(object)new OffRoad();
                    else
                        newItem = (T)(object)new PassengerCar();
                }
                // Если T - это класс PassengerCar, создаем экземпляр PassengerCar
                else if (typeof(T) == typeof(OffRoad))
                {
                    newItem = (T)(object)new OffRoad();
                }

                else if (typeof(T) == typeof(Truck))
                {
                    newItem = (T)(object)new Truck();
                }

                // Добавляем новый элемент в список
                AddToEnd(newItem);
            }
            count = size; //?
        }
        public MyList(T[] collection)
        {
            if (collection == null)
                throw new Exception("empty collection: null");
            if (collection.Length == 0)
                throw new Exception("empty collection");
            T newData = (T)collection[0].Clone();
            beg = new Point<T>(newData);
            end = beg;
            for (int i = 1; i < collection.Length; i++)
            {
                AddToEnd(collection[i]);
            }
        }
        public void PrintList()
        {
            if (count == 0)
                Console.WriteLine("the list is empty");
            Point<T>? current = beg;
            for (int i = 0; current != null; i++)
            {
                Console.WriteLine($"{i + 1}: {current}");
                current = current.Next;
            }
        }
        public Point<T>? FindItem(string brand, out int count)
        {
            count = 0;
            Point<T>? current = beg;
            while (current != null)
            {
                if (current.Data == null)
                    throw new Exception("Data is null");
                //if (current.Data.Equals(item))  //- полное сравнение
                //ниже - сравнение по полю Brand
                if (current.Data.Equals(brand))
                    return current;
                current = current.Next;
                count++;
            }
            return null;
        }
        public MyList<T> ChangeDataOf1stElem()
        {
            beg = beg.Next;
            AddToBegin(MakeRandomItem());
            return this;
        }
        public bool RemoveItemsFromGiven(/*T item*/string brand)
        {
            if (beg == null)
                throw new Exception("the empty list");
            Point<T>? pos = FindItem(brand, out count);
            if (pos == null)
                return false;

            //one elem or first elem
            if (beg == end || pos.Pred == null)
            {
                beg = end = null;
                count = 0;
                return true;
            }
            //the last
            if (pos.Next == null)
            {
                end = end.Pred;
                end.Next = null;
                count--;
                return true;
            }
            //middle
            end = pos.Pred;
            end.Next = null;
            //count -= pos.count()
            return true;
        }
        public MyList<T> MakeCloneList()
        {
            MyList<T> clone = new MyList<T>();
            if (count <= 0)
                throw new Exception("size less zero");
            clone.beg = MakeRandomData();
            clone.end = clone.beg;
            Point<T>? current = beg.Next;
            clone.beg.Next = current;
            for (int i = 1; i < count; i++)
            {
                clone.AddToEnd((T)current.Data.Clone());
                current = current.Next;
            }
            clone.count = count;
            return clone;
        }

        public bool RemoveItem(string brand) //удаление элемента (не нужно)
        {
            if (beg == null)
                throw new Exception("the empty list");
            int count;
            Point<T>? pos = FindItem(brand, out count);
            if (pos == null)
                return false;
            count--;
            //one elem
            if (beg == end)
            {
                beg = end = null;
                return true;
            }
            //the first
            if (pos.Pred == null)
            {
                beg = beg?.Next;
                beg.Pred = null;
                return true;
            }
            //the last
            if (pos.Next == null)
            {
                end = end.Pred;
                end.Next = null;
                return true;
            }
            Point<T> next = pos.Next;
            Point<T> pred = pos.Pred;
            pos.Next.Pred = pred;
            pos.Pred.Next = next;
            return true;
        }
        public int IndexOf(T item)
        {
            Point<T>? current = beg;
            for (int i = 0; current != null; i++)
            {
                if (current.Data.Equals(item))
                    return i;
                current = current.Next;
            }
            return -1; // Если элемент не найден
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (index == 0)
            {
                AddToBegin(item);
                return;
            }
            Point<T>? current = beg;
            for (int i = 0; i < index - 1; i++) // Двигаемся до предыдущего элемента
                current = current.Next;
            Point<T> newItem = new Point<T>((T)item.Clone());
            newItem.Next = current.Next;
            newItem.Pred = current;
            current.Next = newItem;
            if (newItem.Next != null)
                newItem.Next.Pred = newItem;
            count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (index == 0)
            {
                beg = beg.Next;
                if (beg != null)
                    beg.Pred = null;
                count--;
                return;
            }
            Point<T>? current = beg;
            for (int i = 0; i < index - 1; i++)
                current = current.Next;
            Point<T>? toRemove = current.Next;
            current.Next = toRemove.Next;
            if (toRemove.Next != null)
                toRemove.Next.Pred = current;
            count--;
        }

        public void Add(T item)
        {
            AddToEnd(item);
        }

        public void Clear()
        {
            beg = null;
            end = null;
            count = 0;
        }

        public bool Contains(T item)
        {
            Point<T>? current = beg;
            while (current != null)
            {
                if (current.Data.Equals(item))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0 || arrayIndex >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < count)
                throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
            Point<T>? current = beg;
            for (int i = arrayIndex; current != null; i++)
            {
                array[i] = current.Data;
                current = current.Next;
            }
        }

        public bool Remove(T item)
        {
            Point<T>? current = beg;
            Point<T>? previous = null;
            while (current != null)
            {
                if ((current.Data).Equals(item))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next != null)
                            current.Next.Pred = previous;
                    }
                    else
                    {
                        beg = current.Next;
                        if (beg != null)
                            beg.Pred = null;
                    }
                    count--;
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                Point<T> current = beg;
                for (int i = 0; i < index; i++)
                    current = current.Next;
                return current.Data;
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                Point<T> current = beg;
                for (int i = 0; i < index; i++)
                    current = current.Next;
                current.Data = value;
            }
        }
    }
}
