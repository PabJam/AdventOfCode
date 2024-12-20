namespace _2024
{
    using System.Diagnostics;
    using Utils;
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine(Day20.Part_2(Day20.input_2).ToString());   
            timer.Stop();
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }
}
