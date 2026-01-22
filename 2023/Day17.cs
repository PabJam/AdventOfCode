using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day17 : IDay
    {
        private delegate void PopNeighbours(int[][] map,
            ref Dictionary<(Vector2Int, Vector2Int, int), NodeDij> open,
            ref Dictionary<(Vector2Int, Vector2Int, int), NodeDij> closed,
            NodeDij current,
            Vector2Int pos,
            Vector2Int dir,
            int straight);

        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[][] map = new int[lines.Length][];
            List<int> ints = new List<int>();
            for (int y = 0; y < lines.Length; y++)
            {
                ints.Clear();
                for (int x = 0; x < lines[y].Length; x++)
                {
                    ints.Add(lines[y][x] - '0');
                }
                map[y] = ints.ToArray();
            }
            Vector2Int[] path = Dijkstra(map, Vector2Int.Zero, new Vector2Int(map[0].Length - 1, map.Length - 1), PopulateNeighbours);
            Utils.ReverseArray(ref path);
            long result = 0;
            for (int i = 1; i < path.Length; i++)
            {
                result += map[path[i].y][path[i].x];
                Vector2Int dir = path[i] - path[i - 1];
                StringBuilder sb = new StringBuilder(lines[path[i].y]);
                if (dir == Vector2Int.Right)
                {
                    sb[(int)path[i].x] = '>';
                }
                else if (dir == Vector2Int.Left)
                {
                    sb[(int)path[i].x] = '<';
                }
                else if (dir == Vector2Int.Down)
                {
                    sb[(int)path[i].x] = '^';
                }
                else if (dir == Vector2Int.Up)
                {
                    sb[(int)path[i].x] = 'v';
                }
                else
                {
                    throw new Exception("cant move diagonal");
                }
                lines[path[i].y] = sb.ToString();
            }
            return result;
        }

        public static long Part_2(string input)
        {
            // min 4 straight before turn / end
            // maximum 10 straight
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[][] map = new int[lines.Length][];
            List<int> ints = new List<int>();
            for (int y = 0; y < lines.Length; y++)
            {
                ints.Clear();
                for (int x = 0; x < lines[y].Length; x++)
                {
                    ints.Add(lines[y][x] - '0');
                }
                map[y] = ints.ToArray();
            }
            Vector2Int[] path = Dijkstra(map, Vector2Int.Zero, new Vector2Int(map[0].Length - 1, map.Length - 1), PopulateNeighboursUltra);
            Utils.ReverseArray(ref path);
            long result = 0;
            for (int i = 1; i < path.Length; i++)
            {
                result += map[path[i].y][path[i].x];
                Vector2Int dir = path[i] - path[i - 1];
                StringBuilder sb = new StringBuilder(lines[path[i].y]);
                if (dir == Vector2Int.Right)
                {
                    sb[(int)path[i].x] = '>';
                }
                else if (dir == Vector2Int.Left)
                {
                    sb[(int)path[i].x] = '<';
                }
                else if (dir == Vector2Int.Down)
                {
                    sb[(int)path[i].x] = '^';
                }
                else if (dir == Vector2Int.Up)
                {
                    sb[(int)path[i].x] = 'v';
                }
                else
                {
                    throw new Exception("cant move diagonal");
                }
                lines[path[i].y] = sb.ToString();
            }
            return result;
        }

        private static Vector2Int[] Dijkstra(int[][] map, Vector2Int startPos, Vector2Int endPos, PopNeighbours popNeighbours)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            Dictionary<(Vector2Int, Vector2Int, int), NodeDij> open = new Dictionary<(Vector2Int, Vector2Int, int), NodeDij>();
            Dictionary<(Vector2Int, Vector2Int, int), NodeDij> closed = new Dictionary<(Vector2Int, Vector2Int, int), NodeDij>();
            NodeDij current = new NodeDij(Vector2Int.Zero, Vector2Int.Zero, 0, 0);
            Vector2Int posStraight;
            Vector2Int posRight = startPos + Vector2Int.Right;
            Vector2Int posLeft = startPos + Vector2Int.Up;
            open.Add((posRight, Vector2Int.Right, 1), new NodeDij(posRight, Vector2Int.Right, 1, map[posRight.y][posRight.x], current));
            open.Add((posLeft, Vector2Int.Up, 1), new NodeDij(posLeft, Vector2Int.Up, 1, map[posLeft.y][posLeft.x], current));
            closed.Add((current.pos, current.dir, current.straightCount), current);
            while (true)
            {
                current = new NodeDij(Vector2Int.Zero, Vector2Int.Zero, 0, int.MaxValue);
                Vector2Int dir = Vector2Int.Zero;
                foreach (KeyValuePair<(Vector2Int, Vector2Int, int), NodeDij> node in open)
                {
                    if (node.Value.cost < current.cost)
                    {
                        current = node.Value;
                        dir = node.Key.Item2;
                    }
                }
                if (current.pos == endPos) { break; }
                if (dir == Vector2Int.Zero) { throw new Exception("should have found new node"); }
                posStraight = current.pos + dir;
                posRight = current.pos + new Vector2Int(dir.y, dir.x);
                posLeft = current.pos + new Vector2Int(-dir.y, -dir.x);
                popNeighbours.Invoke(map, ref open, ref closed, current, posStraight, dir, current.straightCount + 1);
                popNeighbours.Invoke(map, ref open, ref closed, current, posRight, new Vector2Int(dir.y, dir.x), 1);
                popNeighbours.Invoke(map, ref open, ref closed, current, posLeft, new Vector2Int(-dir.y, -dir.x), 1);
                open.Remove((current.pos, current.dir, current.straightCount));
                closed.Add((current.pos, current.dir, current.straightCount), current);
            }
            while (true)
            {
                path.Add(current.pos);
                if (current.pos == startPos) { break; }
                current = current.parent;
            }
            return path.ToArray();
        }

        private static void PopulateNeighbours(int[][] map,
            ref Dictionary<(Vector2Int, Vector2Int, int), NodeDij> open,
            ref Dictionary<(Vector2Int, Vector2Int, int), NodeDij> closed,
            NodeDij current,
            Vector2Int pos,
            Vector2Int dir,
            int straight)
        {
            if (straight > 3) { return; }
            if (Utils.CheckOOB(map, pos) == true) { return; }
            NodeDij? updateNode;
            if (open.TryGetValue((pos, dir, straight), out updateNode))
            {
                if (updateNode.cost > current.cost + map[pos.y][pos.x])
                {
                    updateNode.cost = current.cost + map[pos.y][pos.x];
                    updateNode.parent = current;
                }
            }
            else if (closed.ContainsKey((pos, dir, straight)) == false)
            {
                open.Add((pos, dir, straight), new NodeDij(pos, dir, straight, current.cost + map[pos.y][pos.x], current));
            }
        }

        private static void PopulateNeighboursUltra(int[][] map,
            ref Dictionary<(Vector2Int, Vector2Int, int), NodeDij> open,
            ref Dictionary<(Vector2Int, Vector2Int, int), NodeDij> closed,
            NodeDij current,
            Vector2Int pos,
            Vector2Int dir,
            int straight)
        {
            if (straight > 10) { return; }
            if (current.straightCount < 4 && dir != current.dir) { return; }
            if (Utils.CheckOOB(map, pos) == true) { return; }
            NodeDij? updateNode;
            if (open.TryGetValue((pos, dir, straight), out updateNode))
            {
                if (updateNode.cost > current.cost + map[pos.y][pos.x])
                {
                    updateNode.cost = current.cost + map[pos.y][pos.x];
                    updateNode.parent = current;
                }
            }
            else if (closed.ContainsKey((pos, dir, straight)) == false)
            {
                open.Add((pos, dir, straight), new NodeDij(pos, dir, straight, current.cost + map[pos.y][pos.x], current));
            }
        }
    }
}
