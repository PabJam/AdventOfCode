using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2025
{
    public class Day04 : IDay
    {
        public static long Part_1(string input)
        {
            Dictionary<Vector2Int, char> grid = DataUtils.StringToGrid(input);
            long counter = 0;
            foreach (var kvp in grid)
            {
                if (kvp.Value != '@') { continue; }
                int paperNeighbours = 0;
                for (int i = 0; i < Vector2Int.Directions.Length; i++)
                {
                    Vector2Int pos = Vector2Int.Directions[i] + kvp.Key;
                    if (grid.ContainsKey(pos) == false) { continue; } // oob
                    if (grid[pos] == '@') { paperNeighbours++; }
                }
                if (paperNeighbours < 4) { counter++; }
            }
            return counter;
        }

        public static long Part_2(string input)
        {
            Dictionary<Vector2Int, char> grid = DataUtils.StringToGrid(input);
            List<Vector2Int> removed = new List<Vector2Int>();
            long counter = 0;
            long lastCount = counter;
            do    
            {
                lastCount = counter;
                foreach (Vector2Int pos in removed)
                {
                    grid[pos] = '.';
                }
                removed.Clear();
                foreach (var kvp in grid)
                {
                    if (kvp.Value != '@') { continue; }
                    int paperNeighbours = 0;
                    for (int i = 0; i < Vector2Int.Directions.Length; i++)
                    {
                        Vector2Int pos = Vector2Int.Directions[i] + kvp.Key;
                        if (grid.ContainsKey(pos) == false) { continue; } // oob
                        if (grid[pos] == '@') { paperNeighbours++; }
                    }
                    if (paperNeighbours < 4) { counter++; removed.Add(kvp.Key); }
                }
            }
            while (counter != lastCount);
            
            return counter;
        }
    }
}
