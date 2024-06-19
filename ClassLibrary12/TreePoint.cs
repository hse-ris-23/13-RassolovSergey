using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab10;

namespace ClassLibrary12
{
    public class TreePoint<T> where T : IComparable
    {
        public T? Data { get; set; }
        public TreePoint<T>? Left { get; set; }
        public TreePoint<T>? Right { get; set; }



        // Конструктор -  ( Параметор - data )
        public TreePoint(T data)
        {
            this.Data = data;
            this.Left = null;
            this.Right = null;
        }

        // Метод - ToString
        public override string ToString()
        {
            if (Data == null)
            {
                return "";
            }
            else
            {
                return Data.ToString();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            TreePoint<T> other = (TreePoint<T>)obj;
            return Equals(Data, other.Data) &&
                   Equals(Left, other.Left) &&
                   Equals(Right, other.Right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (Data != null ? Data.GetHashCode() : 0);
                hash = hash * 23 + (Left != null ? Left.GetHashCode() : 0);
                hash = hash * 23 + (Right != null ? Right.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
