using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2025
{
    internal class Day03 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long sum = 0;

            foreach (string line in lines)
            {
                //int largestIdx = 0;
                //int largestInt = int.Parse(line[largestIdx] + "");
                //for (int i = 1; i < line.Length - 1; i++)
                //{
                //    int current = int.Parse(line[i] + "");
                //    if (current > largestInt)
                //    {
                //        largestIdx = i;
                //        largestInt = current;
                //    }
                //}
                //
                //int secondLargestIdx = largestIdx + 1;
                //int secondLargestInt = int.Parse(line[largestIdx + 1] + "");
                //for (int i = largestIdx + 1; i < line.Length; i++)
                //{
                //    int current = int.Parse(line[i] + "");
                //    if (current > secondLargestInt)
                //    {
                //        secondLargestIdx = i;
                //        secondLargestInt = current;
                //    }
                //}
                //
                //sum += int.Parse(line[largestIdx] + "" + line[secondLargestIdx]);
                sum += GetJoltage(line, 2);
            }

            return sum;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long sum = 0;
            foreach (string line in lines)
            {
                sum += GetJoltage(line, 12);
            }

            return sum;
        }

        private static long GetJoltage(string bank, int length)
        {
            StringBuilder sb = new StringBuilder();
            
            int[] largestIdx = new int[length + 1];
            largestIdx[0] = -1;
            for (int i = 0; i < length; i++)
            {
                int largest = int.Parse(bank[largestIdx[i] + 1] + "");
                largestIdx[i + 1] = largestIdx[i] + 1;
                for (int j = largestIdx[i] + 1; j < bank.Length - (length - (i + 1)); j++)
                {
                    int current = int.Parse(bank[j] + "");
                    if (current > largest)
                    {
                        largestIdx[i + 1] = j;
                        largest = current;
                    }
                }
            }

            for (int i = 1; i < largestIdx.Length; i++)
            {
                sb.Append(bank[largestIdx[i]]);
            }

            return long.Parse(sb.ToString());

        }
    }
}
