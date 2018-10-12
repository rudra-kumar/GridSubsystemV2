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
        static readonly List<char> m_Alphabets = new List<char>() {'A', 'B', 'C', 'D', 'E',
                                                                'F','G','H','I','J','K','L','M',
                                                                'N','O','P','Q','R','S','T','U',
                                                                'V','W','X','Y','Z'};
        private Dictionary<char, List<string>> m_CharMappedWords;

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

            // Map each word to all the alphabets it has
            m_CharMappedWords = new Dictionary<char, List<string>>();
            foreach (char Alphabet in m_Alphabets)
                m_CharMappedWords.Add(Alphabet, new List<string>());
            CharMapWords();
            m_ChildNode = new List<Node>();
        }

        public Node(int rows, int columns)
        {
            m_Grid = new Grid(rows, columns);
            m_ChildNode = new List<Node>();
        }

        void CharMapWords()
        {
            for (int wordIndex = 0; wordIndex < m_Words.Count; wordIndex++)
            {
                string word = m_Words[wordIndex];
                for (int charIndex = 0; charIndex < word.Length; charIndex++)
                {
                    m_CharMappedWords[word[charIndex]].Add(word);
                }
            }
        }

        public Node Copy()
        {
            Node newNode = new Node(m_Grid.Rows, m_Grid.Columns);
            newNode.m_Grid = new Grid(m_Grid);
            newNode.m_CharMappedWords = GenericCopier<Dictionary<char, List<string>>>.DeepCopy(m_CharMappedWords);
            return newNode;
        }

        public void GreedyAlgorithm()
        {
            //Console.WriteLine(m_Grid.ToString());
            List<Word> potentialWords = m_Grid.GenPossiblePositionsV3(m_CharMappedWords);
            //Console.WriteLine(m_Grid.ToString());
            if(potentialWords.Count == 0)
            {
                m_FinalGrid.Add(m_Grid.ToString());
            }
            else
            {
                //if (m_Grid.IsEmpty)
                {
                    foreach (Word word in potentialWords)
                    {
                        Node childNode = Copy();
                        childNode.m_Grid.InsertWord(word);
                        // Remove inserted word
                        for (int charIndex = 0; charIndex < word.Length; charIndex++)
                        {
                            childNode.m_CharMappedWords[word[charIndex].Value].Remove(word.Value);
                        }
                        m_ChildNode.Add(childNode);
                        childNode.GreedyAlgorithm();
                    }
                }
                //else
                //{
                //    Node childNode = Copy();
                //    Word word = potentialWords[0];
                //    childNode.m_Grid.InsertWord(word);
                //    // Remove inserted word
                //    for (int charIndex = 0; charIndex < word.Length; charIndex++)
                //    {
                //        childNode.m_CharMappedWords[word[charIndex].Value].Remove(word.Value);
                //    }
                //    m_ChildNode.Add(childNode);
                //    childNode.GreedyAlgorithm();
                //}
            }


        }


        #endregion

        #region Private Members 
        #endregion

    }
}
