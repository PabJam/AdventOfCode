using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public class Day15 : IDay
    {
        private static readonly Dictionary<char, (int, int)> directions = new Dictionary<char, (int, int)>()
        {
            { '>', (1,0)},
            { 'v', (0, -1)},
            { '<', (-1, 0)},
            { '^', (0, 1)},
        };

        public static long Part_1(string input)
        {
            string[] inputStr = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] gridLines = inputStr[0].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string sequence = string.Join("", inputStr[1].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));

            char[][] grid = new char[gridLines[0].Length][];
            (int, int) robotPos = (0, 0);
            for (int x = 0; x < gridLines[0].Length; x++)
            {
                grid[x] = new char[gridLines.Length];
                for (int y = gridLines.Length - 1; y > -1; y--)
                {
                    int _y = gridLines.Length - 1 - y;
                    grid[x][_y] = gridLines[y][x];
                    if (grid[x][_y] == '@')
                    {
                        robotPos = (x, _y);
                    }
                }
            }

            for (int i = 0; i < sequence.Length; i++)
            {
                bool moved = Move(ref grid, robotPos, sequence[i]);
                if (moved == false) { continue; }
                robotPos = (robotPos.Item1 + directions[sequence[i]].Item1, robotPos.Item2 + directions[sequence[i]].Item2);
            }


            long sum = 0;
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] != 'O') { continue; }
                    sum += x + (grid[x].Length - y - 1) * 100;
                }
            }
            return sum;
        }

        public static long Part_2(string input)
        {
            string[] inputStr = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] gridLines = inputStr[0].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string sequence = string.Join("", inputStr[1].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));

            char[][] grid = new char[gridLines[0].Length * 2][];
            (int, int) robotPos = (0, 0);
            for (int x = 0; x < gridLines[0].Length; x++)
            {
                grid[2 * x] = new char[gridLines.Length];
                grid[2 * x + 1] = new char[gridLines.Length];
                for (int y = gridLines.Length - 1; y > -1; y--)
                {
                    int _y = gridLines.Length - 1 - y;
                    switch (gridLines[y][x])
                    {
                        case '#':
                            grid[2 * x][_y] = '#';
                            grid[2 * x + 1][_y] = '#';
                            break;
                        case '.':
                            grid[2 * x][_y] = '.';
                            grid[2 * x + 1][_y] = '.';
                            break;
                        case 'O':
                            grid[2 * x][_y] = '[';
                            grid[2 * x + 1][_y] = ']';
                            break;
                        case '@':
                            grid[2 * x][_y] = '@';
                            robotPos = (2 * x, _y);
                            grid[2 * x + 1][_y] = '.';
                            break;
                    }
                }
            }

            //int i = 0;
            //sequence = "";
            for (int i = 0; i < sequence.Length; i++)//while (true)
            {

                // Move your self
                //string render = "";
                //for (int y = grid[0].Length - 1; y > -1; y--)
                //{
                //    for (int x = 0; x < grid.Length; x++)
                //    {
                //        render += grid[x][y];
                //    }
                //    render += "\r\n";
                //}
                //Console.Clear();
                //Console.WriteLine(render);
                //ConsoleKeyInfo keyInfo = Console.ReadKey();
                //switch (keyInfo.Key)
                //{
                //    case ConsoleKey.UpArrow:
                //        sequence += "^";
                //        break;
                //    case ConsoleKey.RightArrow:
                //        sequence += ">";
                //        break;
                //    case ConsoleKey.DownArrow:
                //        sequence += "v";
                //        break;
                //    case ConsoleKey.LeftArrow:
                //        sequence += "<";
                //        break;
                //}
                //Console.Clear();

                // Check sequence
                //Console.WriteLine(sequence[i]);
                //string render = "";
                //for (int y = grid[0].Length - 1; y > -1; y--)
                //{
                //    for (int x = 0; x < grid.Length; x++)
                //    {
                //        render += grid[x][y];
                //    }
                //    render += "\r\n";
                //}
                //Console.WriteLine(render);
                

                bool moved = MoveThin(ref grid, robotPos, sequence[i]);
                if (moved == true) { robotPos = (robotPos.Item1 + directions[sequence[i]].Item1, robotPos.Item2 + directions[sequence[i]].Item2); }
                
                //i++;
            }

            

            long sum = 0;
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] != '[') { continue; }
                    sum += x + (grid[x].Length - y - 1) * 100;
                }
            }
            return sum;
        }

        private static bool Move(ref char[][] grid, (int, int) pos, char dir)
        {
            (int, int) nexPos = (pos.Item1 + directions[dir].Item1, pos.Item2 + directions[dir].Item2);
            if (grid[nexPos.Item1][nexPos.Item2] == '#') { return false; }
            if (grid[nexPos.Item1][nexPos.Item2] == 'O') { Move(ref grid, nexPos, dir); }
            if (grid[nexPos.Item1][nexPos.Item2] == '.')
            {
                grid[nexPos.Item1][nexPos.Item2] = grid[pos.Item1][pos.Item2];
                grid[pos.Item1][pos.Item2] = '.';
                return true;
            }
            return false;
        }

        private static bool MoveThin(ref char[][] grid, (int, int) pos, char dir)
        {
            (int, int) nexPos = (pos.Item1 + directions[dir].Item1, pos.Item2 + directions[dir].Item2);
            if (directions[dir].Item2 == 0)
            {
                if (grid[nexPos.Item1][nexPos.Item2] == '#') { return false; }
                if (grid[nexPos.Item1][nexPos.Item2] == ']') { MoveThin(ref grid, nexPos, dir); }
                if (grid[nexPos.Item1][nexPos.Item2] == '[') { MoveThin(ref grid, nexPos, dir); }
                if (grid[nexPos.Item1][nexPos.Item2] == '.')
                {
                    grid[nexPos.Item1][nexPos.Item2] = grid[pos.Item1][pos.Item2];
                    grid[pos.Item1][pos.Item2] = '.';
                    return true;
                }
                return false;
            }
            else
            {
                if (grid[nexPos.Item1][nexPos.Item2] == '#') { return false; }
                if (grid[nexPos.Item1][nexPos.Item2] == ']') { MoveWide(ref grid, (nexPos.Item1 - 1, nexPos.Item2), dir, false); }
                if (grid[nexPos.Item1][nexPos.Item2] == '[') { MoveWide(ref grid, nexPos, dir, false); }
                if (grid[nexPos.Item1][nexPos.Item2] == '.')
                {
                    grid[nexPos.Item1][nexPos.Item2] = grid[pos.Item1][pos.Item2];
                    grid[pos.Item1][pos.Item2] = '.';
                    return true;
                }
                return false;
            }
        }

        private static bool MoveWide(ref char[][] grid, (int, int) posL, char dir, bool scouting)
        {
            (int, int) posR = (posL.Item1 + 1, posL.Item2);
            (int, int) nextPosL = (posL.Item1 + directions[dir].Item1, posL.Item2 + directions[dir].Item2);
            (int, int) nextPosR = (posR.Item1 + directions[dir].Item1, posR.Item2 + directions[dir].Item2);
            string front = grid[nextPosL.Item1][nextPosL.Item2].ToString() + grid[nextPosR.Item1][nextPosR.Item2].ToString();
            bool canMove = false;
            if (front.Contains('#')) { return false; }
            else if (front == "[]") { canMove = MoveWide(ref grid, nextPosL, dir, scouting); }
            else if (front == "].") { canMove = MoveWide(ref grid, (nextPosL.Item1 - 1, nextPosL.Item2), dir, scouting); }
            else if (front == ".[") { canMove = MoveWide(ref grid, nextPosR, dir, scouting); }
            else if (front == "][") 
            { 
                canMove = MoveWide(ref grid, (nextPosL.Item1 - 1, nextPosL.Item2), dir, true); 
                canMove = canMove && MoveWide(ref grid, nextPosR, dir, true);
                if (canMove)
                {
                    MoveWide(ref grid, (nextPosL.Item1 - 1, nextPosL.Item2), dir, false);
                    MoveWide(ref grid, nextPosR, dir, false);
                }
            }
            front = grid[nextPosL.Item1][nextPosL.Item2].ToString() + grid[nextPosR.Item1][nextPosR.Item2].ToString();
            if (front == "..")
            {
                canMove = true;
                if (scouting == false)
                {
                    grid[nextPosL.Item1][nextPosL.Item2] = grid[posL.Item1][posL.Item2];
                    grid[nextPosR.Item1][nextPosR.Item2] = grid[posR.Item1][posR.Item2];
                    grid[posL.Item1][posL.Item2] = '.';
                    grid[posR.Item1][posR.Item2] = '.';
                }
                
            }
            return canMove;
        }


    }
}
