using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public class Day04 : IDay
    {
        private static long XmasPerX(int x, int y, string[] lines)
        {
            long sum = 0;
            (int, int)[] directions = [(1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1)];
            foreach ((int, int) direction in directions)
            {
                for (int i = 0; i < 4; i++)
                {
                    int _x = x + i * direction.Item1;
                    int _y = y + i * direction.Item2;
                    if (_x < 0 || _y < 0 || _x >= lines[0].Length || _y >= lines.Length) { break; }
                    switch(i)
                    {
                        case 0:
                            if (lines[_y][_x] != 'X') { i = 3; }
                            break;
                        case 1:
                            if (lines[_y][_x] != 'M') { i = 3; }
                            break;
                        case 2:
                            if (lines[_y][_x] != 'A') { i = 3; }
                            break;
                        case 3:
                            if (lines[_y][_x] == 'S') { sum++; }
                            break;
                    }
                }
            }
            return sum;
        }

        private static long X_MasPerA(int x, int y, string[] lines)
        {
            (int, int) UpR = (x + 1, y - 1);
            (int, int) DownR = (x + 1, y + 1);
            (int, int) DownL = (x - 1, y + 1);
            (int, int) UpL = (x - 1, y - 1);

            if (UpL.Item1 < 0 || UpL.Item2 < 0) { return 0; }
            if (DownR.Item1 >= lines[0].Length || DownR.Item2 >= lines.Length) { return 0; }

            long hash1 = MathUtils.PerfectlyHashThem((byte)lines[UpL.Item2][UpL.Item1], (byte)lines[DownR.Item2][DownR.Item1]);
            long hash2 = MathUtils.PerfectlyHashThem((byte)lines[UpR.Item2][UpR.Item1], (byte)lines[DownL.Item2][DownL.Item1]);
            long correctHash1 = MathUtils.PerfectlyHashThem((byte)'M', (byte)'S');
            long correctHash2 = MathUtils.PerfectlyHashThem((byte)'S', (byte)'M');

            if (hash1 != correctHash1 && hash1 != correctHash2) { return 0; }
            if (hash2 != correctHash1 && hash2 != correctHash2) { return 0; }


            return 1;
        }

        public static long Part_1(string input)
        {
            string[] linesStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long sum = 0;
            for (int x = 0; x < linesStr[0].Length; x++)
            {
                for (int y = 0; y < linesStr.Length; y++)
                {
                    if (linesStr[y][x] == 'X')
                    {
                        sum += XmasPerX(x, y, linesStr);
                    }
                }
            }

            return sum;
        }

        public static long Part_2(string input)
        {
            string[] linesStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long sum = 0;
            for (int x = 0; x < linesStr[0].Length; x++)
            {
                for (int y = 0; y < linesStr.Length; y++)
                {
                    if (linesStr[y][x] == 'A')
                    {
                        sum += X_MasPerA(x, y, linesStr);
                    }
                }
            }

            return sum;
        }
    }
}
