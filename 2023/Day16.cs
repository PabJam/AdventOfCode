using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day16 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<Vector2Int> visited = new List<Vector2Int>();
            List<(Vector2Int, Vector2Int)> visitedPaths = new List<(Vector2Int, Vector2Int)>();
            Raycast(ref lines, Vector2Int.Zero, Vector2Int.Right, ref visited, ref visitedPaths);
            return visited.Count;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<Vector2Int> visited = new List<Vector2Int>();
            List<(Vector2Int, Vector2Int)> visitedPaths = new List<(Vector2Int, Vector2Int)>();
            long result = 0;
            int count = 0;
            int length = 2 * lines.Length + 2 * lines[0].Length;
            for (int y = 0; y < lines.Length; y++)
            {
                visited.Clear();
                visitedPaths.Clear();
                Raycast(ref lines, new Vector2Int(0, y), Vector2Int.Right, ref visited, ref visitedPaths);
                count++;
                Console.WriteLine("Completed: [" + count + "/" + length + "]");
                if (visited.Count > result) { result = visited.Count; }
            }
            for (int y = 0; y < lines.Length; y++)
            {
                visited.Clear();
                visitedPaths.Clear();
                Raycast(ref lines, new Vector2Int(lines[y].Length - 1, y), Vector2Int.Left, ref visited, ref visitedPaths);
                count++;
                Console.WriteLine("Completed: [" + count + "/" + length + "]");
                if (visited.Count > result) { result = visited.Count; }
            }
            for (int x = 0; x < lines[0].Length; x++)
            {
                visited.Clear();
                visitedPaths.Clear();
                Raycast(ref lines, new Vector2Int(x, 0), Vector2Int.Up, ref visited, ref visitedPaths);
                count++;
                Console.WriteLine("Completed: [" + count + "/" + length + "]");
                if (visited.Count > result) { result = visited.Count; }
            }
            for (int x = 0; x < lines.Length; x++)
            {
                visited.Clear();
                visitedPaths.Clear();
                Raycast(ref lines, new Vector2Int(x, lines.Length - 1), Vector2Int.Down, ref visited, ref visitedPaths);
                count++;
                Console.WriteLine("Completed: [" + count + "/" + length + "]");
                if (visited.Count > result) { result = visited.Count; }
            }
            return result;
        }

        private static void Raycast(ref string[] map, Vector2Int pos, Vector2Int direction, ref List<Vector2Int> visited, ref List<(Vector2Int, Vector2Int)> visitedPaths)
        {
            while (true)
            {
                if (Utils.CheckOOB(map, pos) == true) { return; }
                if (visited.Contains(pos) == false)
                {
                    visited.Add(pos);
                }
                if (visitedPaths.Contains((pos, direction)) == true) { return; }
                visitedPaths.Add((pos, direction));
                switch (map[pos.y][(int)pos.x])
                {
                    case '.':
                        break;
                    case '|':
                        if (direction == Vector2Int.Up || direction == Vector2Int.Down)
                        { break; }
                        Raycast(ref map, pos + Vector2Int.Up, Vector2Int.Up, ref visited, ref visitedPaths);
                        Raycast(ref map, pos + Vector2Int.Down, Vector2Int.Down, ref visited, ref visitedPaths);
                        return;
                    case '-':
                        if (direction == Vector2Int.Right || direction == Vector2Int.Left)
                        { break; }
                        Raycast(ref map, pos + Vector2Int.Right, Vector2Int.Right, ref visited, ref visitedPaths);
                        Raycast(ref map, pos + Vector2Int.Left, Vector2Int.Left, ref visited, ref visitedPaths);
                        return;
                    case '/':
                        direction = new Vector2Int(-direction.y, -direction.x);
                        break;
                    case '\\':
                        direction = new Vector2Int(direction.y, direction.x);
                        break;
                    default: throw new Exception("Should only contain above characters");
                }
                pos += direction;
            }

        }
    }
}
