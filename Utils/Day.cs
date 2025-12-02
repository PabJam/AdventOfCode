using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2025
{

    public interface IDay
    {
        static abstract long Part_1(string input);
        static abstract long Part_2(string input);
    }

    public static class DayHelper
    {
        public static long Solve(Func<string, long> puzzle, string inputPath)
        {
            string input = File.ReadAllText(inputPath);
            return puzzle(input);
        }
    }
}
