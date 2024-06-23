using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;
using Lab12;

namespace ConsoleApp3
{
    public class MyList<T> where T: IInit, ICloneable, new()
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
        //public Point<T> MakeRandomData()
        //{
        //    switch (rnd.Next(1,4))
        //    {
        //        case 1:
        //            T data = new T();
        //            data.RandomInit(rnd);
        //            return new Point<T>(data);
        //        case 2:
        //            PassengerCar pCar = new PassengerCar();
        //            pCar.RandomInit(rnd);
        //            return new Point<PassengerCar>(pCar) as Point<T>;
        //        case 3:
        //            OffRoad offRoad = new OffRoad();
        //            offRoad.RandomInit(rnd);
        //            return new Point<OffRoad>(offRoad) as Point<T>;
        //        case 4:
        //            Truck truck = new Truck();
        //            truck.RandomInit(rnd);
        //            return new Point<T>(truck);
        //    }
        //    return null;
        //}
        //public T MakeRandomItem()
        //{
        //    T data = new T();
        //    data.RandomInit(rnd);
        //    return data;
        //}
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
            while (pred.Next!=null)
            {
                Point<T> oddNumElem = MakeRandomData();
                oddNumElem.Pred = pred;
                oddNumElem.Next = pred.Next;
                pred.Next = oddNumElem;
                pred = oddNumElem.Next;
                pred.Pred = oddNumElem;
                count++;
            }
            newItem = MakeRandomItem();
            AddToEnd(newItem);
        }
        public MyList() { count = 0; }
        public MyList(int size) 
        {
            if (size <= 0) throw new Exception("size less zero");
            beg = MakeRandomData();
            end = beg;
            //for (int i = 1; i < size; i++)
            //{
            //    T newItem = MakeRandomItem();
            //    AddToEnd(newItem);
            //}
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
        //public MyList(int size)
        //{
        //    if (size <= 0) throw new Exception("size less zero");
        //    beg = MakeRandomData();
        //    end = beg;
        //    for (int i = 1; i < size; i++)
        //    {
        //        // Создаем экземпляр класса T
        //        T newItem = new T();
        //        Vehicle v = new Vehicle();
        //        v.RandomInit(rnd);
        //        newItem = (T)(object)v;
        //        AddToEnd(newItem);
        //    }
        //    count = size; //?
        //}
        public MyList(T[] collection)
        {
            if (collection == null)
                throw new Exception("empty collection: null");
            if (collection.Length == 0)
                throw new Exception("empty collection");
            T newData = (T)collection[0].Clone();
            beg = new Point<T> (newData);
            end = beg;
            count++;
            for(int i = 1; i<collection.Length; i++)
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
                Console.WriteLine($"{i+1}: {current}");
                current = current.Next;
            }
        }
        public Point<T>? FindItem(/*T item*/string brand, out int count)
        {
            count = 0;
            Point<T>? current = beg;
            while(current != null)
            {
                if (current.Data == null)
                    throw new Exception("Data is null");
                //if (current.Data.Equals(item))  //- полное сравнение
                //ниже - сравнение по полю Brand
                if ( current.Data.Equals(brand))
                    return current;
                current = current.Next;
                count++;
            }
            return null;
        }
        //public Point<T>? FindItem(int position)
        //{
        //    count = 0;
        //    Point<T>? current = beg;
        //    while(count != position)
        //    {
        //        if (current.Data == null)
        //            throw new Exception("Data is null");
        //        //if (current.Data.Equals(item))  //- полное сравнение
        //        //ниже - сравнение по полю Brand
        //        current = current.Next;
        //        count++;
        //    }
        //    return current;
        //}
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
            if (beg==end || pos.Pred == null)
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

        //public bool RemoveItem(T item) //удаление элемента (не нужно)
        //{
        //    if (beg == null)
        //        throw new Exception("the empty list");
        //    Point<T>? pos = FindItem(item);
        //    if (pos == null)
        //        return false;
        //    count--;
        //    //one elem
        //    if (beg==end)
        //    {
        //        beg = end = null;
        //        return true;
        //    }
        //    //the first
        //    if (pos.Pred == null)
        //    {
        //        beg = beg?.Next;
        //        beg.Pred = null;
        //        return true;
        //    }
        //    //the last
        //    if (pos.Next == null)
        //    {
        //        end = end.Pred;
        //        end.Next = null;
        //        return true;
        //    }
        //    Point<T> next = pos.Next;
        //    Point<T> pred = pos.Pred;
        //    pos.Next.Pred = pred;
        //    pos.Pred.Next = next;
        //    return true;
        //}
    }
}
