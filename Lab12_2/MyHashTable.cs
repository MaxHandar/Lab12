using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;
using Lab12_2;

namespace ConsoleApp3
{
    internal class MyHashTable<T> where T : IInit, ICloneable, new()
    {
        Random rnd = new Random();
        Point<T>?[] table;
        public int Capacity => table.Length;

        //конструктор
        public MyHashTable(int length = 10)
        {
            table = new Point<T>[length];
        }

        //public
        public void PrintTable()
        {
            for (int i = 0; i < table.Length; i++)
            {
                Console.WriteLine($"{i}: ");
                if (table[i] != null) //не пустая строка
                {
                    Console.WriteLine(table[i].Data); //info
                    if (table[i].Next != null) //не пустая цепочка
                    {
                        Point<T>? current = table[i].Next; //идем по цепочке
                        while (current != null)
                        {
                            Console.WriteLine(current.Data);
                            current = current.Next;
                        }
                    }
                }
            }
        }

        public void AddPoint(T data)
        {
            int index = GetIndex(data);
            //позиция пустая
            if (table[index] == null)
            {
                table[index] = new Point<T>(data);
                //table[index].Data = data;
            }
            else //есть цепочка
            {
                Point<T>? current = table[index];

                while (current.Next != null)
                {
                    if (current.Equals(data))
                        return;
                    current = current.Next;
                }
                current.Next = new Point<T>(data);
                current.Next.Pred = current;
            }
        }
        public Point<T> MakeRandomData()
        {
            T data = new T();

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
                    data = (T)(object)v;
                }
                else if (type == 1)
                {
                    PassengerCar p = new PassengerCar();
                    p.RandomInit(rnd);
                    data = (T)(object)p;
                }
                else if (type == 2)
                {
                    OffRoad o = new OffRoad();
                    o.RandomInit(rnd);
                    data = (T)(object)o;
                }
                else
                {
                    Truck truck = new Truck();
                    truck.RandomInit(rnd);
                    data = (T)(object)truck;
                }
            }
            return new Point<T>(data);
        }
        public void FillHashTableRand()
        {
            if (table == null || table.Length == 0)
                throw new Exception("empty table");
            int numOfElems = InputData.NumInput("Сколько элементов добавить?");
            for (int i = 0; i < numOfElems; i++)
            {
                T data = new T();
                //data.RandomInit(rnd);
                if (typeof(T) == typeof(Vehicle))
                {
                    // Создаем случайный тип (0 - Vehicle, 1 - OffRoad, 2 - PassengerCar)
                    int type = rnd.Next(4);

                    // Создаем экземпляр соответствующего класса
                    if (type == 0)
                    {
                        Vehicle v = new Vehicle();
                        v.RandomInit(rnd);
                        data = (T)(object)v;
                    }
                    else if (type == 1)
                    {
                        PassengerCar p = new PassengerCar();
                        p.RandomInit(rnd);
                        data = (T)(object)p;
                    }
                    else if (type == 2)
                    {
                        OffRoad o = new OffRoad();
                        o.RandomInit(rnd);
                        data = (T)(object)o;
                    }
                    else
                    {
                        Truck truck = new Truck();
                        truck.RandomInit(rnd);
                        data = (T)(object)truck;
                    }
                }
                AddPoint(data);
            }
        }
        //public void FillHashTableRand()
        //{
        //    if (table == null || table.Length == 0)
        //        throw new Exception("empty table");
        //    int numOfElems = InputData.NumInput("Сколько элементов добавить?");
        //    for (int i = 0; i < numOfElems; i++)
        //    {
        //        T data = new T();
        //        data.RandomInit(rnd);
        //        AddPoint(data);
        //    }
        //}

        public bool Contains(T data, out int index)
        {
            index = GetIndex(data);
            if (table == null)
            {
                throw new Exception("empty table");
            }
            if (table[index] == null) //цепочка пустая, элемента нет
            {
                return false;
            }
            if (table[index].Data.Equals(data)) //попали на нужный элемент
            {
                return true;
            }
            else
            {
                Point<T>? current = table[index]; //идем по цепочке
                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        return true;
                    }
                    current = current.Next;
                }
            }
            return false;
        }

        public bool RemoveData(T data)
        {
            Point<T>? current;
            int index = GetIndex(data);
            if (table[index].Data.Equals(data))
            {
                if (table[index].Next == null) //один элемент в цепочке
                {
                    table[index] = null;
                }
                else
                {
                    table[index] = table[index].Next;
                    table[index].Pred = null;
                }
                return true;
            }
            else
            {
                current = table[index];
                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        Point<T>? pred = current.Pred;
                        Point<T>? next = current.Next;
                        pred.Next = next;
                        current.Pred = null;
                        if (next != null)
                        {
                            next.Pred = pred;
                        }
                        return true;
                    }
                    current = current.Next;
                }
            }
            return false;
        }

        //private
        int GetIndex(T data)
        {
            return Math.Abs(data.GetHashCode()) % Capacity;
        }
    }
}