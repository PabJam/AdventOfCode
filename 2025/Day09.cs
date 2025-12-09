using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static System.Net.Mime.MediaTypeNames;

namespace _2025
{
    public class Day09 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Vector2Int[] positions = new Vector2Int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] coords = lines[i].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                positions[i] = new Vector2Int(long.Parse(coords[0]), long.Parse(coords[1]));
            }

            long maxArea = 0;
            Vector2Int maxPos1 = Vector2Int.Zero, maxPos2 = Vector2Int.Zero;
            for (int i = 0; i < positions.Length - 1; i++)
            {
                for (int j = i + 1; j < positions.Length; j++)
                {
                    long area = Math.Abs((positions[i].x - positions[j].x) + 1) * (Math.Abs(positions[i].y - positions[j].y) + 1);
                    if (area > maxArea)
                    {
                        maxArea = area;
                        maxPos1 = positions[i];
                        maxPos2 = positions[j];
                    }
                }
            }

            return maxArea;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Vector2Int[] positions = new Vector2Int[lines.Length];
            HashSet<Vector2Int> corners = new HashSet<Vector2Int>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] coords = lines[i].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                positions[i] = new Vector2Int(long.Parse(coords[0]), long.Parse(coords[1]));
                corners.Add(positions[i]);
            }

            Dictionary<Vector2Int, (Vector2Int, Vector2Int)> border = new Dictionary<Vector2Int, (Vector2Int, Vector2Int)>();
            for (int i = 0; i < positions.Length; i++)
            {
                Vector2Int previous;
                Vector2Int next;
                if (i == 0) { previous = positions[positions.Length - 1]; }
                else { previous = positions[i - 1]; }
                if (i == positions.Length - 1) { next = positions[0]; }
                else { next = positions[i + 1]; }

                Vector2Int directionPrev;
                Vector2Int directionNext;
                if (positions[i].x == previous.x)
                {
                    if (previous.y < positions[i].y) { directionPrev = Vector2Int.Up; }
                    else { directionPrev = Vector2Int.Down; }
                }
                else
                {
                    if (previous.x < positions[i].x) { directionPrev = Vector2Int.Right; }
                    else { directionPrev = Vector2Int.Left; }
                }
                if (positions[i].x == next.x)
                {
                    if (next.y < positions[i].y) { directionNext = Vector2Int.Down; }
                    else { directionNext = Vector2Int.Up; }
                }
                else
                {
                    if (next.x < positions[i].x) { directionNext = Vector2Int.Left; }
                    else { directionNext = Vector2Int.Right; }
                }

                border.Add(positions[i], (directionPrev, directionNext));
            }

            Vector2Int start = positions[0];
            Vector2Int current = start;
            Vector2Int dir = border[start].Item2;
            while(true)
            {
                Vector2Int next = current + dir;
                if (next == start) { break; }
                if (corners.Contains(next))
                {
                    dir = border[next].Item2;
                }
                else
                {
                    border.Add(next, (dir, dir));
                }
                current = next;
            }

            //Test which way is inside
            Vector2Int testpos = start + border[start].Item2;
            while (border[testpos].Item1 != border[testpos].Item2)
            {
                testpos = testpos + border[testpos].Item2;
            }
            Vector2Int testposCW = testpos + Rotate90CW(border[testpos].Item2);
            Vector2Int testposCCW = testpos + Rotate90CCW(border[testpos].Item2);
            Func<Vector2Int, Vector2Int> RotateOut = Rotate90CW;
            bool[] hitAllSidesCW = new bool[4] { false, false, false, false};
            bool[] hitAllSidesCCW = new bool[4] { false, false, false, false};
            long idxCW = 0, idxCCW = 0;
            while(true)
            {
                if (hitAllSidesCW[0] == false)
                {
                    if (border.ContainsKey(testposCW + idxCW * Vector2Int.Right))
                    {
                        idxCW = 0;
                        hitAllSidesCW[0] = true;
                    }
                }
                else if (hitAllSidesCW[1] == false)
                {
                    if (border.ContainsKey(testposCW + idxCW * Vector2Int.Down))
                    {
                        idxCW = 0;
                        hitAllSidesCW[1] = true;
                    }
                }
                else if (hitAllSidesCW[2] == false)
                {
                    if (border.ContainsKey(testposCW + idxCW * Vector2Int.Left))
                    {
                        idxCW = 0;
                        hitAllSidesCW[2] = true;
                    }
                }
                else
                {
                    if (border.ContainsKey(testposCW + idxCW * Vector2Int.Up))
                    {
                        RotateOut = Rotate90CCW;
                        break;
                    }
                   
                }

                if (hitAllSidesCCW[0] == false)
                {
                    if (border.ContainsKey(testposCCW + idxCCW * Vector2Int.Right))
                    {
                        idxCCW = 0;
                        hitAllSidesCCW[0] = true;
                    }
                }
                else if (hitAllSidesCCW[1] == false)
                {
                    if (border.ContainsKey(testposCCW + idxCCW * Vector2Int.Down))
                    {
                        idxCCW = 0;
                        hitAllSidesCCW[1] = true;
                    }
                }
                else if (hitAllSidesCCW[2] == false)
                {
                    if (border.ContainsKey(testposCCW + idxCCW * Vector2Int.Left))
                    {
                        idxCCW = 0;
                        hitAllSidesCCW[2] = true;
                    }
                }
                else
                {
                    if (border.ContainsKey(testposCCW + idxCCW * Vector2Int.Up))
                    {
                        RotateOut = Rotate90CW;
                        break;
                    }
                    
                }
                idxCCW++;
                idxCW++;
            }


            List<(long, Vector2Int, Vector2Int)> areas = new List<(long, Vector2Int, Vector2Int)>();

            Vector2Int maxPos1 = Vector2Int.Zero, maxPos2 = Vector2Int.Zero;
            for (int i = 0; i < positions.Length - 1; i++)
            {
                for (int j = i + 1; j < positions.Length; j++)
                {
                    long area = (Math.Abs(positions[i].x - positions[j].x) + 1) * (Math.Abs(positions[i].y - positions[j].y) + 1);
                    areas.Add((area, positions[i], positions[j]));
                }
            }
            areas.Sort((a, b) => a.Item1.CompareTo(b.Item1));
            for (int i = areas.Count - 1; i >= 0; i--) 
            {
                if (CheckBounds(areas[i].Item2, areas[i].Item3, border, RotateOut))
                {
                    return areas[i].Item1;
                }
            }

            return -1;
        }

        private static bool CheckBounds(Vector2Int c1, Vector2Int c2, Dictionary<Vector2Int, (Vector2Int, Vector2Int)> border, Func<Vector2Int, Vector2Int> RotateOut)
        {
            Vector2Int c3 = new Vector2Int(c1.x, c2.y);
            Vector2Int c4 = new Vector2Int(c2.x, c1.y);

            Vector2Int direction = c3.y < c1.y ? Vector2Int.Down : Vector2Int.Up;
            if (TraceLine(c1, c3, direction, border, RotateOut) == false) { return false; }
            direction = c2.x < c3.x ? Vector2Int.Left : Vector2Int.Right;
            if (TraceLine(c3, c2, direction, border, RotateOut) == false) { return false; }
            direction = c4.y < c2.y ? Vector2Int.Down : Vector2Int.Up;
            if (TraceLine(c2, c4, direction, border, RotateOut) == false) { return false; }
            direction = c1.x < c4.x ? Vector2Int.Left : Vector2Int.Right;
            if (TraceLine(c4, c1, direction, border, RotateOut) == false) { return false; }
            return true;
            
            
        }

        private static bool TraceLine(Vector2Int c1, Vector2Int c2, Vector2Int dir, Dictionary<Vector2Int, (Vector2Int, Vector2Int)> border, Func<Vector2Int, Vector2Int> RotateOut)
        {
            Vector2Int current = c1;
            
            while (true)
            {
                if (current == c2) { return true; }
                bool legal = false;

                if (border.ContainsKey(current) == false) { legal = true; } // not border
                else if (border[current].Item2 == dir) { legal = true; } // on border in direction
                else if (-1 * border[current].Item1 == dir) { legal = true; } // on border against direction
                else if (border[current].Item2 == RotateOut(border[current].Item1)) { legal = true; } // concave corner cant exit
                else if (border[current].Item1 == border[current].Item2 && dir != RotateOut(border[current].Item2)) { legal = true; } // straight line not in outside direction
                
                if (legal == false) { return false; }
                current = current + dir;
            }
        }

        private static Vector2Int Rotate90CW(Vector2Int vec)
        {
            return new Vector2Int(vec.y, vec.x * -1);
        }

        private static Vector2Int Rotate90CCW(Vector2Int vec)
        {
            return new Vector2Int(vec.y * -1, vec.x);
        }
    }
}
