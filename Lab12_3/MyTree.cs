using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;
using Lab12_2;

namespace Lab12_3
{
    public class MyTree<T> where T: IInit, IComparable, ICloneable, new()
    {
        public Point<T> root = null;
        public bool isFindTree = false;
        Random rnd = new Random();

        int count = 0;
        public int Count => count;
        public MyTree(int length = 5)
        {
            count = length;
            root = MakeTree(length, root);
            isFindTree = false;
        }
        public void ShowTree()
        {
            Show(root);
        }

        public T MakeRandomData()
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
            return data;
        }

        //ИСД
        public Point<T>? MakeTree(int length, Point<T>? point)
        {
            if (length == 0)
            {
                return null;
            }
            T data = new T();
            data = MakeRandomData();
            //data.RandomInit(rnd);
            Point<T> newItem = new Point<T>(data);
            int nl = length / 2;
            int nr = length - nl - 1;
            newItem.Left = MakeTree(nl, newItem.Left);
            newItem.Right = MakeTree(nr, newItem.Right);
            return newItem;
        }
        public void Show(Point<T>? point, int spaces=5)
        {
            if (point != null)
            {
                Show(point.Left, spaces + 5);
                for (int i = 0; i < spaces; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(/*(point.Data is Vehicle).ToString() +*/ point.Data);
                Show(point.Right, spaces + 5);
            }
        }
        
        public void AddPoint(T data)
        {
            Point<T>? point = root;
            Point<T>? current = null;
            bool isExist = false;
            while (point != null && !isExist)
            {
                current = point;
                if (point.Data.CompareTo(data) == 0)
                {
                    isExist = true;
                }
                else
                {
                    if (point.Data.CompareTo(data) < 0)
                    {
                        point = point.Left;
                    }
                    else
                    {
                        point = point.Right;
                    }
                }
            }
            if (isExist)
            {
                return;
            }
            Point<T> newPoint = new Point<T>(data);
            if (current.Data.CompareTo(data) < 0)
            {
                current.Left = newPoint;
            }
            else
            {
                current.Right = newPoint;
            }
            count++;
        }

        public void TransformToArray(Point<T>? point, T[] array, ref int current)
        {
            if (point != null)
            {
                TransformToArray(point.Left, array, ref current);
                array[current] = (T)point.Data.Clone();
                current++;
                TransformToArray(point.Right, array, ref current);
            }
        }

        public MyTree<T>? TransformToFindTree()
        {
            T[] array = new T[count];
            int current = 0;
            TransformToArray(root, array, ref current);

            MyTree<T> findTree = new MyTree<T>();
            findTree.root = new Point<T>(array[0]);
            findTree.count = 0;
            for (int i=1; i<array.Length; i++)
            {
                findTree.AddPoint(array[i]);
            }
            isFindTree = true;
            return findTree;
        }

        public void FindMaxTree(Point<T>? p, ref Point<T> maxObj)
        {
            if (p != null)
            {
                if (p.CompareTo(maxObj) > 0)
                {
                    maxObj = p;
                }
                FindMaxTree(p.Left, ref maxObj);
                FindMaxTree(p.Right, ref maxObj);
            }
            return;
        }
        public Point<T>? FindMaxFindTree()
        {
            if (root != null)
            {
                Point<T>? current = root;
                while (current.Right != null)
                {
                    current = current.Right;
                }
                return current;
            }
            return null;
        }
        public Point<T>? FindMax()
        {
            if (isFindTree)
            {
                return FindMaxFindTree();
            }
            else
            {
                Point<T> maxPoint = root;
                Point<T> current = root;
                FindMaxTree(current, ref maxPoint);
                return maxPoint;
            }
        }
        //public void FindElemInTreeByYear(Point<T>? p, ref Point<T> neededObj, int year)
        //{
        //    if (p != null)
        //    {
        //        if (p.Equals(year))
        //        {
        //            neededObj = p;
        //            return;
        //        }
        //        FindElemInTreeByYear(p.Left, ref neededObj, year);
        //        FindElemInTreeByYear(p.Right, ref neededObj, year);
        //    }
        //    return;
        //}
    }
}
