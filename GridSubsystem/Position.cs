using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSubsystem
{
    [Serializable]
    struct Position
    {
        public int m_X, m_Y;
        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }
        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        public Position(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }

        /// <summary>
        /// Method to create copies of an object
        /// </summary>
        /// <param name="rhs">Objec to copy</param>
        public Position(Position position)
        {
            m_X = position.m_X;
            m_Y = position.m_Y;
        }

        public Position Copy()
        {
            Position pos;
            pos.m_X = m_X;
            pos.m_Y = m_Y;
            return pos;
        }

        public static bool operator ==(Position lhs, Position rhs)
        {

            if (lhs.m_X == rhs.m_X && lhs.m_Y == rhs.m_Y)
                return true;
            else
                return false;
        }

        public static bool operator !=(Position lhs, Position rhs)
        {

            if (lhs.m_X != rhs.m_X || lhs.m_Y != rhs.m_Y)
                return true;
            else
                return false;
        }

        public static bool operator <=(Position lhs, Position rhs)
        {
            if (lhs.m_X <= rhs.m_X && lhs.m_Y <= rhs.m_Y)
                return true;
            else
                return false;
        }

        public static bool operator >=(Position lhs, Position rhs)
        {
            if (lhs.m_X >= rhs.m_X && lhs.m_Y >= rhs.m_Y)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return String.Format("X: {0}, Y: {1}", m_X, m_Y);
        }
    }
}
