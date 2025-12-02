using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2015
{
    public class Day06 : IDay
    {
       
        public static long Part_1(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[,] grid = new int[1000, 1000];
            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection collection = Regex.Matches(lines[i], @"^(turn off|turn on|toggle) (\d+),(\d+) through (\d+),(\d+)");
                ExecuteInstruction(collection[0].Groups[1].Value, ((Int32.Parse(collection[0].Groups[2].Value), Int32.Parse(collection[0].Groups[3].Value)), (Int32.Parse(collection[0].Groups[4].Value), Int32.Parse(collection[0].Groups[5].Value))), grid);
            }
            long on = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x,y] == 1) { on++; }
                }
            }
            return on;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[,] grid = new int[1000, 1000];
            for (int i = 0; i < lines.Length; i++)
            {
                MatchCollection collection = Regex.Matches(lines[i], @"^(turn off|turn on|toggle) (\d+),(\d+) through (\d+),(\d+)");
                ExecuteInstruction2(collection[0].Groups[1].Value, ((Int32.Parse(collection[0].Groups[2].Value), Int32.Parse(collection[0].Groups[3].Value)), (Int32.Parse(collection[0].Groups[4].Value), Int32.Parse(collection[0].Groups[5].Value))), grid);
            }
            long on = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    on += grid[x, y];
                }
            }
            return on;
        }

        private static void ExecuteInstruction(string instruction, ((int, int), (int, int)) range, int[,] grid)
        {
            switch (instruction)
            {
                case "turn off":
                    Off(range, grid);
                    break;
                case "turn on": 
                    On(range, grid);
                    break;
                case "toggle":
                    Toggle(range, grid);
                    break;
            }
        }

        private static void Off(((int, int), (int, int)) range, int[,] grid)
        {
            for (int x = range.Item1.Item1; x <= range.Item2.Item1; x++)
            {
                for (int y = range.Item1.Item2; y <= range.Item2.Item2; y++)
                {
                    grid[x, y] = 0;
                }
            }
        }

        private static void On(((int, int), (int, int)) range, int[,] grid)
        {
            for (int x = range.Item1.Item1; x <= range.Item2.Item1; x++)
            {
                for (int y = range.Item1.Item2; y <= range.Item2.Item2; y++)
                {
                    grid[x, y] = 1;
                }
            }
        }

        private static void Toggle(((int, int), (int, int)) range, int[,] grid)
        {
            for (int x = range.Item1.Item1; x <= range.Item2.Item1; x++)
            {
                for (int y = range.Item1.Item2; y <= range.Item2.Item2; y++)
                {
                    grid[x, y] ^= 1;  
                }
            }
        }

        private static void ExecuteInstruction2(string instruction, ((int, int), (int, int)) range, int[,] grid)
        {
            switch (instruction)
            {
                case "turn off":
                    Off2(range, grid);
                    break;
                case "turn on":
                    On2(range, grid);
                    break;
                case "toggle":
                    Toggle2(range, grid);
                    break;
            }
        }

        private static void Off2(((int, int), (int, int)) range, int[,] grid)
        {
            for (int x = range.Item1.Item1; x <= range.Item2.Item1; x++)
            {
                for (int y = range.Item1.Item2; y <= range.Item2.Item2; y++)
                {
                    if (grid[x, y] > 0) { grid[x, y]--; }
                }
            }
        }

        private static void On2(((int, int), (int, int)) range, int[,] grid)
        {
            for (int x = range.Item1.Item1; x <= range.Item2.Item1; x++)
            {
                for (int y = range.Item1.Item2; y <= range.Item2.Item2; y++)
                {
                    grid[x, y]++;
                }
            }
        }

        private static void Toggle2(((int, int), (int, int)) range, int[,] grid)
        {
            for (int x = range.Item1.Item1; x <= range.Item2.Item1; x++)
            {
                for (int y = range.Item1.Item2; y <= range.Item2.Item2; y++)
                {
                    grid[x, y]+=2;
                }
            }
        }
    }
}
