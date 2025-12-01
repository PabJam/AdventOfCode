using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2025
{
    public abstract class Day
    {
        public delegate long Puzzle(string input);
        public static long Solve(Puzzle puzzle, string inputPath)
        {
            string input = File.ReadAllText(inputPath);
            return puzzle(input);
        }
        public abstract long Part_1(string input);
        public abstract long Part_2(string input);
    }
}
