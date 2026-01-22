using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day03 : IDay
    {
        public static long Part_1(string input)
        {
            string[] map = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            List<long> connectedNums = new List<long>();
            long count = 0;
            List<Vector2Int> usedCoords = new List<Vector2Int>();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == '.')
                    {
                        continue;
                    }
                    if (char.IsDigit(map[y][x]) == true)
                    {
                        continue;
                    }
                    long[] neighbours = GetNeighbours(map, new Vector2Int(x, y), out Vector2Int[] usedFirstCoords);
                    for (int i = 0; i < neighbours.Length; i++)
                    {
                        if (usedCoords.Contains(usedFirstCoords[i]) == false)
                        {
                            connectedNums.Add(neighbours[i]);
                            usedCoords.Add(usedFirstCoords[i]);
                        }
                    }

                }
            }
            for (int i = 0; i < connectedNums.Count; i++)
            {
                count += connectedNums[i];
            }

            return count;
        }

        public static long Part_2(string input)
        {
            string[] map = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long count = 0;
            for (long y = 0; y < map.Length; y++)
            {
                for (long x = 0; x < map[y].Length; x++)
                {
                    if (map[y][(int)x] != '*')
                    {
                        continue;
                    }
                    long[] neighbours = GetNeighbours(map, new Vector2Int(x, y), out Vector2Int[] usedFirstCoords);
                    if (neighbours.Length != 2)
                    {
                        continue;
                    }
                    count += neighbours[0] * neighbours[1];

                }
            }
            return count;
        }

        private static long[] GetNeighbours(string[] map, Vector2Int coords, out Vector2Int[] usedFirstCoords)
        {
            List<long> neighbours = new List<long>();
            List<Vector2Int> usedCoords = new List<Vector2Int>();
            List<Vector2Int> usedFirstCoordsL = new List<Vector2Int>();
            for (long x = -1; x < 2; x++)
            {
                for (long y = -1; y < 2; y++)
                {
                    if (coords.x + x < 0) { continue; }
                    if (coords.y + y < 0) { continue; }
                    if (coords.y + y >= map.Length) { continue; }
                    if (coords.x + x >= map[coords.y + y].Length) { continue; }
                    if (x == 0 && y == 0) { continue; }
                    if (char.IsDigit(map[coords.y + y][(int)(coords.x + x)]) == true)
                    {
                        List<Vector2Int> digitPos = new List<Vector2Int>();
                        digitPos.Add(new Vector2Int(coords.x + x, coords.y + y));
                        int count = 1;
                        if (usedCoords.Contains(digitPos[0]) == true)
                        {
                            continue;
                        }
                        while (true)
                        {
                            if (coords.x + x + count >= map[coords.y + y].Length) { break; }
                            if (char.IsDigit(map[coords.y + y][(int)(coords.x + x + count)]) == true)
                            {
                                digitPos.Add(new Vector2Int(coords.x + x + count, coords.y + y));
                            }
                            else { break; }
                            count++;
                        }
                        count = -1;
                        while (true)
                        {
                            if (coords.x + x + count < 0) { break; }
                            if (char.IsDigit(map[coords.y + y][(int)(coords.x + x) + count]) == true)
                            {
                                digitPos.Insert(0, new Vector2Int(coords.x + x + count, coords.y + y));
                            }
                            else { break; }
                            count--;
                        }
                        StringBuilder numStr = new StringBuilder();
                        usedCoords.AddRange(digitPos);
                        usedFirstCoordsL.Add(digitPos[0]);
                        for (int i = 0; i < digitPos.Count; i++)
                        {
                            numStr.Append(map[digitPos[i].y][(int)(digitPos[i].x)]);
                        }
                        neighbours.Add(Int32.Parse(numStr.ToString()));
                    }
                }
            }
            usedFirstCoords = usedFirstCoordsL.ToArray();
            return neighbours.ToArray();
        }
    }
}
