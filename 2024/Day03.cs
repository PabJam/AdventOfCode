using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2024
{
    public class Day03 : IDay
    {
        public static long Part_1(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"mul\(\d+,\d+\)");
            long sum = 0;
            for (int i = 0; i < matches.Count; i++)
            {
                long a = long.Parse(Regex.Match(matches[i].Value, @"mul\((\d+)").Groups[1].Value);
                long b = long.Parse(Regex.Match(matches[i].Value, @"mul\(\d+,(\d+)").Groups[1].Value);
                sum += a * b;
            }
            return sum;
        }

        public static long Part_2(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"(mul\(\d+,\d+\)|do\(\)|don't\(\))");
            long sum = 0;
            bool enabled = true;
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].Value == "do()") { enabled = true; continue; }
                if (matches[i].Value == "don't()") { enabled = false; continue; }
                if (enabled == false) { continue; }
                long a = long.Parse(Regex.Match(matches[i].Value, @"mul\((\d+)").Groups[1].Value);
                long b = long.Parse(Regex.Match(matches[i].Value, @"mul\(\d+,(\d+)").Groups[1].Value);
                sum += a * b;
            }
            return sum;
        }
    }
}
