﻿using System;
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
            Grid grid = new Grid(10, 10);
            List<string> words = new List<string> {"AXE", "END", "FAN", "DAN" };
            grid.InsertWord(new Word("CAT", new Position(0, 0), Orientation.Horizontal));
            grid.GenerateGrid(words);
            //Console.WriteLine(grid.InsertWord(new Word("CAT", new Position(0, 0), Orientation.Horizontal)));
            //Console.WriteLine(grid.InsertWord(new Word("AXE", new Position(0, 1), Orientation.Vertical)));
            //Console.WriteLine(grid.InsertWord(new Word("XN", new Position(1, 1), Orientation.Vertical)));
            Console.WriteLine(grid.ToString());



            Console.ReadLine();
        }
    }
}
