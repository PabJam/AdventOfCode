using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public class Day12 : IDay
    {
        private static readonly (int, int)[] directions = { (1, 0), (0, -1), (-1, 0), (0, 1) };

        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                }
            }
            List<HashSet<(int, int)>> fields = new List<HashSet<(int, int)>>();
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[0].Length; y++)
                {
                    bool containsPos = false;
                    foreach (HashSet<(int, int)> field in fields)
                    {
                        if (field.Contains((x, y))) { containsPos = true; break; }
                    }
                    if (containsPos == true) { continue; }
                    fields.Add(GetField(grid, (x, y)));
                }
            }
            List<List<(int, int)>> fences = new List<List<(int, int)>>();
            foreach (HashSet<(int, int)> field in fields)
            {
                fences.Add(GetFence(field));
            }

            long sum = 0;
            for (int i = 0; i < fields.Count; i++)
            {
                sum += fields[i].Count * fences[i].Count;
            }
            return sum;
        }

        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                }
            }
            List<HashSet<(int, int)>> fields = new List<HashSet<(int, int)>>();
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[0].Length; y++)
                {
                    bool containsPos = false;
                    foreach (HashSet<(int, int)> field in fields)
                    {
                        if (field.Contains((x, y))) { containsPos = true; break; }
                    }
                    if (containsPos == true) { continue; }
                    fields.Add(GetField(grid, (x, y)));
                }
            }
            List<List<((int, int), (int, int))>> fences = new List<List<((int, int), (int, int))>>();
            foreach (HashSet<(int, int)> field in fields)
            {
                fences.Add(GetFenceWithDirection(field));
            }

            long sum = 0;
            for (int i = 0; i < fields.Count; i++)
            {
                sum += fields[i].Count * GetSides(fences[i]);
            }
            return sum;
        }

        private static HashSet<(int, int)> GetField(char[][] grid, (int, int) startPos)
        {
            HashSet<(int, int)> result = new HashSet<(int, int)>();
            List<(int,int)> open = new List<(int, int)>();
            open.Add(startPos);
            char id = grid[startPos.Item1][startPos.Item2];
            while (open.Count > 0) 
            {
                for (int i = 0; i < directions.Length; i++)
                {
                    (int, int) neighbour = (open[0].Item1 + directions[i].Item1, open[0].Item2 + directions[i].Item2);
                    if (DataUtils.InBound(neighbour, grid.Length, grid[0].Length) == false) { continue; }
                    if (grid[neighbour.Item1][neighbour.Item2] != id) { continue; }
                    if (result.Contains(neighbour)) { continue; }
                    if (open.Contains(neighbour)) { continue; }
                    open.Add(neighbour);
                }
                result.Add(open[0]);
                open.RemoveAt(0);
            }


            return result;
        }

        private static List<(int, int)> GetFence(HashSet<(int,int)> field)
        {
            List<(int, int)> fence = new List<(int, int)>();
            foreach ((int,int) pos in field)
            {
                for (int i = 0; i < directions.Length; i++)
                {
                    (int, int) neighbour = (pos.Item1 + directions[i].Item1, pos.Item2 + directions[i].Item2);
                    if (field.Contains(neighbour)) { continue; }
                    fence.Add(neighbour);
                }
            }
            return fence;
        }

        private static List<((int, int), (int, int))> GetFenceWithDirection(HashSet<(int, int)> field)
        {
            List<((int, int), (int, int))> fence = new List<((int, int), (int, int))>();
            foreach ((int, int) pos in field)
            {
                for (int i = 0; i < directions.Length; i++)
                {
                    (int, int) neighbour = (pos.Item1 + directions[i].Item1, pos.Item2 + directions[i].Item2);
                    if (field.Contains(neighbour)) { continue; }
                    fence.Add((neighbour, directions[i]));
                }
            }
            return fence;
        }

        private static int GetSides(List<((int, int), (int, int))> fence)
        {
            List<((int, int), (int, int))> open = new List<((int, int), (int, int))>(fence);
            List<HashSet<(int, int)>> sides = new List<HashSet<(int, int)>>();
            while (open.Count > 0)
            {
                sides.Add(new HashSet<(int, int)>());
                sides[sides.Count - 1].Add(open[0].Item1);
                (int, int) baseDirection = (open[0].Item2.Item2, open[0].Item2.Item1 * -1);
                (int, int) direction = baseDirection;
                while (true)
                {
                    ((int, int), (int, int)) posDir = ((open[0].Item1.Item1 + direction.Item1, open[0].Item1.Item2 + direction.Item2), open[0].Item2);
                    if (open.Contains(posDir))
                    {
                        sides[sides.Count - 1].Add(posDir.Item1);
                        open.Remove(posDir);
                    }
                    else { break; }
                    direction = (direction.Item1 + baseDirection.Item1, direction.Item2 + baseDirection.Item2);
                }
                baseDirection = (open[0].Item2.Item2 * -1, open[0].Item2.Item1);
                direction = baseDirection;
                while (true)
                {
                    ((int, int), (int, int)) posDir = ((open[0].Item1.Item1 + direction.Item1, open[0].Item1.Item2 + direction.Item2), open[0].Item2);
                    if (open.Contains(posDir))
                    {
                        sides[sides.Count - 1].Add(posDir.Item1);
                        open.Remove(posDir);
                    }
                    else { break; }
                    direction = (direction.Item1 + baseDirection.Item1, direction.Item2 + baseDirection.Item2);
                }
                open.RemoveAt(0);
            }

            return sides.Count;
        }
    }
}
