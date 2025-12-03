using System.Diagnostics;
using Utils;

namespace _2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string pathTest = @"D:\AOC_Test.txt";
            const string pathPart1 = @"D:\AOC_Part1.txt";
            const string pathPart2 = @"D:\AOC_Part2.txt";
            Stopwatch timer = new Stopwatch();
            timer.Start();
            long result = DayHelper.Solve(Day03.Part_2, pathPart1);
            timer.Stop();
            Console.WriteLine(result);
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }
}
