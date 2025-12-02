using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2015
{
    public class Day05 : IDay
    {
      

        public static long Part_1(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long nice = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], @"[aeiou].*[aeiou].*[aeiou]") == false) { continue; }
                if (Regex.IsMatch(lines[i], @"([a-z])\1") == false) { continue; }
                if (Regex.IsMatch(lines[i], @"(ab|cd|pq|xy)") == true) { continue; }
                nice++;
            }
            return nice;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long nice = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (Regex.IsMatch(lines[i], @"(..).*\1") == false) { continue; }
                if (Regex.IsMatch(lines[i], @"(.).\1") == false) { continue; }
                nice++;
            }
            return nice;
        }
    }
}
