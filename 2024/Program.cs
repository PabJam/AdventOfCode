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
            //Console.WriteLine(Day16.Part_1(Day16.mini_test_input).ToString());
            //Console.WriteLine(Day16.Part_1(Day16.test_input_1).ToString());
            //518 is to low    
            Console.WriteLine(Day16.Part_2(Day16.input_2).ToString());   
            timer.Stop();
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }
}
