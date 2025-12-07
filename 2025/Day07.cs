using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2025
{
    public class Day07 : IDay
    {
        public static long Part_1(string input)
        {
            Dictionary<Vector2Int, char> grid = DataUtils.StringToGrid(input);
            long count = 0;
           

            Vector2Int startpos = Vector2Int.Zero;
            foreach (var kvp in grid)
            {
                if (kvp.Value == 'S')
                {
                    startpos = kvp.Key;
                    break;
                }
            }

            HashSet<Vector2Int> open = new HashSet<Vector2Int>();
            HashSet<Vector2Int> closed = new HashSet<Vector2Int>();
            open.Add(startpos + Vector2Int.Down);

            while (open.Count != 0)
            {
                Vector2Int current = Vector2Int.Zero;
                foreach (Vector2Int pos in open)
                {
                    current = pos;
                    break;
                }
                open.Remove(current);
                closed.Add(current);
                if (grid[current] == '^')
                {
                    Vector2Int left = current + Vector2Int.Left;
                    Vector2Int right = current + Vector2Int.Right;
                    if (grid.ContainsKey(left) && !open.Contains(left) && !closed.Contains(left))
                    {
                        open.Add(left);
                    }
                    if (grid.ContainsKey(right) && !open.Contains(right) && !closed.Contains(right))
                    {
                        open.Add(right);
                    }
                    count++;
                }
                else
                {
                    Vector2Int next = current + Vector2Int.Down;
                    if (grid.ContainsKey(next) && !open.Contains(next) && !closed.Contains(next))
                    {
                        open.Add(next);
                    }
                }
            }

            return count;
        }

        public static long Part_2(string input)
        {
            Dictionary<Vector2Int, char> grid = DataUtils.StringToGrid(input);
            Vector2Int startpos = Vector2Int.Zero;
            foreach (var kvp in grid)
            {
                if (kvp.Value == 'S')
                {
                    startpos = kvp.Key;
                    break;
                }
            }

            List<Vector2Int> splitters = new List<Vector2Int>();
            List<Vector2Int> open = new List<Vector2Int>();
            List<Vector2Int> nextOpen = new List<Vector2Int>();
            Dictionary<Vector2Int, long> weights = new Dictionary<Vector2Int, long>();
            long cloesedTimelines = 0;
            open.Add(startpos);
            weights.Add(startpos, 1);
            while (splitters.Count != 0 || open.Count != 0)
            {
                foreach (Vector2Int splitter in splitters)
                {
                    Vector2Int left = splitter + Vector2Int.Left;
                    Vector2Int right = splitter + Vector2Int.Right;
                    if (weights.ContainsKey(left))
                    {
                        weights[left] += weights[splitter];
                    }
                    else
                    {
                        open.Add(left);
                        weights.Add(left, weights[splitter]);
                    }
                    if (weights.ContainsKey(right))
                    {
                        weights[right] += weights[splitter];
                    }
                    else
                    {
                        open.Add(right);
                        weights.Add(right, weights[splitter]);
                    }
                }
                splitters.Clear();
                foreach (Vector2Int pos in open)
                {
                    Vector2Int next = pos + Vector2Int.Down;
                    if (grid.ContainsKey(next) == false)
                    {
                        cloesedTimelines += weights[pos];
                        continue;
                    }
                    weights.Add(next, weights[pos]);
                    if (grid[next] == '^')
                    {
                        splitters.Add(next);
                    }
                    else
                    {
                        nextOpen.Add(next);
                    }
                }
                open.Clear();
                open.AddRange(nextOpen);
                nextOpen.Clear();
            }

            return cloesedTimelines;
        }
    }
}
