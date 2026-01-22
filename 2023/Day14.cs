using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day14 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long result = 0;
            Vector2Int[] stonePos = Tilt(ref lines, new Vector2Int(0, -1));
            for (int i = 0; i < stonePos.Length; i++)
            {
                result += lines.Length - stonePos[i].y;
            }
            return result;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long result = 0;
            Vector2Int[] endPos = Array.Empty<Vector2Int>();
            List<Vector2Int[]> recentPos = new List<Vector2Int[]>();
            bool repeat = false;
            ulong repeatCount = 0;
            ulong firstIdx = 0;
            for (ulong i = 0; i < 1000000000; i++)
            {
                _ = Tilt(ref lines, Vector2Int.Down);
                _ = Tilt(ref lines, Vector2Int.Left);
                _ = Tilt(ref lines, Vector2Int.Up);
                endPos = Tilt(ref lines, Vector2Int.Right);
                repeat = recentPos.Any(p => p.SequenceEqual(endPos));
                if (repeat == true)
                {
                    firstIdx = 1 + (ulong)recentPos.FindIndex(p => p.SequenceEqual(endPos));
                    repeatCount = i + 1;
                    break;
                }
                recentPos.Add(endPos);
            }
            (_, ulong rem) = Math.DivRem(1000000000ul - firstIdx, repeatCount - firstIdx);
            for (ulong i = 0; i < rem; i++)
            {
                _ = Tilt(ref lines, Vector2Int.Down);
                _ = Tilt(ref lines, Vector2Int.Left);
                _ = Tilt(ref lines, Vector2Int.Up);
                endPos = Tilt(ref lines, Vector2Int.Right);
            }
            for (int i = 0; i < endPos.Length; i++)
            {
                result += lines.Length - endPos[i].y;
            }
            return result;
        }

        private static Vector2Int[] Tilt(ref string[] map, Vector2Int direction)
        {
            List<Vector2Int> RollingstonePos = new List<Vector2Int>();
            List<Vector2Int> StopedstonePos = new List<Vector2Int>();
            Vector2Int stonePos;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] != 'O')
                    {
                        continue;
                    }
                    RollingstonePos.Add(new Vector2Int(x, y));
                }
            }

            while (RollingstonePos.Count > 0)
            {
                if (CheckRolling(ref map, 0, RollingstonePos[0] + direction, StopedstonePos, ref RollingstonePos) == true)
                {
                    continue;
                }
                StopedstonePos.Add(RollingstonePos[0]);
                RollingstonePos.RemoveAt(0);
            }
            return StopedstonePos.ToArray();
        }

        private static bool CheckRolling(ref string[] map, int idx, Vector2Int checkPos, List<Vector2Int> stopedStones, ref List<Vector2Int> rollingStones)
        {
            if (checkPos.x < 0 || checkPos.y < 0 || checkPos.x >= map[0].Length || checkPos.y >= map.Length)
            {
                return false;
            }
            if (map[checkPos.y][(int)checkPos.x] == '#')
            {
                return false;
            }
            if (map[checkPos.y][(int)checkPos.x] == '.')
            {
                Swap(ref map, idx, checkPos, ref rollingStones);
                return true;
            }
            if (stopedStones.Contains(checkPos))
            {
                return false;
            }
            Vector2Int next = checkPos + (checkPos - rollingStones[idx]);
            return CheckRolling(ref map, rollingStones.IndexOf(checkPos), next, stopedStones, ref rollingStones);
        }

        private static void Swap(ref string[] map, int idx, Vector2Int swapPos, ref List<Vector2Int> rollingStones)
        {
            char temp = map[swapPos.y][(int)swapPos.x];
            StringBuilder sb = new StringBuilder(map[swapPos.y]);
            sb[(int)swapPos.x] = map[rollingStones[idx].y][(int)rollingStones[idx].x];
            map[swapPos.y] = sb.ToString();
            sb = new StringBuilder(map[rollingStones[idx].y]);
            sb[(int)rollingStones[idx].x] = temp;
            map[rollingStones[idx].y] = sb.ToString();
            rollingStones[idx] = swapPos;
        }
    }
}
