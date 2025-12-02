using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public class Day08 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                    char character = grid[x][_y];
                    if (character == '.') { continue; }
                    if (antennas.ContainsKey(character) == false)
                    {
                        antennas.Add(character, new List<(int, int)>());
                    }
                    antennas[character].Add((x, _y));
                }
            }
            HashSet<(int, int)> signals = new HashSet<(int, int)>();
            foreach(KeyValuePair<char,List<(int, int)>> kvp in antennas)
            {
                signals.UnionWith(GetSignalsFromAntennas(kvp.Value, grid.Length, grid[0].Length));
            }

            return signals.Count;
        }

        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                    char character = grid[x][_y];
                    if (character == '.') { continue; }
                    if (antennas.ContainsKey(character) == false)
                    {
                        antennas.Add(character, new List<(int, int)>());
                    }
                    antennas[character].Add((x, _y));
                }
            }
            HashSet<(int, int)> signals = new HashSet<(int, int)>();
            foreach (KeyValuePair<char, List<(int, int)>> kvp in antennas)
            {
                signals.UnionWith(GetAllSignalsFromAntennas(kvp.Value, grid.Length, grid[0].Length));
            }

            return signals.Count;
        }

        private static HashSet<(int, int)> GetSignalsFromAntennas(List<(int, int)> antennas, int gridSizeX, int gridSizeY)
        {
            HashSet<(int, int)> signals = new HashSet<(int, int)>();

            for (int i = 0; i < antennas.Count; i++) 
            {
                for (int j = 0; j < antennas.Count; j++)
                {
                    if (i == j) { continue; }
                    (int, int) vec;
                    (int, int) pos;
                    vec.Item1 = 2 * (antennas[j].Item1 - antennas[i].Item1);
                    vec.Item2 = 2* (antennas[j].Item2 - antennas[i].Item2);
                    pos.Item1 = antennas[i].Item1 + vec.Item1;
                    pos.Item2 = antennas[i].Item2 + vec.Item2;
                    if (pos.Item1 < 0 || pos.Item1 >= gridSizeX) { continue; }
                    if (pos.Item2 < 0 || pos.Item2 >= gridSizeY) { continue; }
                    signals.Add(pos);
                }
            }

            return signals;
        }

        private static HashSet<(int, int)> GetAllSignalsFromAntennas(List<(int, int)> antennas, int gridSizeX, int gridSizeY)
        {
            HashSet<(int, int)> signals = new HashSet<(int, int)>();

            for (int i = 0; i < antennas.Count; i++)
            {
                for (int j = 0; j < antennas.Count; j++)
                {
                    if (i == j) { continue; }
                    (int, int) vec;
                    (int, int) pos;
                    int counter = 0;
                    while(true)
                    {
                        counter++;
                        vec.Item1 = counter * (antennas[j].Item1 - antennas[i].Item1);
                        vec.Item2 = counter * (antennas[j].Item2 - antennas[i].Item2);
                        pos.Item1 = antennas[i].Item1 + vec.Item1;
                        pos.Item2 = antennas[i].Item2 + vec.Item2;
                        if (pos.Item1 < 0 || pos.Item1 >= gridSizeX) { break; }
                        if (pos.Item2 < 0 || pos.Item2 >= gridSizeY) { break; }
                        signals.Add(pos);
                    }
                    
                }
            }

            return signals;
        }
    }
}
