using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSubsystem
{
    [Serializable]
    struct Letter
    {
        private char m_Letter;

        public char Value
        {
            get { return m_Letter; }
            set { m_Letter = value; }
        }

        public static char Empty
        {
            get { return '\0';  }
        }

        public Letter(char letter)
        {
            m_Letter = letter;
        }

        public static bool operator ==(Letter lhs, Letter rhs)
        {
            return lhs.Value == rhs.Value;
        }
        public static bool operator !=(Letter lhs, Letter rhs)
        {
            return lhs.Value == rhs.Value;
        }

        public override string ToString()
        {
            return m_Letter.ToString();
        }
    }
}
