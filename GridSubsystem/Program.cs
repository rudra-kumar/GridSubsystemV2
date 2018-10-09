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
            Grid grid = new Grid(4, 4);
            List<string> words = new List<string> {"CAT", "ASH", "HOAX", "TAX"};

            grid.GreedyAlgorithm(words);
            //Console.WriteLine(grid.InsertWord(new Word("BOX", new Position(0, 1), Orientation.Vertical)));
            //List<Word> possibleWords = grid.GenPossiblePositions(words);

            //foreach (Word word in possibleWords)
            //{
            //    Console.WriteLine(String.Format("Word {0}, Orientation: {1}, Position: {2}", word.ToString(), word.Orientation.ToString(), word.Position.ToString()));
            //}
            //Console.WriteLine(grid.ToString());
            Console.ReadLine();
        }
    }
}
