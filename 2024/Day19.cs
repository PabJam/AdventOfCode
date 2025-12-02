using _2025;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utils;

namespace _2024
{
    public class Day19 : IDay
    {
       
        public static long Part_1(string input)
        {
            string[] linesStr = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] available = linesStr[0].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] desired = linesStr[1].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> impossible = new HashSet<string>();
            long sum = 0;
            for (int i = 0; i < desired.Length; i++)
            {
                if (Possible(desired[i], available, ref impossible)) { sum++; }
            }

            return sum;
        }

        public static long Part_2(string input)
        {
            string[] linesStr = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] available = linesStr[0].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] desired = linesStr[1].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, long> combinations = new Dictionary<string, long>();
            long sum = 0;
            for (int i = 0; i < desired.Length; i++)
            {
                sum += NumDesgins(desired[i], available, ref combinations);
            }
            return sum;
        }

        private static bool Possible(string desired, string[] available, ref HashSet<string> impossible) 
        {
            if (desired.Length == 0) { return true; }
            if (impossible.Contains(desired)) { return false; }
            for (int i = 0; i < available.Length; i++)
            {
                Match match = Regex.Match(desired, @"^(" + available[i] + @")(.*)");
                if (match.Success)
                {
                    if (Possible(match.Groups[2].Value, available, ref impossible)) { return true; }
                }
            }

            impossible.Add(desired);
            return false;
        }

        private static long NumDesgins(string desired, string[] available, ref Dictionary<string, long> knownCombinations)
        {
            long combinations = 0;
            if (desired.Length == 0) { return 1; }
            if (knownCombinations.TryGetValue(desired, out combinations)) { return combinations; }
            for (int i = 0; i < available.Length; i++)
            {
                Match match = Regex.Match(desired, @"^(" + available[i] + @")(.*)");
                if (match.Success)
                {
                    combinations += NumDesgins(match.Groups[2].Value, available, ref knownCombinations);
                }
            }

            knownCombinations.Add(desired, combinations);
            return combinations;
        }

    }
}
