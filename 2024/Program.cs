namespace _2024
{
    using System.Diagnostics;
    using Utils;

    internal class Program
    {
        static void Main(string[] args)
        {
            const string pathTest = @"D:\AOC_Test.txt";
            const string pathPart1 = @"D:\AOC_Part1.txt";
            const string pathPart2 = @"D:\AOC_Part2.txt";
            long result = DayHelper.Solve(Day11.Part_2, pathPart1, out Stopwatch timer);
            Console.WriteLine(result);
            Console.WriteLine(timer.ElapsedMilliseconds.ToString() + "ms");
        }
    }

    
}
