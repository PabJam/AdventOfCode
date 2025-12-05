using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{

    public interface IDay
    {
        static abstract long Part_1(string input);
        static abstract long Part_2(string input);
    }

    public static class DayHelper
    {
        public static long Solve(Func<string, long> puzzle, string inputPath, out Stopwatch timer)
        {
            string input = File.ReadAllText(inputPath);
            timer = new Stopwatch();
            timer.Start();
            long result = puzzle(input);
            timer.Stop();
            return result;
        }
    }
}
