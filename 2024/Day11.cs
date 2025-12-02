using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day11 : IDay
    {

        public static long Part_1(string input)
        {
            string[] numStr = input.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<long> nums = new List<long>();

            for (int i = 0; i < numStr.Length; i++)
            {
                nums.Add(long.Parse(numStr[i]));
            }
            for (int i = 0; i < 25; i++)
            {
                nums = Iterate(nums);
            }
            return nums.Count;
        }

        public static long Part_2(string input)
        {
            string[] numStr = input.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Dictionary<long, long> nums = new Dictionary<long, long>();

            for (int i = 0; i < numStr.Length; i++)
            {
                long num = long.Parse(numStr[i]);
                if (nums.TryAdd(num, 1) == false)
                {
                    nums[num]++;
                }
            }
            for (int i = 0; i < 75; i++)
            {
                nums = SmartIterate(nums);
            }
            long sum = 0;
            foreach (long cnt in nums.Values)
            {
                sum += cnt;
            }
            return sum;
        }

        private static List<long> Iterate(List<long> nums)
        {
            List<long> result = new List<long>();
            for (int i = 0; i < nums.Count; i++)
            {
                if (nums[i] == 0) { result.Add(1); continue; }
                int numDigits = ((int)Math.Log10(nums[i])) + 1;
                if (numDigits % 2 == 0)
                {
                    result.Add(long.Parse(nums[i].ToString().Substring(0, numDigits / 2)));
                    result.Add(long.Parse(nums[i].ToString().Substring(numDigits / 2, numDigits / 2)));
                    continue;
                }
                result.Add(nums[i] * 2024);
            }

            return result;
        }

        private static Dictionary<long, long> SmartIterate(Dictionary<long, long> nums)
        {
            Dictionary<long, long> result = new Dictionary<long, long>();

            foreach (KeyValuePair<long, long> numCnt in nums)
            {
                if (numCnt.Key == 0)
                {
                    if (result.TryAdd(1, numCnt.Value) == false)
                    {
                        result[1] += numCnt.Value;
                    }
                    continue;
                }
                int numDigits = ((int)Math.Log10(numCnt.Key)) + 1;
                if (numDigits % 2 == 0)
                {
                    long front = long.Parse(numCnt.Key.ToString().Substring(0, numDigits / 2));
                    long back = long.Parse(numCnt.Key.ToString().Substring(numDigits / 2, numDigits / 2));
                    if (result.TryAdd(front, numCnt.Value) == false)
                    {
                        result[front] += numCnt.Value;
                    }
                    if (result.TryAdd(back, numCnt.Value) == false)
                    {
                        result[back] += numCnt.Value;
                    }
                    continue;
                }
                if (result.TryAdd(numCnt.Key * 2024, numCnt.Value) == false)
                {
                    result[numCnt.Key * 2024] += numCnt.Value;
                }
            }

            return result;
        }
    }
}
