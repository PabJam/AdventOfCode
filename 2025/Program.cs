namespace _2025
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string pathTest = @"D:\AOC_Test.txt";
            const string pathPart1 = @"D:\AOC_Part1.txt";
            const string pathPart2 = @"D:\AOC_Part2.txt";
            Day01 day = new Day01();
            // 5698
            long result = Day.Solve(day.Part_2, pathPart1);
            Console.WriteLine(result);
        }
    }
}
