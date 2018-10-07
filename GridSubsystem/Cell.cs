using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSubsystem
{
    enum CellState
    {
        Empty,
        Filled,
        Overridden
    }
    class Cell
    {
        #region Private Members
        Letter m_Data;
        Position m_Position;
        CellState m_State;
        
        #endregion

        #region Properties
        public Letter Data
        {
            get { return m_Data; }
            set
            {
                if (m_State == CellState.Empty)
                    m_State = CellState.Filled;
                m_Data = value;
            }
        }
        public Position Position
        {
            get { return m_Position; }
        }
        public CellState State
        {
            get { return m_State; }
        }

        public bool isEmpty
        {
            get { return m_State == CellState.Empty ? true : false; }
        }

        #endregion

        #region Public Members
        public Cell(Position position)
        {
            m_Position = new Position(-1, -1);
            m_Data.Value = Letter.Empty;
            m_Position.X = position.X;
            m_Position.Y = position.Y;
            m_State = CellState.Empty;
        }

        public Cell(Cell cell)
        {
            m_Position = cell.m_Position;
            m_Data.Value = cell.m_Data.Value;
            m_Position.X = cell.m_Position.X;
            m_Position.Y = cell.m_Position.Y;
            m_State = cell.m_State;
        }
        #endregion

        #region Private Member Functions
        #endregion

        #region Public Member Functions
        public override string ToString()
        {
            return m_Data.Value.ToString();
        }
        #endregion
    }
}
