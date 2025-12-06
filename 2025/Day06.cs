using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2025
{
    public class Day06 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            List<long>[] numbers = new List<long>[lines.Length - 1];
            long count = 0;
            for (int i = 0; i < lines.Length - 1; i++)
            {
                string[] numStr = lines[i].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                numbers[i] = new List<long>();
                for (int j = 0; j < numStr.Length; j++)
                {
                    numbers[i].Add(long.Parse(numStr[j]));
                }
            }
            string[] symbols = lines[lines.Length - 1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            for (int x = 0; x < numbers[0].Count; x++)
            {
                long result = numbers[0][x];
                Func<long, long, long> operation = symbols[x] == "+" ? add : mul;
                for (int y = 1; y < numbers.Length; y++)
                {
                    result = operation(result, numbers[y][x]);
                }
                count += result;
            }
            return count;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            List<char> symbols = new List<char>();
            List<List<long>> numbers = new List<List<long>>();
            long count = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < lines[lines.Length - 1].Length; i++)
            {
                sb.Clear();
                for (int j = 0; j < lines.Length - 1; j++)
                {
                    if (lines[j][i] != ' ')
                    {
                        sb.Append(lines[j][i]);
                    }
                }
                if (lines[lines.Length - 1][i] != ' ')
                {
                    symbols.Add(lines[lines.Length - 1][i]);
                    numbers.Add(new List<long>());
                }
                string num = sb.ToString();
                if (num == "") { continue; }
                numbers[numbers.Count - 1].Add(long.Parse(num));
            }

            for (int x = 0; x < numbers.Count; x++)
            {
                long result = numbers[x][0];
                Func<long, long, long> operation = symbols[x] == '+' ? add : mul;
                for (int y = 1; y < numbers[x].Count; y++)
                {
                    result = operation(result, numbers[x][y]);
                }
                count += result;
            }

            return count;
        }

        private static long mul(long a, long b)
        {
            return a * b;
        }

        private static long add(long a, long b)
        {
            return a + b;
        }
    }
}
