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
    public class Day16 : IDay
    {

        private static readonly (int, int)[] directions = new (int, int)[] {(1, 0), (0, -1), (-1, 0), (0, 1)};

        private class Node : IEquatable<Node>
        {
            public (int, int) pos;
            public (int, int) direction;
            public int gCost;
            public int hCost;
            public HashSet<Node> parents;
            public Node((int, int) pos, (int, int) direction, int gCost, int hCost, HashSet<Node> parents)
            {
                this.pos = pos;
                this.direction = direction;
                this.gCost = gCost;
                this.hCost = hCost;
                this.parents = parents;
            }

            public bool Equals(Node? other)
            {
                if (other == null) { throw new ArgumentNullException("other"); }
                if (other.pos == pos && other.direction == direction) { return true; }
                return false;
            }
        }
        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            (int, int) startPos = (0,0);
            (int, int) endPos = (0,0);
            (int, int) direction = (1,0);
   
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                    if (grid[x][_y] == 'S') { startPos = (x, _y); grid[x][_y] = '.'; }
                    else if (grid[x][_y] == 'E') { endPos = (x, _y); grid[x][_y] = '.'; }
                }
            }

            Node current = new Node(startPos, direction, 0, HCost(startPos, endPos), new HashSet<Node>());
            while (true)
            {
                closed.Add(current);
                if (current.pos == endPos) { break; }

                for (int i = 0; i < 4; i++)
                {
                    (int, int) nextPos = (current.pos.Item1 + direction.Item1, current.pos.Item2 + direction.Item2);
                    
                    if (grid[nextPos.Item1][nextPos.Item2] != '#')
                    {
                        int cost = current.gCost + 1;
                        if ((direction.Item1, direction.Item2) == (-current.direction.Item1, -current.direction.Item2))
                        {
                            cost += 2000;
                        }
                        else if (direction == current.direction)
                        {
                            cost += 0;
                        }
                        else
                        {
                            cost += 1000;
                        }
                        Node neighbour = new Node(nextPos, direction, cost, HCost(nextPos, endPos), new HashSet<Node> { current });
                        if (closed.Contains(neighbour) == false)
                        {
                            open.Add(neighbour);
                        }
                    }    
                    direction = (-direction.Item2, direction.Item1);
                }


                current = open[0];
                int idx = 0;
                for (int i = 1; i < open.Count; i++)
                {
                    if (open[i].gCost + open[i].hCost < current.gCost + current.hCost)
                    {
                        current = open[i];
                        idx = i;
                    }
                }
                open.RemoveAt(idx);
                direction = current.direction;
            }


            StringBuilder render = new StringBuilder();
            Node trace = current;
            grid[trace.pos.Item1][trace.pos.Item2] = 'E';
            while (true)
            {
                if (trace.parents.Count == 0) { break; }
                foreach (Node parent in trace.parents)
                {
                    trace = parent;
                }

                switch (trace.direction)
                {
                    case (1,0):
                        grid[trace.pos.Item1][trace.pos.Item2] = '>';
                        break;
                    case (0, -1):
                        grid[trace.pos.Item1][trace.pos.Item2] = 'v';
                        break;
                    case (-1, 0):
                        grid[trace.pos.Item1][trace.pos.Item2] = '<';
                        break;
                    case (0, 1):
                        grid[trace.pos.Item1][trace.pos.Item2] = '^';
                        break;
                }
            }
            grid[trace.pos.Item1][trace.pos.Item2] = 'S';

            for (int y = grid[0].Length - 1; y > -1; y--)
            {
                for (int x = 0; x < grid.Length; x++)
                {
                    render.Append(grid[x][y]);
                }
                render.Append("\r\n");
            }
            Console.WriteLine(render.ToString());
            return current.gCost;
        }



        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            (int, int) startPos = (0, 0);
            (int, int) endPos = (0, 0);
            (int, int) direction = (1, 0);

            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                    if (grid[x][_y] == 'S') { startPos = (x, _y); grid[x][_y] = '.'; }
                    else if (grid[x][_y] == 'E') { endPos = (x, _y); grid[x][_y] = '.'; }
                }
            }

            Node current = new Node(startPos, direction, 0, HCost(startPos, endPos), new HashSet<Node>());
            while (true)
            {
                closed.Add(current);
                if (current.pos == endPos) { break; }

                for (int i = 0; i < 4; i++)
                {
                    (int, int) nextPos = (current.pos.Item1 + direction.Item1, current.pos.Item2 + direction.Item2);

                    if (grid[nextPos.Item1][nextPos.Item2] != '#')
                    {
                        int cost = current.gCost + 1;
                        if ((direction.Item1, direction.Item2) == (-current.direction.Item1, -current.direction.Item2))
                        {
                            cost += 2000;
                        }
                        else if (direction == current.direction)
                        {
                            cost += 0;
                        }
                        else
                        {
                            cost += 1000;
                        }
                        Node neighbour = new Node(nextPos, direction, cost, HCost(nextPos, endPos), new HashSet<Node> { current });
                        if (closed.Contains(neighbour) == false)
                        {
                            open.Add(neighbour);
                        }
                    }
                    direction = (-direction.Item2, direction.Item1);
                }


                current = GetOpen(ref open);
                direction = current.direction;
            }


            HashSet<Node> path = new HashSet<Node>();
            long lowestPath = current.hCost + current.gCost;

            Node trace = current;
            while (true)
            {
                path.Add(trace);
                if (trace.parents.Count == 0) { break; }
                foreach (Node parent in trace.parents)
                {
                    trace = parent;
                }
            }

            current = new Node(startPos, (1, 0), 0, HCost(startPos, endPos), new HashSet<Node>());
            open.Clear();
            closed.Clear();
            while (true)
            {
                if (current.pos != endPos)
                {
                    closed.Add(current);
                }

                for (int i = 0; i < 4; i++)
                {
                    (int, int) nextPos = (current.pos.Item1 + direction.Item1, current.pos.Item2 + direction.Item2);

                    if (grid[nextPos.Item1][nextPos.Item2] != '#')
                    {
                        int cost = current.gCost + 1;
                        if ((direction.Item1, direction.Item2) == (-current.direction.Item1, -current.direction.Item2))
                        {
                            cost += 2000;
                        }
                        else if (direction == current.direction)
                        {
                            cost += 0;
                        }
                        else
                        {
                            cost += 1000;
                        }
                        Node neighbour = new Node(nextPos, direction, cost, HCost(nextPos, endPos), new HashSet<Node> { current });
                        if (closed.Contains(neighbour) == false)
                        {
                            open.Add(neighbour);
                        }
                        else
                        {
                            Node explored = closed[closed.IndexOf(neighbour)];
                            if (explored.gCost + explored.hCost == neighbour.gCost + neighbour.hCost)
                            {
                                explored.parents.Add(current);
                            }
                        }
                    }
                    direction = (-direction.Item2, direction.Item1);
                }

                if (current.pos == endPos)
                {
                    if (current.hCost + current.gCost <= lowestPath)
                    {
                        GetParents(current, ref path);
                    }
                }

                if (open.Count == 0) { break; }

                current = GetOpen(ref open);
                direction = current.direction;

                if (current.hCost + current.gCost > lowestPath) { break; }
            }

            // Rendering
            StringBuilder render = new StringBuilder();
            foreach (Node pos in path)
            {
                grid[pos.pos.Item1][pos.pos.Item2] = 'O';
            }

            for (int y = grid[0].Length - 1; y > -1; y--)
            {
                for (int x = 0; x < grid.Length; x++)
                {
                    render.Append(grid[x][y]);
                }
                render.Append("\r\n");
            }
            Console.WriteLine(render.ToString());
            HashSet<(int, int)> uniquePos = new HashSet<(int, int)>();
            foreach (Node pos in path)
            {
                uniquePos.Add(pos.pos);
            }
            return uniquePos.Count;
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
                if (open[i].gCost + open[i].hCost < current.gCost + current.hCost)
                {
                    current = open[i];
                    idx = i;
                }
            }
            open.RemoveAt(idx);
            return current;
        }

        private static void GetParents(Node node, ref HashSet<Node> paths)
        {
            paths.Add(node);
            if (node.parents.Count == 0) { return; }
            foreach (Node parent in node.parents) 
            {
                paths.Add(parent);
                GetParents(parent, ref paths);
            }
        }

    }
}
