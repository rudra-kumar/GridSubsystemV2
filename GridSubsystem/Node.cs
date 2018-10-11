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
        Grid m_Grid;
        List<string> m_Words;
        List<Node> m_ChildNode;
        static List<string> m_FinalGrid = new List<string>();
        private static int count = 0;
        #endregion

        #region Properties
        public Grid Grid
        {
            get
            {
                return m_Grid;
            }
        }
        public List<string> FinalGrid
        {
            get { return m_FinalGrid; }
        }
        #endregion

        #region Public Members
        public Node(int rows, int columns, List<string> words)
        {

            m_Grid = new Grid(rows, columns);
            m_Words = new List<string>(words);
            m_ChildNode = new List<Node>();
        }

        public Node Copy()
        {
            var sortedWords = m_Words.OrderByDescending(x => x).ToList();
            Node newNode = new Node(m_Grid.Rows, m_Grid.Columns, sortedWords);
            newNode.m_Grid = new Grid(m_Grid);
            return newNode;
        }

        public void GreedyAlgorithm()
        {
            List<Word> potentialWords = m_Grid.GenPossiblePositionsV2(m_Words);
            //Console.WriteLine(m_Grid.ToString());
            if(potentialWords.Count == 0)
            {
                m_FinalGrid.Add(m_Grid.ToString());
            }
            else
            {
                if (m_Grid.IsEmpty)
                {
                    foreach (Word word in potentialWords)
                    {
                        Node childNode = Copy();
                        childNode.m_Grid.InsertWord(word);
                        childNode.m_Words.Remove(word.Value);
                        m_ChildNode.Add(childNode);
                        childNode.GreedyAlgorithm();
                    }
                }
                else
                {
                    Node childNode = Copy();
                    childNode.m_Grid.InsertWord(potentialWords[0]);
                    childNode.m_Words.Remove(potentialWords[0].Value);
                    m_ChildNode.Add(childNode);
                    childNode.GreedyAlgorithm();
                }
            }


        }


        #endregion

        #region Private Members 
        #endregion

    }
}
