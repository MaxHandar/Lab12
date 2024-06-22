using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab12_3;

namespace Lab12_2
{
    internal class Point<T> where T: IComparable
    {
        public T? Data { get; set; }
        public Point<T>? Right { get; set; }
        public Point<T>? Left { get; set; }
        public Point()
        {
            this.Data = default(T);
            this.Right = null;
            this.Left = null;
        }
        public Point(T data)
        {
            this.Data = data;
            this.Right = null;
            this.Left = null;
        }
        public override string ToString()
        {
            return Data == null ? "" : Data.ToString();
        }
        public override int GetHashCode()
        {
            return Data == null ? 0 : Data.GetHashCode();
        }
        public int CompareTo(Point<T> other)
        {
            return Data.CompareTo(other.Data);
        }
    }
}