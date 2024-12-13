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
            Console.WriteLine(Day13.Part_2(Day13.input_2).ToString());     
            timer.Stop();
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }
}
