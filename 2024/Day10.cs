using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day10 : IDay
    {
        private static readonly (int, int)[] directions = { (1, 0), (0, -1), (-1, 0), (0, 1) };

        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[][] grid = new int[lineStr[0].Length][];
            List<(int, int)> trailHeads = new List<(int, int)>();
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new int[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x] - '0';
                    if (grid[x][_y] == 0)
                    {
                        trailHeads.Add((x, _y));
                    }
                }
            }
            List<(int, int)[]> paths = new List<(int, int)[]>();
            for (int i = 0; i < trailHeads.Count; i++)
            {
                paths.Add(new (int, int)[10]);
                paths[paths.Count - 1][0] = trailHeads[i];
                CheckPath(grid, 0, ref paths);
            }
            
            Dictionary<(int, int), HashSet<(int, int)>> uniquePaths = new Dictionary<(int, int), HashSet<(int, int)>>();
            for (int i = 0; i < paths.Count; i++)
            {
                if (uniquePaths.ContainsKey(paths[i][0]) == false)
                {
                    uniquePaths.Add(paths[i][0], new HashSet<(int, int)>());
                }
                uniquePaths[paths[i][0]].Add(paths[i][9]);
            }
            long sum = 0;
            foreach (KeyValuePair<(int, int), HashSet<(int, int)>> trailheadPaths in uniquePaths)
            {
                sum += trailheadPaths.Value.Count;
            }
            return sum;
        }

        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[][] grid = new int[lineStr[0].Length][];
            List<(int, int)> trailHeads = new List<(int, int)>();
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new int[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x] - '0';
                    if (grid[x][_y] == 0)
                    {
                        trailHeads.Add((x, _y));
                    }
                }
            }
            List<(int, int)[]> paths = new List<(int, int)[]>();
            for (int i = 0; i < trailHeads.Count; i++)
            {
                paths.Add(new (int, int)[10]);
                paths[paths.Count - 1][0] = trailHeads[i];
                CheckPath(grid, 0, ref paths);
            }
            return paths.Count;
        }

        private static void CheckPath(int[][] grid, int idx, ref List<(int, int)[]> paths)
        {
            if (idx == 9) { return; }
            int path_idx = paths.Count - 1;
            (int, int) pos = paths[paths.Count - 1][idx];
            for (int i = 0; i < directions.Length; i++)
            {
                if ( pos.Item1 + directions[i].Item1 < 0 || pos.Item1 + directions[i].Item1 >= grid.Length)
                {
                    continue;
                }
                if (pos.Item2 + directions[i].Item2 < 0 || pos.Item2 + directions[i].Item2 >= grid[0].Length)
                {
                    continue;
                }
                if (grid[pos.Item1 + directions[i].Item1][pos.Item2 + directions[i].Item2] == idx + 1)
                {
                    paths.Add(new (int, int)[10]);
                    Array.Copy(paths[path_idx], paths[paths.Count - 1], idx + 1);
                    paths[paths.Count - 1][idx + 1] = (pos.Item1 + directions[i].Item1, pos.Item2 + directions[i].Item2);
                    CheckPath(grid, idx + 1, ref paths);
                }
            }
            paths.RemoveAt(path_idx);
        }

        
    }
}
