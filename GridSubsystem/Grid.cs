using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSubsystem
{
    // Enum to represent offset from a specific cell
    enum Offset
    {
        Left,
        Right,
        Top,
        Bottom
    }

    class Grid
    {
        #region Private Members
        private int m_Rows;
        private int m_Columns;
        _2DArray<Cell> m_Grid;
        List<Word> m_InsertedWords;
        bool m_IsEmpty = true;
        static readonly List<char> m_Alphabets = new List<char>() {'A', 'B', 'C', 'D', 'E',
                                                                'F','G','H','I','J','K','L','M',
                                                                'N','O','P','Q','R','S','T','U',
                                                                'V','W','X','Y','Z'};
        public static int count = 0;
        private List<Word> m_wordList;
        #endregion

        #region Properties
        public int Rows
        {
            get { return m_Rows; }
        }
        public int Columns
        {
            get { return m_Columns; }
        }
        public bool IsEmpty
        {
            get { return m_IsEmpty; }
        }
        #endregion

        #region Public Methods
        public Grid(int rows, int columns)
        {
            m_Rows = rows;
            m_Columns = columns;
            m_Grid = new _2DArray<Cell>(rows, columns);
            m_InsertedWords = new List<Word>();
            InitCells();
        }
        // Clone the grid.
        public Grid(Grid grid)
        {
            m_Rows =    grid.m_Rows;
            m_Columns = grid.m_Columns;
            m_Grid = new _2DArray<Cell>(grid.m_Grid);
            m_InsertedWords = new List<Word>(grid.m_InsertedWords);
            m_IsEmpty = grid.m_IsEmpty;
        }


        public void InitializeWordList(List<string> words)
        {
            Position defaultPosition;
            defaultPosition.m_X = -1;
            defaultPosition.m_Y = -1;
            foreach (string word in words)
            {
                m_wordList.Add(new Word(word, defaultPosition, Orientation.Horizontal));
                m_wordList.Add(new Word(word, defaultPosition, Orientation.Vertical));
            }

        }


        public List<Word> GenPossiblePositionsV2(Dictionary<char,List<string>> charMappedWords)
        {
            List<Word> possibleWords = new List<Word>();
            // For each cell in the grid that is not empty 
            Position position;
            // If Grid is empty then generate all possible positions for all words
            if(m_IsEmpty)
            {
                HashSet<string> words = new HashSet<string>();
                foreach (char key in charMappedWords.Keys)
                    foreach (string word in charMappedWords[key])
                        words.Add(word);

                possibleWords = GenPossiblePositions(words.ToList());

            }
            else
            {
                for (int row = 0; row < m_Rows; row++)
                    for (int col = 0; col < m_Columns; col++)
                    {
                        Cell cell = m_Grid[row, col];
                        // If the cell contains a char 
                        char cellData = cell.Data.Value;
                        if(cellData != '\0')
                        {
                            // Try inserting words that contain that letter Horizontally & Vertically 
                            for(int wordIndex = 0; wordIndex < charMappedWords[cellData].Count; wordIndex++)
                            {
                                string word = charMappedWords[cellData][wordIndex];
                                // #TODO: Optimizations check if the nearby cells are empty and call only 1 constructor
                                string[] splitString = word.Split(cellData);
                                position.m_X = cell.Position.X;
                                position.m_Y = cell.Position.Y - (splitString[0].Length);
                                Word Horizontal = new Word(word, position, Orientation.Horizontal);
                                // Offset for vertical positions
                                position.m_X = cell.Position.X - (splitString[0].Length);
                                position.m_Y = cell.Position.Y;

                                Word Vertical = new Word(word, position, Orientation.Vertical);
                                if (CanInsertWord(Horizontal))
                                    possibleWords.Add(Horizontal);
                                if (CanInsertWord(Vertical))
                                    possibleWords.Add(Vertical);
                            }
                        }
                    }
            }
            return possibleWords;
        }

        /// <summary>
        /// Generates all possible positions, Each word can be inserted
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public List<Word> GenPossiblePositions(List<string> words)
        {
            List<Word> possibleWords = new List<Word>();
            foreach(string word in words)
            {
                for (int row = 0; row < m_Rows; row++)
                    for (int col = 0; col < m_Columns; col++)
                    {
                        Position position;
                        position.m_X = row;
                        position.m_Y = col;
                        Word Horizontal = new Word(word, position, Orientation.Horizontal);
                        Word Vertical = new Word(word, position, Orientation.Vertical);

                        if (CanInsertWord(Horizontal))
                            possibleWords.Add(Horizontal);
                        if (CanInsertWord(Vertical))
                            possibleWords.Add(Vertical);
                    }
            }
            return possibleWords;
        }

        /// <summary>
        /// Insert a word 
        /// </summary>
        /// <param name="word">Word to be inserted</param>
        /// <returns>Bool representing if the operation was successful.</returns>
        public bool InsertWord(Word word)
        {
           
            List<Position> insertionPositions = GetRange(word);
            for(int index = 0; index < insertionPositions.Count; index++)
            {
                Position position = insertionPositions[index];
                m_Grid[position.X, position.Y].Data = word[index];
            }
            m_InsertedWords.Add(word);
            if (m_IsEmpty)
                m_IsEmpty = false;
            return true;
        }

        public override string ToString()
        {
            string output = "";
            int count = 0;
            foreach(Cell cell in m_Grid.To1D())
            {
                if(count == m_Columns)
                {
                    output += '\n';
                    count = 0;
                }
                output += '|';
                output += cell.ToString();
                count++;
            }
            return output;
        }

        /// <summary>
        /// Generates a grid when given a set of words        /// </summary>
        /// <param name="words">list of potential words</param>
        public void GenerateGrid(List<string> words)
        {
            // Sort the incoming words by alphabet
            Dictionary<char, List<string>> alphabeticalMap = new Dictionary<char, List<string>>();
            List<string> potentialWords = new List<string>(words);
            foreach(char letter in m_Alphabets)
                alphabeticalMap.Add(letter, new List<string>());

            for(int word = 0; word < potentialWords.Count; word++)
                alphabeticalMap[potentialWords[word][0]].Add(potentialWords[word]);



            for(int row = 0; row < m_Rows; row++)
                for(int col = 0; col < m_Columns; col++)
                {
                    // iterate over all the words in the list
                    for(int word = 0; word < potentialWords.Count; word++)
                    {
                        Position position;
                        position.m_X = row;
                        position.m_Y = col;
                        if (InsertWord(new Word(potentialWords[word], position, Orientation.Horizontal)) ||
                            InsertWord(new Word(potentialWords[word], position, Orientation.Vertical)))
                        {
                            potentialWords.RemoveAt(word);
                            word = 0;
                            row = 0;
                            col = 0;
                            break;
                        }
                    }
                }

            // While all non empty cells have been checked 
            //  Try inserting the corresponding word to the char horizontally and vertically,
            //  IF operation succeeds
            //      , move that word out of the word list 
            //      Start operation all over again for all cells 
            //  ELSE
            //      Move on to the next cell
        }

        public void GreedyAlgorithm(List<string> words)
        {

            // If the masterlist is empty or possible locations is zero
            //  Print grid to the console.
            List<Word> potentialWords = GenPossiblePositions(words);

            // if the list is empty then exit the recursiveness 
            if (words.Count == 0 || potentialWords.Count == 0)
            {
                Console.WriteLine(count);
                count++;
                Console.WriteLine(ToString());
                Console.Write(String.Format("Inserted Words: {0}", m_InsertedWords.Count));
                Console.WriteLine();
            }
            else
            {
                //  Find all words with locations for remaining words on list
                foreach (Word word in potentialWords)
                {
                    Grid gridCopy = new Grid(this);
                    gridCopy.InsertWord(word);
                    List<string> wordsCopy = new List<string>(words);
                    wordsCopy.Remove(word.Value);
                    gridCopy.GreedyAlgorithm(wordsCopy);
                }
            }
        }
        #endregion

        #region Private Methods
        // Initialize the cells with their location;
        private void InitCells()
        {
            for (int row = 0; row < m_Rows; row++)
                for (int col = 0; col < m_Columns; col++)
                {
                    Position position;
                    position.m_X = row;
                    position.m_Y = col;
                    m_Grid[row, col] = new Cell(position);
                }
        }



        /// <summary>
        /// Checks if a word can be inserted Horizontally
        /// </summary>
        /// <param name="word">Word to be Inserted</param>
        /// <returns>True if word can be inserted</returns>
        private bool CanInsertWord(Word word)
        {
            bool isOverlapping = false;
            // If the word is longer than columns 
            if (!IsValidIndex(word.EndPosition.X, word.EndPosition.Y) || !IsValidIndex(word.Position.X, word.Position.Y))
                return false;
            // Get the range of cells that the word would be inserted in. 
            List<Position> cellPositions = GetRange(word);

            // Check if the word would be inserted horizontally or Vertically 
            // If it is to be inserted horizontally 
            // Check that range to see if it is empty 
            if(cellPositions.Count != 0)
            {
                for (int index = 0; index < cellPositions.Count; index++)
                {
                    Cell cell = m_Grid[cellPositions[index].X, cellPositions[index].Y];
                    if (cell.isEmpty)
                    {
                        switch (word.Orientation)
                        {
                            case Orientation.Horizontal:
                                //  IF empty check the cell above and below it is empty or not
                                if (CheckIfEmpty(cellPositions[index], Offset.Top) && CheckIfEmpty(cellPositions[index], Offset.Bottom))
                                {
                                    // IF overriding the first letter of the word, ensure you're not overriding a horizontal word
                                    if (index == 0 && !CheckIfEmpty(cellPositions[index], Offset.Left))
                                        return false;
                                    continue;
                                }
                                else
                                    return false;
                            case Orientation.Vertical:
                                if (CheckIfEmpty(cellPositions[index], Offset.Left) && CheckIfEmpty(cellPositions[index], Offset.Right))
                                {
                                    // IF overriding the first letter of the word, ensure you're not overriding a Vertical word
                                    if (index == 0 && !CheckIfEmpty(cellPositions[index], Offset.Top))
                                        return false;
                                    continue;
                                }
                                else
                                    return false;
                        }
                    }
                    // Else if the cell contains the same letter 
                    // check if it is the same word ? 
                    else if (cell.Data == word[index])
                    {
                        isOverlapping = true;
                        // if the cell to the right is empty, continue to the next letter and cell
                        switch (word.Orientation)
                        {
                            case Orientation.Horizontal:
                                if (CheckIfEmpty(cellPositions[index], Offset.Right))
                                {
                                    // IF overriding the first letter of the word, ensure you're not overriding a horizontal word
                                    if (index == 0 && !CheckIfEmpty(cellPositions[index], Offset.Left))
                                        return false;
                                    continue;
                                }
                                else
                                    return false;
                            case Orientation.Vertical:
                                if (CheckIfEmpty(cellPositions[index], Offset.Bottom))
                                {
                                    // IF overriding the first letter of the word, ensure you're not overriding a Vertical word
                                    if (index == 0 && !CheckIfEmpty(cellPositions[index], Offset.Top))
                                        return false;
                                    continue;
                                }
                                else
                                    return false;
                        }
                    }
                    else
                        return false;
                }
            }
            if (isOverlapping || m_IsEmpty)
                return true;
            else
                return false;
        }


        // Checks if the index is valid
        private bool IsValidIndex(int row, int col)
        {
            if (row > -1 && row < m_Rows && col > -1 && col < m_Columns)
                return true;
            return false;
        }

        // Function that gets the range of positions the word would occupy when given the start and end positions
        private List<Position> GetRange(Word word)
        {
            List<Position> range = new List<Position>();
            // Start at 0,0
            // While position is less than the ending position
            for(Position pos = word.Position.Copy(); pos <= word.EndPosition; )
            {
            //  Add position to the list
                range.Add(pos);
            //  Increment position 
                if (word.Orientation == Orientation.Horizontal)
                    pos.Y++;
                else
                    pos.X++;
            }
            return range;
        }


        /// <summary>
        /// Checks if the surrounding cell is empty
        /// </summary>
        /// <param name="position">Position of the cell</param>
        /// <param name="offset">Offset to the surrounding cells</param>
        /// <returns></returns>
        private bool CheckIfEmpty(Position position, Offset offset)
        {
            switch (offset)
            {
                case Offset.Left:
                    int leftCell = position.Y - 1;
                    if (!IsValidIndex(position.X, leftCell))
                        return true;
                    else
                        return IsCellEmpty(m_Grid[position.X, leftCell]);
                case Offset.Right:
                    int rightCell = position.Y + 1;
                    if (!IsValidIndex(position.X, rightCell))
                        return true;
                    else
                        return IsCellEmpty(m_Grid[position.X, rightCell]);
                case Offset.Top:
                    int cellAbove = position.X - 1;
                    if (!IsValidIndex(cellAbove, position.Y))
                        return true;
                    else
                        return IsCellEmpty(m_Grid[cellAbove, position.Y]);
                case Offset.Bottom:
                    int cellBelow = position.X + 1;
                    if (!IsValidIndex(cellBelow, position.Y))
                        return true;
                    else
                        return IsCellEmpty(m_Grid[cellBelow, position.Y]);
                default:
                    Console.WriteLine("CheckIfEmpty Function failed");
                    return false;
            }
        }

        // Checks to see if the letter is empty at given index in the grid
        private bool IsCellEmpty(Cell cell)
        {
            return cell.isEmpty;
        }


        #endregion

    }
}
