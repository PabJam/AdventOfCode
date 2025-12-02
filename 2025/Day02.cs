using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2025
{
    public  class Day02 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(',' , StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long counter = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int splitIdx = lines[i].IndexOf('-');
                long lower = long.Parse(lines[i].Substring(0, splitIdx));
                long higher = long.Parse(lines[i].Substring(splitIdx + 1, lines[i].Length - splitIdx - 1));
                for (long id = lower; id <= higher; id++)
                {
                    string idStr = id.ToString();
                    if (idStr.Length % 2 != 0) { continue; }
                    int halfLength = idStr.Length / 2;
                    if (idStr.Substring(0, halfLength) == idStr.Substring(halfLength, halfLength))
                    {
                        counter += id;
                    }
                }
            }
            return counter;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long counter = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int splitIdx = lines[i].IndexOf('-');
                long lower = long.Parse(lines[i].Substring(0, splitIdx));
                long higher = long.Parse(lines[i].Substring(splitIdx + 1, lines[i].Length - splitIdx - 1));
                bool repeating = true;
                for (long id = lower; id <= higher; id++)
                {
                    string idStr = id.ToString();
                    for (int j = 1; j <= idStr.Length / 2; j++)
                    {
                        repeating = true;
                        if (idStr.Length % j != 0) { continue; }
                        List<string> parts = Split(idStr, j).ToList();
                        foreach (string part in parts)
                        {
                            if (part != parts[0]) { repeating = false; break; } 
                        }
                        if (repeating == true)
                        {
                            counter += id;
                            break;
                        }
                    }
                }
            }
            return counter;
        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}
