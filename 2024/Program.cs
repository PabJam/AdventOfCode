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
            // not 68,36
            Console.WriteLine(Day18.Part_2(Day18.input_2).ToString());   
            timer.Stop();
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }
}
