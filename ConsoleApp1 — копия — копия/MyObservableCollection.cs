using ClassLibraryLab10;
using Lab12_4;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab12_4
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
    public class MyObservableCollection<T> : MyCollection<T> where T : IInit, ICloneable, new()
    {
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;
        public string Name { get ; set; }    

        public int Length => Count;

        public new T this[int index]
        {
            get => base[index];
            set
            {
                base[index] = value;
                CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs("Item replaced", value));
            }
        }

        public new void Add(T item)
        {
            base.Add(item);
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Item added", item));
        }

        public new bool Remove(T item)
        {
            bool result = base.Remove(item);
            if (result)
            {
                CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("Item removed", item));
            }
            return result;
        }
        public MyObservableCollection() : base() { }
        public MyObservableCollection(int size) : base(size) { }
        public MyObservableCollection(T[] collection) : base(collection) { }


    }
}

