using _2025;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public class Day07 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long[] results = new long[lineStr.Length];
            List<long>[] args = new List<long>[lineStr.Length];

            for (int i = 0; i < lineStr.Length; i++)
            {
                string[] line = lineStr[i].Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                results[i] = long.Parse(line[0]);
                string[] argStr = line[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                args[i] = new List<long>();
                for (int j = 0; j < argStr.Length; j++)
                {
                    args[i].Add(long.Parse(argStr[j]));
                }
            }

            long sum = 0;
            uint combinations = 0;
            for (int i = 0; i < results.Length; i++)
            {
                combinations = (uint)(1 << (args[i].Count - 1));
                for (int j = 0; j < combinations; j++)
                {
                    long result = args[i][0];
                    for (int k = 1; k < args[i].Count; k++)
                    {
                        if ( (j & (1 << (k - 1))) >> (k-1) == 1)
                        {
                            result += args[i][k];
                        }
                        else
                        {
                            result *= args[i][k];
                        }
                    }
                    if (result == results[i]) { sum += results[i]; break; }
                }
            }
            return sum;
        }

        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long[] results = new long[lineStr.Length];
            List<long>[] args = new List<long>[lineStr.Length];

            for (int i = 0; i < lineStr.Length; i++)
            {
                string[] line = lineStr[i].Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                results[i] = long.Parse(line[0]);
                string[] argStr = line[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                args[i] = new List<long>();
                for (int j = 0; j < argStr.Length; j++)
                {
                    args[i].Add(long.Parse(argStr[j]));
                }
            }

            BigInteger sum = 0;
            int combinations = 0;
            char[] base3 = new char[] { '0', '1', '2'};
            for (int i = 0; i < results.Length; i++)
            {
                combinations = (int)Math.Pow(3, args[i].Count - 1);
                for (int j = 0; j < combinations; j++)
                {
                    BigInteger result = args[i][0];
                    string combination = "0000000000000000" + DataUtils.IntToString(j, base3);
                    for (int k = 1; k < args[i].Count; k++)
                    {
                        char module = combination[combination.Length - k];
                        if (module == '0')
                        {
                            result += args[i][k];
                        }
                        else if (module == '1')
                        {
                            result *= args[i][k];
                        }
                        else
                        {
                            result = BigInteger.Parse(result.ToString() + args[i][k].ToString());
                        }
                    }
                    if (result == results[i])
                    { sum += result; break; }
                }
            }
            return (long)sum;
        }

    }
}
