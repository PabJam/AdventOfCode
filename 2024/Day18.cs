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
    public class Day18 : IDay
    {
        private static readonly (int, int)[] directions = new (int, int)[] { (1, 0), (0, -1), (-1, 0), (0, 1) };
        private const int gridLen = 71;
        private const int bytesFallen = 1024;

        private class Node : IEquatable<Node>
        {
            public (int, int) pos;
            public int fCost { get => gCost + hCost; }
            public int gCost;
            public int hCost;
            public Node? parent;
            public Node((int, int) pos, int gCost, int hCost, Node? parent)
            {
                this.pos = pos;
                this.gCost = gCost;
                this.hCost = hCost;
                this.parent = parent;
            }

            public bool Equals(Node? other)
            {
                if (other == null) { throw new ArgumentNullException("other is null"); }
                if (other.pos == pos) { return true; }
                return false;
            }
        }
        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[gridLen][];
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            (int, int) startPos = (0, 0);
            (int, int) endPos = (gridLen - 1, gridLen - 1);
            for (int x = 0; x < gridLen; x++)
            {
                grid[x] = new char[gridLen];
                for (int y = 0; y < gridLen; y++)
                {
                    grid[x][y] = '.';
                }
            }
            List<(int,int)> obstructed = new List<(int,int)>();
            for (int i = 0; i < bytesFallen; i++)
            {
                Match match = Regex.Match(lineStr[i], @"(\d+),(\d+)");
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                obstructed.Add((x,y));
                grid[x][y] = '#';
            }

            Node current = new Node(startPos, 0, HCost(startPos, endPos), null);
            while (true)
            {
                closed.Add(current);
                if (current.pos == endPos) { break; }

                for (int i = 0; i < 4; i++)
                {
                    (int, int) nextPos = (current.pos.Item1 + directions[i].Item1, current.pos.Item2 + directions[i].Item2);
                    if (nextPos.Item1 < 0 || nextPos.Item1 >= gridLen) { continue; }
                    if (nextPos.Item2 < 0 || nextPos.Item2 >= gridLen) { continue; }
                    if (grid[nextPos.Item1][nextPos.Item2] == '#') { continue; }
                    Node neighbour = new Node(nextPos, current.gCost + 1, HCost(nextPos, endPos), current);
                    if (closed.Contains(neighbour) == false && open.Contains(neighbour) == false)
                    {
                        open.Add(neighbour);
                    }
                }

                current = GetOpen(ref open);
            }

            List<Node> path = new List<Node>();
            while (true)
            {
                path.Add(current);
                if (current.parent == null) { break; }
                current = current.parent;
            }

            for (int i = 0; i < path.Count; i++)
            {
                grid[path[i].pos.Item1][path[i].pos.Item2] = 'O';
            }
            
            StringBuilder render = new StringBuilder();
            for (int y = 0; y < gridLen; y++)
            {
                for (int x = 0; x < gridLen; x++)
                {
                    render.Append(grid[x][y]);
                }
                render.Append("\r\n");
            }
            Console.WriteLine(render.ToString());
            return path.Count - 1;
        }



        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[gridLen][];
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            (int, int) startPos = (0, 0);
            (int, int) endPos = (gridLen - 1, gridLen - 1);
            List<(int, int)> obstructed = new List<(int, int)>();
            List<(int, int)> path = new List<(int, int)>();
            for (int x = 0; x < gridLen; x++)
            {
                grid[x] = new char[gridLen];
                for (int y = 0; y < gridLen; y++)
                {
                    grid[x][y] = '.';
                }
            }

            for (int i = 0; i < lineStr.Length; i++)
            {
                obstructed.Clear();
                for (int j = 0; j < i; j++)
                {
                    Match match = Regex.Match(lineStr[j], @"(\d+),(\d+)");
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    obstructed.Add((x, y));
                    grid[obstructed[j].Item1][obstructed[j].Item2] = '#';
                }
                

                open.Clear();
                closed.Clear();
                Node current = new Node(startPos, 0, HCost(startPos, endPos), null);
                while (true)
                {
                    closed.Add(current);
                    if (current.pos == endPos) { break; }

                    for (int j = 0; j < 4; j++)
                    {
                        (int, int) nextPos = (current.pos.Item1 + directions[j].Item1, current.pos.Item2 + directions[j].Item2);
                        if (nextPos.Item1 < 0 || nextPos.Item1 >= gridLen) { continue; }
                        if (nextPos.Item2 < 0 || nextPos.Item2 >= gridLen) { continue; }
                        if (grid[nextPos.Item1][nextPos.Item2] == '#') { continue; }
                        Node neighbour = new Node(nextPos, current.gCost + 1, HCost(nextPos, endPos), current);
                        if (closed.Contains(neighbour) == false && open.Contains(neighbour) == false)
                        {
                            open.Add(neighbour);
                        }
                    }

                    if (open.Count == 0) 
                    {
                        Console.WriteLine(obstructed[obstructed.Count - 1].Item1 + "," + obstructed[obstructed.Count - 1].Item2);
                        return 0;
                    }
                    current = GetOpen(ref open);
                }

                path.Clear();
                while (true)
                {
                    path.Add(current.pos);
                    if (current.parent == null) { break; }
                    current = current.parent;
                }

                for (int j = i + 1; j < lineStr.Length; j++)
                {
                    Match match = Regex.Match(lineStr[j], @"(\d+),(\d+)");
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    if (path.Contains((x,y)) == true)
                    {
                        i = j - 1;
                        break;
                    }
                }

            }

            return -1;
        }

        private static int HCost((int, int) pos, (int, int) end)
        {
            return Math.Abs(end.Item1 - pos.Item1) + Math.Abs(end.Item2 - pos.Item2);
        }

        private static Node GetOpen(ref List<Node> open)
        {
            int idx = 0;
            Node current = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].fCost < current.fCost)
                {
                    current = open[i];
                    idx = i;
                }
            }
            open.RemoveAt(idx);
            return current;
        }

    }
}
