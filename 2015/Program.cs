namespace _2015
{
    using System.Diagnostics;
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine(Day02.Part_2(Day02.input_2).ToString());
            timer.Stop();
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }
}
