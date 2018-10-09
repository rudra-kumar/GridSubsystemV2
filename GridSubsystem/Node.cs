using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSubsystem
{
    class Node
    {
        #region Private Members 
        Node m_BestNode;
        Node m_RootNode;
        Grid m_Grid;
        List<Position> possibleWords;
        List<Node> m_ChildNode;
        #endregion

        #region Properties
        #endregion

        #region Public Members
        public Node()
        {
            m_BestNode = new Node();
            m_RootNode = new Node();
        }


        #endregion

        #region Private Members 
        #endregion

    }
}
