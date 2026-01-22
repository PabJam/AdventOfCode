using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day18 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[][] commands = new string[lines.Length][];
            List<Vector2Int> directions = new List<Vector2Int>();
            List<Vector2Int> edgePos = new List<Vector2Int>();
            for (int i = 0; i < lines.Length; i++)
            {
                commands[i] = lines[i].Split(' ');
                int repeat = Int32.Parse(commands[i][1]);
                for (int j = 0; j < repeat; j++)
                {
                    switch (commands[i][0][0])
                    {
                        case 'R':
                            directions.Add(Vector2Int.Right);
                            break;
                        case 'L':
                            directions.Add(Vector2Int.Left);
                            break;
                        case 'D':
                            directions.Add(Vector2Int.Up);
                            break;
                        case 'U':
                            directions.Add(Vector2Int.Down);
                            break;
                    }
                }
            }
            edgePos.Add(Vector2Int.Zero);
            Vector2Int lastPos = Vector2Int.Zero;
            for (int i = 0; i < directions.Count; i++)
            {
                lastPos = lastPos + directions[i];
                edgePos.Add(lastPos);
            }
            int maxX = 0, maxY = 0, minX = 0, minY = 0;
            for (int i = 0; i < edgePos.Count; i++)
            {
                minX = (int)(edgePos[i].x < minX ? edgePos[i].x : minX);
                minY = (int)(edgePos[i].y < minY ? edgePos[i].y : minY);
                maxX = (int)(edgePos[i].x > maxX ? edgePos[i].x : maxX);
                maxY = (int)(edgePos[i].y > maxY ? edgePos[i].y : maxY);
            }
            for (int i = 0; i < edgePos.Count; i++)
            {
                Vector2Int pos = edgePos[i];
                pos.x -= minX;
                pos.y -= minY;
                edgePos[i] = pos;
            }
            maxX = maxX - minX + 1;
            maxY = maxY - minY + 1;
            int[][] map = new int[maxY][];
            for (int y = 0; y < map.Length; y++)
            {
                map[y] = new int[maxX];
                for (int x = 0; x < map[y].Length; x++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    if (edgePos.Contains(pos)) { map[y][x] = 3; }
                    else { map[y][x] = 0; }
                }
            }
            long result = 0;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 0) { FloodFill(ref map, new Vector2Int(x, y)); }
                    if (map[y][x] != 1) { result++; }
                }
            }
            return result;
        }

        public static long Part_2(string input)
        {
            // https://www.quora.com/How-do-you-calculate-the-area-of-an-irregular-shape-and-how-do-you-divide-it-into-equal-parts
            // should also be solveable by compressing the space where there are no corners : 
            // https://www.geeksforgeeks.org/dsa/coordinate-compression/
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[][] commands = new string[lines.Length][];
            List<Vector2Int> corners = new List<Vector2Int>();
            Vector2Int pos = Vector2Int.Zero;
            corners.Add(pos);
            for (int i = 0; i < lines.Length; i++)
            {
                commands[i] = lines[i].Split(' ');
                int repeat = Int32.Parse(commands[i][1]) + 1;
                switch (commands[i][0][0])
                {
                    case 'R':
                        pos += Vector2Int.Right * repeat;
                        break;
                    case 'L':
                        pos += Vector2Int.Left * repeat;
                        break;
                    case 'D':
                        pos += Vector2Int.Down * repeat;
                        break;
                    case 'U':
                        pos += Vector2Int.Up * repeat;
                        break;
                }
                corners.Add(pos);
            }
            corners.RemoveAt(corners.Count - 1);
            long result = 0;
            for (int i = 0; i < corners.Count; i++)
            {
                int iplus = i < corners.Count - 1 ? i + 1 : 0;
                result += (corners[iplus].y + corners[i].y) * (corners[iplus].x - corners[i].x);
            }
            result /= 2;
            return result;
        }

        private static void FloodFill(ref int[][] map, Vector2Int startPos)
        {
            List<Vector2Int> open = new List<Vector2Int>();
            List<Vector2Int> closed = new List<Vector2Int>();
            bool inside = true;
            open.Add(startPos);
            while (open.Count > 0)
            {
                Vector2Int rootpos = open[0];
                for (int y = -1; y < 2; y++)
                {
                    for (int x = -1; x < 2; x++)
                    {
                        if (Math.Abs(x) + Math.Abs(y) != 1) { continue; }
                        Vector2Int pos = new Vector2Int(rootpos.x + x, rootpos.y + y);
                        if (Utils.CheckOOB(map, pos) == true) { inside = false; continue; }
                        if (map[pos.y][pos.x] == 3) { continue; }
                        if (open.Contains(pos)) { continue; }
                        if (closed.Contains(pos)) { continue; }
                        open.Add(pos);
                    }
                }
                closed.Add(rootpos);
                open.RemoveAt(0);
            }
            for (int i = 0; i < closed.Count; i++)
            {
                if (inside == true)
                {
                    map[closed[i].y][closed[i].x] = 2;
                }
                else
                {
                    map[closed[i].y][closed[i].x] = 1;
                }
            }
        }
    }
}
