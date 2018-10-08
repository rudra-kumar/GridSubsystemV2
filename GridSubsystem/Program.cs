using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridSubsystem
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello World!!");
            Grid grid = new Grid(3, 3);
            List<string> words = new List<string> {"CAT", "AXE", "BOX"};
            //Console.WriteLine(grid.InsertWord(new Word("CAT", new Position(0, 0), Orientation.Horizontal)));
            List<Word> possibleWords = grid.GenPossiblePositions(words);
            //Console.WriteLine(grid.InsertWord(new Word("CAT", new Position(0, 0), Orientation.Horizontal)));
            //Console.WriteLine(grid.InsertWord(new Word("AXE", new Position(0, 1), Orientation.Vertical)));
            //Console.WriteLine(grid.InsertWord(new Word("XN", new Position(1, 1), Orientation.Vertical)));
            foreach (Word word in possibleWords)
            {
                Console.WriteLine(String.Format("Word {0}, Orientation: {1}, Position: {2}", word.ToString(), word.Orientation.ToString(), word.Position.ToString()));

            }
            Console.WriteLine(grid.ToString());



            Console.ReadLine();
        }
    }
}
