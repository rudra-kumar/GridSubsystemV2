using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSubsystem
{
    enum Orientation
    {
        Default,
        Horizontal,
        Vertical
    }

    class Word : IEnumerable
    {
        #region Private Data Members
        private List<Letter> m_Data;
        private int m_Length;
        private Position m_Position;
        private Orientation m_Orientation;
        private string m_StringRepresentation;
        #endregion

        #region Properties
        public string Value
        {
            get { return m_StringRepresentation; }
        }
        public int Length
        {
            get { return m_Length; }
        }
        public Position Position
        {
            get { return m_Position; }
        }
        public Position EndPosition
        {
            get
            {
                if (m_Orientation == Orientation.Horizontal)
                    return new Position(m_Position.X, m_Position.Y + (m_Length - 1));
                else
                    return new Position(m_Position.X + (m_Length - 1), m_Position.Y);
            }
        }
        public Orientation Orientation
        {
            get { return m_Orientation; }
        }
        public Letter this[int index]
        {
            get
            {
                return m_Data[index];
            }
        }

        #endregion

        #region Public Methods
        public Word(string word, Position position, Orientation orientation)
        {
            m_Position = position;
            m_Length = word.Length;
            m_Orientation = orientation;
            m_Data = new List<Letter>();
            m_StringRepresentation = word;
            ProcessWord(ref word);
        }

        public IEnumerator GetEnumerator()
        {
            return m_Data.GetEnumerator();
        }

        public override string ToString()
        {
            string output = "";
            foreach (Letter l in m_Data)
                output += l.ToString();
            return output;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// ConvertsString to list of letters
        /// </summary>
        private void ProcessWord(ref string word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                m_Data.Add(new Letter(word[i]));
            }
        }
        #endregion
    }
}
