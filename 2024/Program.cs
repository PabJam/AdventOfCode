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
            //518 is to low    
            //45
            Console.WriteLine(Day16.Part_2(Day16.mini_test_input).ToString());   
            //64
            Console.WriteLine(Day16.Part_2(Day16.test_input_2).ToString());     
            timer.Stop();
            Console.WriteLine(timer.Elapsed.ToString());
        }
    }
}
