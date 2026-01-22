using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day13 : IDay
    {
        public static long Part_1(string input)
        {
            string[] blocks = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[][] lines = new string[blocks.Length][];
            int result = 0;
            int tempRes = -1;
            for (int i = 0; i < blocks.Length; i++)
            {
                lines[i] = blocks[i].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            }
            for (int i = 0; i < lines.Length; i++)
            {
                tempRes = -1;
                for (int j = 0; j < lines[i].Length - 1; j++)
                {
                    if (CheckHorizontal(lines[i], j) == true)
                    {
                        tempRes = j + 1;
                        result += tempRes * 100;
                        break;
                    }
                }
                if (tempRes != -1) { continue; }
                for (int j = 0; j < lines[i][0].Length - 1; j++)
                {
                    if (CheckVertical(lines[i], j) == true)
                    {
                        tempRes = j + 1;
                        result += tempRes;
                        break;
                    }
                }
                if (tempRes == -1) { throw new Exception("No Mirror Found"); }
            }
            return result;
        }

        public static long Part_2(string input)
        {
            string[] blocks = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[][] lines = new string[blocks.Length][];
            int result = 0;
            for (int i = 0; i < blocks.Length; i++)
            {
                lines[i] = blocks[i].Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            }
            for (int i = 0; i < lines.Length; i++)
            {
                result += ReplaceSmudge(ref lines[i]);
            }
            return result;
        }

        private static bool CheckHorizontal(string[] lines, int idx)
        {
            int y1 = idx;
            int y2 = idx + 1;
            while (true)
            {
                if (y1 < 0) { break; }
                if (y2 >= lines.Length) { break; }
                if (lines[y1] != lines[y2]) { return false; }
                y1--;
                y2++;
            }
            return true;
        }

        private static bool CheckVertical(string[] lines, int idx)
        {
            int x1 = idx;
            int x2 = idx + 1;
            while (true)
            {
                if (x1 < 0) { break; }
                if (x2 >= lines[0].Length) { break; }
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i][x1] != lines[i][x2])
                    { return false; }
                }
                x1--;
                x2++;
            }
            return true;
        }

        private static int ReplaceSmudge(ref string[] lines)
        {
            Vector2Int smudgePos = new Vector2Int(-1, -1);
            int smudgeCount = 0;
            int x1, x2, y1, y2;
            for (int i = 0; i < lines.Length - 1; i++)
            {
                y1 = i;
                y2 = i + 1;
                smudgeCount = 0;
                while (true)
                {
                    for (int j = 0; j < lines[0].Length; j++)
                    {
                        if (lines[y1][j] != lines[y2][j])
                        {
                            smudgeCount++;
                            smudgePos.x = j;
                            smudgePos.y = y1;
                        }
                        if (smudgeCount > 1) { goto break2; }
                    }
                    y1--;
                    y2++;
                    if (y1 < 0 || y2 >= lines.Length) { break; }
                }
            break2:
                if (smudgeCount == 1)
                {
                    StringBuilder sb = new StringBuilder(lines[smudgePos.y]);
                    sb[(int)smudgePos.x] = lines[smudgePos.y][(int)smudgePos.x] == '#' ? '.' : '#';
                    lines[smudgePos.y] = sb.ToString();
                    return (i + 1) * 100;
                }
            }

            for (int i = 0; i < lines[0].Length - 1; i++)
            {
                x1 = i;
                x2 = i + 1;
                smudgeCount = 0;
                while (true)
                {
                    for (int j = 0; j < lines.Length; j++)
                    {
                        if (lines[j][x1] != lines[j][x2])
                        {
                            smudgeCount++;
                            smudgePos.x = x1;
                            smudgePos.y = j;
                        }
                        if (smudgeCount > 1) { goto break3; }
                    }
                    x1--;
                    x2++;
                    if (x1 < 0 || x2 >= lines[0].Length) { break; }
                }
                break3:
                if (smudgeCount == 1)
                {
                    StringBuilder sb = new StringBuilder(lines[smudgePos.y]);
                    sb[(int)smudgePos.x] = lines[smudgePos.y][(int)smudgePos.x] == '#' ? '.' : '#';
                    lines[smudgePos.y] = sb.ToString();
                    return i + 1;
                }
            }

            throw new Exception("Did not find any smudge");
        }
    }
}
