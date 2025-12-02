using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public class Day14 : IDay
    {
        private static readonly (int, int) gridSize = (101, 103);

        public static long Part_1(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            ((int, int), (int, int))[] robots = new ((int, int), (int, int))[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
                robots[i].Item1.Item1 = int.Parse(match.Groups[1].Value);
                robots[i].Item1.Item2 = int.Parse(match.Groups[2].Value);
                robots[i].Item2.Item1 = int.Parse(match.Groups[3].Value);
                robots[i].Item2.Item2 = int.Parse(match.Groups[4].Value);
            }
            long TL = 0, TR = 0, BL = 0, BR = 0;
            for (int i = 0; i < robots.Length; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    robots[i].Item1 = MoveRobot(robots[i].Item1, robots[i].Item2);
                }
                if (robots[i].Item1.Item1 > gridSize.Item1 / 2)
                {
                    if (robots[i].Item1.Item2 > gridSize.Item2 / 2)
                    {
                        BR++;
                    }
                    else if (robots[i].Item1.Item2 < gridSize.Item2 / 2)
                    {
                        TR++;
                    }
                }
                else if (robots[i].Item1.Item1 < gridSize.Item1 / 2)
                {
                    if (robots[i].Item1.Item2 > gridSize.Item2 / 2)
                    {
                        BL++;
                    }
                    else if (robots[i].Item1.Item2 < gridSize.Item2 / 2)
                    {
                        TL++;
                    }
                }
            }


            return BR * TR * BL * TL;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            ((int, int), (int, int))[] robots = new ((int, int), (int, int))[lines.Length];
            char[,] grid = new char[gridSize.Item1, gridSize.Item2];
            List<string> possiblePrints = new List<string> ();
            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
                robots[i].Item1.Item1 = int.Parse(match.Groups[1].Value);
                robots[i].Item1.Item2 = int.Parse(match.Groups[2].Value);
                robots[i].Item2.Item1 = int.Parse(match.Groups[3].Value);
                robots[i].Item2.Item2 = int.Parse(match.Groups[4].Value);
            }
            while (true)
            {
                
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        grid[x, y] = '.';
                    }
                }
                for (int i = 0; i < robots.Length; i++)
                {
                    grid[robots[i].Item1.Item1, robots[i].Item1.Item2] = '#';
                    robots[i].Item1 = MoveRobot(robots[i].Item1, robots[i].Item2);
                }
                string render = "";

                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    for (int x = 0; x < grid.GetLength(0); x++)
                    {
                        render += grid[x, y];
                    }
                    render += "\r\n";
                    
                }

                if (possiblePrints.Contains(render) == true)
                {
                    break;
                }
                else
                {
                    possiblePrints.Add(render);
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < possiblePrints.Count; i++)
            {
                stringBuilder.Append(i.ToString() + "\r\n");
                stringBuilder.Append(possiblePrints[i]);
            }
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Day14Part2.txt", stringBuilder.ToString());
            return 0;
        }

        private static (int,int) MoveRobot((int, int) pos, (int, int) velocity) 
        {
            pos.Item1 += velocity.Item1;
            pos.Item2 += velocity.Item2;
            if (pos.Item1 >= gridSize.Item1) { pos.Item1 -= gridSize.Item1; }
            if (pos.Item1 < 0) { pos.Item1 += gridSize.Item1; }
            if (pos.Item2 >= gridSize.Item2) { pos.Item2 -= gridSize.Item2; }
            if (pos.Item2 < 0) { pos.Item2 += gridSize.Item2; }
            return pos;
        }
    }
}
