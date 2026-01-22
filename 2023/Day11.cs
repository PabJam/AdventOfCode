using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day11 : IDay
    {
        public static long Part_1(string input)
        {
            Dictionary<Vector2Int, char> smallMap = Utils.StringToCharMapDic(input);
            Vector2Int line = new Vector2Int(0, 0);
            string[] lines = input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            bool hitGalaxyY;
            List<List<char>> Universe = new List<List<char>>();
            List<Vector2Int> galaxyPos = new List<Vector2Int>();
            for (int i = 0; i < lines.Length; i++)
            {
                Universe.Add(new List<char>());
            }

            while (line.x < lines[0].Length)
            {
                hitGalaxyY = false;
                line.y = 0;
                while (line.y < lines.Length)
                {
                    Universe[(int)line.y].Add(smallMap[line]);
                    if (smallMap[line] == '#')
                    {
                        hitGalaxyY = true;
                    }
                    line.y++;
                }
                if (hitGalaxyY == false)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        Universe[i].Add('.');
                    }
                }
                line.x++;
            }
            for (int i = 0; i < Universe.Count; i++)
            {
                if (Universe[i].Contains('#') == true) { continue; }
                Universe.Insert(i, new List<char>(Universe[i].ToArray()));
                i++;
            }
            for (int y = 0; y < Universe.Count; y++)
            {
                for (int x = 0; x < Universe[y].Count; x++)
                {
                    if (Universe[y][x] == '#')
                    {
                        galaxyPos.Add(new Vector2Int(x, y));
                    }
                }
            }
            long result = 0;
            for (int i = 0; i < galaxyPos.Count; i++)
            {
                for (int j = i + 1; j < galaxyPos.Count; j++)
                {
                    result += Math.Abs(galaxyPos[i].x - galaxyPos[j].x) + Math.Abs(galaxyPos[i].y - galaxyPos[j].y);
                }
            }
            return result;
        }

        public static long Part_2(string input)
        {
            Dictionary<Vector2Int, char> smallMap = Utils.StringToCharMapDic(input);
            Vector2Int line = new Vector2Int(0, 0);
            string[] lines = input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            bool hitGalaxyY;
            List<List<char>> Universe = new List<List<char>>();
            List<Vector2Int> galaxyPos = new List<Vector2Int>();
            List<Vector2Int> galaxyPosExpand = new List<Vector2Int>();
            List<Vector2Int> galaxyPosShift = new List<Vector2Int>();
            for (int i = 0; i < lines.Length; i++)
            {
                Universe.Add(new List<char>());
            }

            while (line.x < lines[0].Length)
            {
                hitGalaxyY = false;
                line.y = 0;
                while (line.y < lines.Length)
                {
                    Universe[(int)line.y].Add(smallMap[line]);
                    if (smallMap[line] == '#')
                    {
                        hitGalaxyY = true;
                    }
                    line.y++;
                }
                if (hitGalaxyY == false)
                {
                    for (int i = 0; i < Universe.Count; i++)
                    {
                        Universe[i].Add('.');
                    }
                }
                line.x++;
            }
            for (int i = 0; i < Universe.Count; i++)
            {
                if (Universe[i].Contains('#') == true) { continue; }
                Universe.Insert(i, new List<char>(Universe[i].ToArray()));
                i++;
            }

            for (int y = lines.Length - 1; y > -1; y--)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        galaxyPos.Add(new Vector2Int(x, lines.Length - 1 - y));
                    }
                }
            }
            for (int y = 0; y < Universe.Count; y++)
            {
                for (int x = 0; x < Universe[y].Count; x++)
                {
                    if (Universe[y][x] == '#')
                    {
                        galaxyPosExpand.Add(new Vector2Int(x, y));
                    }
                }
            }


            for (int i = 0; i < galaxyPos.Count; i++)
            {
                galaxyPosShift.Add(galaxyPosExpand[i] - galaxyPos[i]);
            }

            int factor = 999999;
            for (int i = 0; i < galaxyPos.Count; i++)
            {
                galaxyPosExpand[i] = galaxyPos[i] + (factor * galaxyPosShift[i]);
            }
            ulong result = 0;
            for (int i = 0; i < galaxyPosExpand.Count; i++)
            {
                for (int j = i + 1; j < galaxyPosExpand.Count; j++)
                {
                    result += (ulong)(Math.Abs(galaxyPosExpand[i].x - galaxyPosExpand[j].x) + Math.Abs(galaxyPosExpand[i].y - galaxyPosExpand[j].y));
                }
            }
            return (long)result;
        }
    }
}
