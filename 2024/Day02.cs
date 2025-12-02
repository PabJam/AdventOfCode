using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day02 : IDay
    {
        private static bool CheckSafeLine(List<long> line, bool complete)
        {
            bool increasing = (line[1] > line[0] && line[2] > line[1]) ||
                              (line[1] > line[0] && line[3] > line[2]) ||
                              (line[2] > line[1] && line[3] > line[2]);
            bool safe = true;
            (int, int) indexTuple = (0, 0);
            for (int i = 1; i < line.Count; i++) 
            {
                long diff = Math.Abs(line[i] - line[i - 1]);
                if (diff < 1 || diff > 3) { safe = false; indexTuple = (i - 1, i); break; }
                if (line[i] > line[i - 1] && increasing == false) { safe = false; indexTuple = (i - 1, i); break; }
                if (line[i] < line[i - 1] && increasing == true) { safe = false; indexTuple = (i - 1, i); break; }
            }
            if (safe == false && complete == true)
            {
                List<long> errLine1 = new List<long>(line);
                List<long> errLine2 = new List<long>(line);
                errLine1.RemoveAt(indexTuple.Item1);
                errLine2.RemoveAt(indexTuple.Item2);
                safe = CheckSafeLine(errLine1, false) || CheckSafeLine(errLine2, false);
            }

            return safe;
        }

        public static long Part_1(string input)
        {
            string[] linesStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<long>[] lines = new List<long>[linesStr.Length];
            for (int i = 0; i < linesStr.Length; i++)
            {
                lines[i] = new List<long>();
                string[] numsStr = linesStr[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < numsStr.Length; j++)
                {
                    lines[i].Add(long.Parse(numsStr[j]));
                }
            }

            long notSafe = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                bool increasing = lines[i][1] > lines[i][0];
                for (int j = 1; j < lines[i].Count; j++)
                {
                    long diff = Math.Abs(lines[i][j] - lines[i][j - 1]);
                    if (diff < 1 || diff > 3) { notSafe++; break; }
                    if (lines[i][j] > lines[i][j - 1] && increasing == false) { notSafe++; break; }
                    if (lines[i][j] < lines[i][j - 1] && increasing == true) { notSafe++; break; }
                }
            }
            return lines.Length - notSafe;
        }

        public static long Part_2(string input)
        {
            string[] linesStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<long>[] lines = new List<long>[linesStr.Length];
            for (int i = 0; i < linesStr.Length; i++)
            {
                lines[i] = new List<long>();
                string[] numsStr = linesStr[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < numsStr.Length; j++)
                {
                    lines[i].Add(long.Parse(numsStr[j]));
                }
            }

            long safe = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (CheckSafeLine(lines[i], true)) { safe++; }
            }
            return safe;
        }
    }
}
