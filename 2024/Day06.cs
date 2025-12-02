using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day06 : IDay
    {

        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            HashSet<(int, int)> visited = new HashSet<(int, int)> ();
            (int, int) pos = (0, 0);
            (int, int) direction = (0, 1);
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y; 
                    grid[x][_y] = lineStr[y][x];
                    if (grid[x][_y] == '^')
                    {
                        pos = (x, _y);
                    }
                }
            }

            visited.Add(pos);
            while(true)
            {
                (int, int) nextPos = (pos.Item1 + direction.Item1, pos.Item2 + direction.Item2);
                if (nextPos.Item1 < 0 || nextPos.Item1 >= grid.Length) { break; }
                if (nextPos.Item2 < 0 || nextPos.Item2 >= grid[0].Length) { break; }
                if (grid[nextPos.Item1][nextPos.Item2] == '.')
                {
                    grid[nextPos.Item1][nextPos.Item2] = '^';
                    grid[pos.Item1][pos.Item2] = '.';
                    pos = nextPos;
                    visited.Add(pos);
                    continue;
                }
                if (grid[nextPos.Item1][nextPos.Item2] == '#')
                {
                    direction = (direction.Item2, direction.Item1 * -1);
                    continue;
                }
            }
            return visited.Count;
        }

        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            (int, int) pos = (0, 0);
            (int, int) startPos = (0, 0);
            (int, int) direction = (0, 1);
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                    if (grid[x][_y] == '^')
                    {
                        startPos = (x, _y);
                        pos = startPos;
                    }
                }
            }

            HashSet<(int, int)> obstacles = new HashSet<(int, int)>();
            (int, int) obstacle = (0, 0);
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[0].Length; y++)
                {
                    if (grid[x][y] == '.')
                    {
                        pos = startPos;
                        direction = (0, 1);
                        grid[x][y] = '#';
                        HashSet<((int, int), (int, int))> visited = new HashSet<((int, int), (int, int))>();
                        obstacle = (x, y);
                        //if (obstacle == (3,3))
                        //{
                        //    Console.WriteLine("should loop");
                        //}
                        while (true)
                        {
                            if(!visited.Add((pos, direction)))
                            {
                                obstacles.Add(obstacle);
                                break;
                            }
                            pos = (pos.Item1 + direction.Item1, pos.Item2 + direction.Item2);
                            if (pos.Item1 < 0 || pos.Item1 >= grid.Length) { break; }
                            if (pos.Item2 < 0 || pos.Item2 >= grid[0].Length) { break; }
                            if (grid[pos.Item1][pos.Item2] == '.')
                            {
                                continue;
                            }
                            if (grid[pos.Item1][pos.Item2] == '#')
                            {
                                pos = (pos.Item1 - direction.Item1, pos.Item2 - direction.Item2);
                                direction = (direction.Item2, direction.Item1 * -1);
                                continue;
                            }
                        }
                        grid[x][y] = '.';
                    }
                }
            }

           
            //int counter = 0;
            //while (true)
            //{
            //    //counter++;
            //    //Console.Write("\r" + ((double)counter / 5131.0) * 100 + "%          ");
            //    visited.Add((pos, direction));
            //    
            //    pos = (pos.Item1 + direction.Item1, pos.Item2 + direction.Item2);
            //    if (pos.Item1 < 0 || pos.Item1 >= grid.Length) { break; }
            //    if (pos.Item2 < 0 || pos.Item2 >= grid[0].Length) { break; }
            //
            //    if (grid[pos.Item1][pos.Item2] == '.')
            //    {
            //        (int, int) obstacle = pos;
            //        (int, int) _pos = (pos.Item1 - direction.Item1, pos.Item2 - direction.Item2);
            //        (int, int) _dir = (direction.Item2, direction.Item1 * -1);
            //        grid[obstacle.Item1][obstacle.Item2] = '#';
            //        HashSet<((int, int), (int, int))> _visited = new HashSet<((int, int), (int, int))>(visited);
            //        while (true)
            //        {
            //            if (visited.Contains((_pos, _dir)))
            //            {
            //                obstacles.Add(obstacle);
            //                break;
            //            }
            //            if (_visited.Contains((_pos, _dir)))
            //            {
            //                //obstacles.Add(obstacle);
            //                break;
            //            }
            //            _visited.Add((_pos, _dir));
            //            _pos = (_pos.Item1 + _dir.Item1, _pos.Item2 + _dir.Item2);
            //            if (_pos.Item1 < 0 || _pos.Item1 >= grid.Length) 
            //            { break; }
            //            if (_pos.Item2 < 0 || _pos.Item2 >= grid[0].Length) 
            //            { break; }
            //
            //            if (grid[_pos.Item1][_pos.Item2] == '#')
            //            {
            //                _pos = (_pos.Item1 - _dir.Item1, _pos.Item2 - _dir.Item2);
            //                _dir = (_dir.Item2, _dir.Item1 * -1);
            //                continue;
            //            }
            //
            //        }
            //        grid[obstacle.Item1][obstacle.Item2] = '.';
            //        continue;
            //    }
            //    if (grid[pos.Item1][pos.Item2] == '#')
            //    {
            //        pos = (pos.Item1 - direction.Item1, pos.Item2 - direction.Item2);
            //        direction = (direction.Item2, direction.Item1 * -1);
            //        continue;
            //    }
            //}
            //if (obstacles.Contains(startPos)) { obstacles.Remove(startPos); }
            return obstacles.Count;
        }


    }
}
