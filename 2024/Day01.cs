using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day01 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split("\r\n");
            long[] leftNums = new long[lines.Length];
            long[] rightNums = new long[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] lrSplit = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                leftNums[i] = long.Parse(lrSplit[0]);
                rightNums[i] = long.Parse(lrSplit[1]);
            }
            Array.Sort(leftNums);
            Array.Sort(rightNums);

            long sum = 0;
            for (int i = 0; i < leftNums.Length; i++)
            {
                sum += Math.Abs(leftNums[i] - rightNums[i]);
            }

            return sum;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split("\r\n");
            long[] leftNums = new long[lines.Length];
            long[] rightNums = new long[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] lrSplit = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                leftNums[i] = long.Parse(lrSplit[0]);
                rightNums[i] = long.Parse(lrSplit[1]);
            }
            Dictionary<long, long> appearences = new Dictionary<long, long>();
            for (int i = 0; i < rightNums.Length; i++)
            {
                if (appearences.ContainsKey(rightNums[i]))
                {
                    appearences[rightNums[i]]++;
                }
                else
                {
                    appearences.Add(rightNums[i], 1);
                }
            }
            long sum = 0;
            for (int i = 0; i < leftNums.Length; i++)
            {
                if (appearences.TryGetValue(leftNums[i], out long value))
                {
                    sum += leftNums[i] * value;
                }
            }
            return sum;
        }

        // More like slowsort... TODO replace function with one which does not alloc more dyn mem and prob not recursiv
        //public static void Quicksort(ref long[] arr)
        //{
        //    if (arr.Length <= 1) { return; }
        //    int pivotIdx = arr.Length / 2;
        //    long pivot = arr[pivotIdx];
        //
        //    List<long> lowerList = new List<long>();
        //    List<long> higherList = new List<long>();
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        if (i == pivotIdx) { continue; }
        //        if (arr[i] < pivot) { lowerList.Add(arr[i]); }
        //        else { higherList.Add(arr[i]); }
        //    }
        //    arr[lowerList.Count] = pivot;
        //    long[] lower = lowerList.ToArray();
        //    long[] higher = higherList.ToArray();
        //    Quicksort(ref lower);
        //    Quicksort(ref higher);
        //    lower.CopyTo(arr, 0);
        //    higher.CopyTo(arr, lower.Length + 1);
        //} 
    }
}
