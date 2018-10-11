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
            //Grid grid = new Grid(3, 3);
            //List<string> words = new List<string> { "AL", "ALAN", "ANGELA", "BETTY", "BILL", "BRENDA", "CHARLES", "FRED", "GARY", "GEORGE", "GRAHAM", "HARRY", "JACK", "JESSICA", "JILL", "JOHNATHON", "LARRY", "MARK", "MARY", "MATTHEW", "OSCAR", "PAM", "PETER", "ROBERT", "ROGER", "RON", "RONALD", "ROSE", "SUSAN", "TOM", "WENDY" };
            //List<string> words = new List<string> { "CAT", "AXE", "BOX", "TAX"  };
            List<string> words = new List<string> { "CAT", "ASH", "HOAX", "TAX"  };
            //List<string> words = new List<string> { "IDOL", "GREEDY", "RUN" };
            //grid.GreedyAlgorithm(words);
            // 
            Node node = new Node(4, 4, words);
            node.GreedyAlgorithm();
            int count = 0;
            foreach (string str in node.FinalGrid)
            {
                Console.WriteLine(count);
                Console.WriteLine(str);
                count++;
            }
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
