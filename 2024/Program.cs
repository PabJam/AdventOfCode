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
            Stopwatch timer = new Stopwatch();
            timer.Start();
            long result = DayHelper.Solve(Day01.Part_1, pathPart1);
            timer.Stop();
            Console.WriteLine(result);
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }

    
}
