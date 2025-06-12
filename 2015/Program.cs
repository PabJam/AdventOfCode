namespace _2015
{
    using System.Diagnostics;
    using System.Numerics;

    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine(Day06.Part_1(Day06.input_1).ToString());
            timer.Stop();
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }
}
