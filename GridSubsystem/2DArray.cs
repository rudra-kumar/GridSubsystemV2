using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSubsystem
{
    class _2DArray<T> :IEnumerable
    {
        #region Member Variables
        int m_Height;
        int m_Width;
        T[] m_Array;
        #endregion

        public int Height
        {
            get { return m_Height; }
        }
        public int Width
        {
            get { return m_Width; }
        }

        public int Size
        {
            get { return m_Width * m_Height; }
        }
        public _2DArray(int height, int width)
        {
            m_Height = height;
            m_Width = width;
            m_Array = new T[height * width];
        }

        public T this[int row, int col]
        {
            get
            {
                if (col >= m_Width || col < 0 || row >= m_Height || row < 0)
                {
                    Console.WriteLine("*Warning* Index out of range");
                    return default(T);
                }
                return m_Array[m_Width * row + col];
            }
            set
            {
                if (col >= m_Width || col < 0 || row >= m_Height || row < 0)
                {
                    Console.WriteLine("*Warning* Index out of range");
                }
                else
                    m_Array[m_Width * row + col] = value;
            }
        }
        public T this[int index]
        {
            get
            {
                return m_Array[index];
            }
        }
        public T[] To1D()
        {
            return m_Array;
        }
        public override string ToString()
        {
            string output = "";
            int count = 0;
            foreach (T element in m_Array)
            {
                if (count == m_Width)
                {
                    output += '\n';
                    count = 0;
                }
                output += '|';
                output += element.ToString();
                count++;
            }
            return output;
        }

        public IEnumerator GetEnumerator()
        {
            return m_Array.GetEnumerator();
        }
    }
}
