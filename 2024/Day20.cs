using _2025;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utils;

namespace _2024
{
    public class Day20 : IDay
    {
        private class Node : IEquatable<Node>
        {
            public (int, int) pos;
            public List<Node> parents;
            public bool usedCheat;

            public Node((int, int) pos, bool usedCheat, List<Node> parents)
            {
                this.pos = pos;
                this.usedCheat = usedCheat;
                this.parents = parents;
            }

            public bool Equals(Node? other)
            {
                if (other == null) { throw new ArgumentNullException("other"); }
                if (other.pos == pos && other.usedCheat == usedCheat) { return true; }
                return false;
            }
        }

        private class Cheat
        {
            public (int, int) pos1;
            public (int, int) pos2;
            public long savedTime;

            public Cheat((int, int) pos1, (int, int) pos2, long savedTime)
            {
                this.pos1 = pos1;
                this.pos2 = pos2;
                this.savedTime = savedTime;
            }
        }

        private static readonly (int, int)[] directions = new (int, int)[] { (1, 0), (0, -1), (-1, 0), (0, 1) };

        public static long Part_1(string input)
        {
            string[] linesStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[linesStr.Length][];
            (int, int) startPos = (0, 0);
            (int, int) endPos = (0, 0);
            for (int x = 0; x < linesStr[0].Length; x++)
            {
                grid[x] = new char[linesStr.Length];
                for (int y = 0; y < linesStr.Length; y++)
                {
                    grid[x][y] = linesStr[y][x];
                    if (grid[x][y] == 'S')
                    {
                        startPos = (x, y);
                        grid[x][y] = '.';
                    }
                    else if (grid[x][y] == 'E')
                    {
                        endPos = (x, y);
                        grid[x][y] = '.';
                    }
                }
            }

            Node current = new Node(startPos, false, new List<Node>());
            List<(int, int)> path = new List<(int, int)>();

            while (true)
            {
                path.Add(current.pos);
                if (current.pos == endPos) { break; }
                current = GetNeighbour(grid, current);
            }

            Dictionary<long, List<Cheat>> cheatsByTimesave = new Dictionary<long, List<Cheat>>();
            for (int i = 0; i < path.Count; i++)
            {
                List<Cheat> cheats = GetCheats(grid, path, i);
                for (int j = 0; j < cheats.Count; j++)
                {
                    if (cheatsByTimesave.ContainsKey(cheats[j].savedTime) == false)
                    {
                        cheatsByTimesave.Add(cheats[j].savedTime, new List<Cheat>());
                    }
                    cheatsByTimesave[cheats[j].savedTime].Add(cheats[j]);
                }
            }

            long sum = 0;

            foreach (KeyValuePair<long, List<Cheat>> cheat in cheatsByTimesave)
            {
                if (cheat.Key >= 100) { sum += cheat.Value.Count; }
            }
            
            return sum;
        }

        public static long Part_2(string input)
        {
            string[] linesStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[linesStr.Length][];
            (int, int) startPos = (0, 0);
            (int, int) endPos = (0, 0);
            for (int x = 0; x < linesStr[0].Length; x++)
            {
                grid[x] = new char[linesStr.Length];
                for (int y = 0; y < linesStr.Length; y++)
                {
                    grid[x][y] = linesStr[y][x];
                    if (grid[x][y] == 'S')
                    {
                        startPos = (x, y);
                        grid[x][y] = '.';
                    }
                    else if (grid[x][y] == 'E')
                    {
                        endPos = (x, y);
                        grid[x][y] = '.';
                    }
                }
            }

            Node current = new Node(startPos, false, new List<Node>());
            List<(int, int)> path = new List<(int, int)>();

            while (true)
            {
                path.Add(current.pos);
                if (current.pos == endPos) { break; }
                current = GetNeighbour(grid, current);
            }

            Dictionary<long, List<Cheat>> cheatsByTimesave = new Dictionary<long, List<Cheat>>();
            for (int i = 0; i < path.Count; i++)
            {
                List<Cheat> cheats = GetBigCheats(grid, path, i);
                for (int j = 0; j < cheats.Count; j++)
                {
                    if (cheatsByTimesave.ContainsKey(cheats[j].savedTime) == false)
                    {
                        cheatsByTimesave.Add(cheats[j].savedTime, new List<Cheat>());
                    }
                    cheatsByTimesave[cheats[j].savedTime].Add(cheats[j]);
                }
            }

            long sum = 0;

            foreach (KeyValuePair<long, List<Cheat>> cheat in cheatsByTimesave)
            {
                if (cheat.Key >= 100) { sum += cheat.Value.Count; }
            }

            return sum;
        }

        private static Node GetNeighbour(char[][] grid, Node current)
        {
            for (int i = 0; i < 4; i++)
            {
                (int, int) neighbourPos = (current.pos.Item1 + directions[i].Item1, current.pos.Item2 + directions[i].Item2);
                if (grid[neighbourPos.Item1][neighbourPos.Item2] == '#') { continue; }
                if (current.parents.Count != 0 && current.parents[0].pos == neighbourPos) { continue; }
                return new Node(neighbourPos, false, new List<Node> { current });
            }
            throw new Exception("Should be able to find neighbour");
        }

        private static List<Cheat> GetCheats(char[][] grid, List<(int, int)> path, int idx)
        {
            (int, int) current = path[idx];
            List<Cheat> cheats = new List<Cheat>();
            for (int i = 0; i < 4; i++)
            {
                (int, int) cheatPos1 = (current.Item1 + directions[i].Item1, current.Item2 + directions[i].Item2);
                if (grid[cheatPos1.Item1][cheatPos1.Item2] != '#') { continue; }
                for (int j = 0; j < 4; j++)
                {
                    (int, int) cheatPos2 = (cheatPos1.Item1 + directions[j].Item1, cheatPos1.Item2 + directions[j].Item2);
                    if (cheatPos2.Item1 < 0 || cheatPos2.Item1 >= grid.Length) { continue; }
                    if (cheatPos2.Item2 < 0 || cheatPos2.Item2 >= grid[0].Length) { continue; }
                    if (grid[cheatPos2.Item1][cheatPos2.Item2] == '#') { continue; }
                    if (cheatPos2 == current) { continue; }
                    cheats.Add(new Cheat(cheatPos1, cheatPos2, path.IndexOf(cheatPos2) - idx - 2));
                }
            }
            
            return cheats;
        }

        private static List<Cheat> GetBigCheats(char[][] grid, List<(int, int)> path, int idx)
        {
            (int, int) cheatPos1 = path[idx];
            List<Cheat> cheats = new List<Cheat>();

            for (int x = cheatPos1.Item1 - 20; x <= cheatPos1.Item1 + 20; x++)
            {
                for (int y = cheatPos1.Item2 - 20; y <= cheatPos1.Item2 + 20; y++)
                {
                    if (x < 0 || x >= grid.Length) { continue; }
                    if (y < 0 || y >= grid[0].Length) { continue; }
                    if (grid[x][y] == '#') { continue; }
                    int dist = GetDist(cheatPos1, (x, y));
                    if (dist > 20) { continue; }
                    long timeSave = path.IndexOf((x, y)) - idx - dist;
                    if (timeSave < 50) { continue; } 
                    cheats.Add(new Cheat(cheatPos1, (x,y), timeSave));
                }
            }

            return cheats;
        }

        private static int GetDist((int, int) pos1, (int, int) pos2)
        {
            return Math.Abs(pos2.Item1 - pos1.Item1) + Math.Abs(pos2.Item2 - pos1.Item2);
        }
    }
}
