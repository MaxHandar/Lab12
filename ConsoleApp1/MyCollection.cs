using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab12_4
{
    public class MyCollection<T> : MyList<T>, IList<T>, IEnumerable<T> where T : IInit, ICloneable, new()
    {
        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }

        public MyCollection():base() { }
        public MyCollection(int size):base(size) { }
        public MyCollection(T[] collection):base(collection) { }

        public IEnumerator<T> GetEnumerator()
        {
            //return new MyEnumerator<T>(this);
            Point<T> current = beg;
            while (current != null)
            {
                yield return current.Data; 
                current = current.Next;
            }
        }


        IEnumerator IEnumerable.GetEnumerator()//не трогаем
        {
            //throw new NotImplementedException();
            return GetEnumerator();
        }
        public void Insert(int index, T item)
        {
            base.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }

        public int IndexOf(T item)
        {
            return base.IndexOf(item);
        }
        public void Add(T item)
        {
            base.Add(item);
        }

        public void Clear()
        {
            base.Clear();
        }

        public bool Contains(T item)
        {
            return base.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return base.Remove(item);
        }
    }

    public class MyEnumerator<T> : IEnumerator<T> where T : IInit, ICloneable, new()
    {
        Point<T>? beg;
        Point<T>? current;

        public MyEnumerator(MyCollection<T> collection)
        {
            beg = collection.beg;
            current = beg;
        }
        public T Current => current.Data;

        object IEnumerator.Current => throw new NotImplementedException();//не трогаем

        public void Dispose()//не трогаем
        {
            //throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (current.Next == null)
            {
                Reset();
                return false;
            }
            else
            {
                current = current.Next;
                return true;
            }
        }

        public void Reset()
        {
            current = beg;
        }
    }
}
