using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day12 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] springs = new string[lines.Length];
            string[] numStrings = new string[lines.Length];
            string[] line;
            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split(' ');
                springs[i] = line[0];
                numStrings[i] = line[1];
            }
            int[][] nums = new int[numStrings.Length][];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = Utils.NumStringToIntArr(numStrings[i], ',');
            }

            int[][] qmIdx = new int[springs.Length][];
            int[] budget = new int[nums.Length];
            int tempBudget;
            List<int> tempQmIdx = new List<int>();
            for (int i = 0; i < qmIdx.Length; i++)
            {
                tempBudget = 0;
                tempQmIdx.Clear();
                for (int j = 0; j < nums[i].Length; j++)
                {
                    tempBudget += nums[i][j];
                }
                for (int j = 0; j < springs[i].Length; j++)
                {
                    if (springs[i][j] != '?')
                    {
                        if (springs[i][j] == '#')
                        {
                            tempBudget--;
                        }
                        continue;
                    }
                    tempQmIdx.Add(j);
                }
                qmIdx[i] = tempQmIdx.ToArray();
                budget[i] = tempBudget;
            }


            ulong result = 0;
            for (int i = 0; i < springs.Length; i++)
            {
                result += GetValidPermutations(springs[i], budget[i], qmIdx[i], nums[i]);
            }

            return (long)result;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] springs = new string[lines.Length];
            string[] numStrings = new string[lines.Length];
            string[] line;
            StringBuilder sbSprings = new StringBuilder();
            StringBuilder sbNums = new StringBuilder();
            int[][] nums = new int[numStrings.Length][];
            int[][] qmIdx = new int[springs.Length][];
            int[] budget = new int[nums.Length];
            int tempBudget;
            ulong result = 0;
            ulong[] results1 = new ulong[springs.Length];
            ulong[] results2 = new ulong[springs.Length];
            ulong[] quoutients = new ulong[springs.Length];
            ulong tempRes;
            List<int> tempQmIdx = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split(' ');
                sbSprings.Clear();
                sbNums.Clear();
                for (int j = 0; j < 1; j++)
                {
                    sbSprings.Append(line[0]);
                    sbSprings.Append('?');
                    sbNums.Append(line[1]);
                    sbNums.Append(',');
                }
                sbSprings.Remove(sbSprings.Length - 1, 1);
                sbNums.Remove(sbNums.Length - 1, 1);
                springs[i] = sbSprings.ToString();
                numStrings[i] = sbNums.ToString();
            }
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = Utils.NumStringToIntArr(numStrings[i], ',');
            }

            for (int i = 0; i < qmIdx.Length; i++)
            {
                tempBudget = 0;
                tempQmIdx.Clear();
                for (int j = 0; j < nums[i].Length; j++)
                {
                    tempBudget += nums[i][j];
                }
                for (int j = 0; j < springs[i].Length; j++)
                {
                    if (springs[i][j] != '?')
                    {
                        if (springs[i][j] == '#')
                        {
                            tempBudget--;
                        }
                        continue;
                    }
                    tempQmIdx.Add(j);
                }
                qmIdx[i] = tempQmIdx.ToArray();
                budget[i] = tempBudget;
            }

            for (int i = 0; i < springs.Length; i++)
            {
                results1[i] = GetValidPermutations(springs[i], budget[i], qmIdx[i], nums[i]);
            }


            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split(' ');
                sbSprings.Clear();
                sbNums.Clear();
                for (int j = 0; j < 2; j++)
                {
                    sbSprings.Append(line[0]);
                    sbSprings.Append('?');
                    sbNums.Append(line[1]);
                    sbNums.Append(',');
                }
                sbSprings.Remove(sbSprings.Length - 1, 1);
                sbNums.Remove(sbNums.Length - 1, 1);
                springs[i] = sbSprings.ToString();
                numStrings[i] = sbNums.ToString();
            }
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = Utils.NumStringToIntArr(numStrings[i], ',');
            }

            for (int i = 0; i < qmIdx.Length; i++)
            {
                tempBudget = 0;
                tempQmIdx.Clear();
                for (int j = 0; j < nums[i].Length; j++)
                {
                    tempBudget += nums[i][j];
                }
                for (int j = 0; j < springs[i].Length; j++)
                {
                    if (springs[i][j] != '?')
                    {
                        if (springs[i][j] == '#')
                        {
                            tempBudget--;
                        }
                        continue;
                    }
                    tempQmIdx.Add(j);
                }
                qmIdx[i] = tempQmIdx.ToArray();
                budget[i] = tempBudget;
            }


            for (int i = 0; i < springs.Length; i++)
            {
                ulong bin = Utils.Binominal((ulong)qmIdx[i].Length, (ulong)budget[i]);
                if (bin > 100000)
                {
                    continue;
                }
                results2[i] = GetValidPermutations(springs[i], budget[i], qmIdx[i], nums[i]);
                quoutients[i] = results2[i] / results1[i];
                //Console.WriteLine("Completed : [" + (i + 1).ToString() + "/" + springs.Length + "]");
                //result += results1[i] * (ulong)Math.Pow(quoutients[i], 4);
            }

            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split(' ');
                sbSprings.Clear();
                sbNums.Clear();
                for (int j = 0; j < 3; j++)
                {
                    sbSprings.Append(line[0]);
                    sbSprings.Append('?');
                    sbNums.Append(line[1]);
                    sbNums.Append(',');
                }
                sbSprings.Remove(sbSprings.Length - 1, 1);
                sbNums.Remove(sbNums.Length - 1, 1);
                springs[i] = sbSprings.ToString();
                numStrings[i] = sbNums.ToString();
            }
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = Utils.NumStringToIntArr(numStrings[i], ',');
            }

            for (int i = 0; i < qmIdx.Length; i++)
            {
                tempBudget = 0;
                tempQmIdx.Clear();
                for (int j = 0; j < nums[i].Length; j++)
                {
                    tempBudget += nums[i][j];
                }
                for (int j = 0; j < springs[i].Length; j++)
                {
                    if (springs[i][j] != '?')
                    {
                        if (springs[i][j] == '#')
                        {
                            tempBudget--;
                        }
                        continue;
                    }
                    tempQmIdx.Add(j);
                }
                qmIdx[i] = tempQmIdx.ToArray();
                budget[i] = tempBudget;
            }


            for (int i = 0; i < springs.Length; i++)
            {
                ulong bin = Utils.Binominal((ulong)qmIdx[i].Length, (ulong)budget[i]);
                if (bin > 100000)
                {
                    continue;
                }
                tempRes = GetValidPermutations(springs[i], budget[i], qmIdx[i], nums[i]);
                if (results2[i] * quoutients[i] != tempRes)
                {
                    Console.WriteLine("Error");
                }
                quoutients[i] = results2[i] / results1[i];
                Console.WriteLine("Completed : [" + (i + 1).ToString() + "/" + springs.Length + "]");
                //result += results1[i] * (ulong)Math.Pow(quoutients[i], 4);
            }

            return (long)result;
        }

        private static bool CheckValid(string spring, int[] nums)
        {
            List<int> chains = new List<int>();
            int chain = 0;
            for (int i = 0; i < spring.Length; i++)
            {
                if (spring[i] == '.')
                {
                    if (chain == 0) { continue; }
                    chains.Add(chain);
                    chain = 0;
                }
                else if (spring[i] == '#')
                {
                    chain++;
                }
                else
                {
                    throw new Exception("Spring string can only contain . or # characters");
                }
            }
            if (chain != 0)
            {
                chains.Add(chain);
            }
            return Enumerable.SequenceEqual(chains.ToArray(), nums);
        }

        private static ulong GetValidPermutations(string spring, int budged, int[] qmIdx, int[] nums)
        {
            if (budged == 0)
            {
                return CheckValid(spring.Replace('?', '.'), nums) ? 1ul : 0;
            }
            if (budged == qmIdx.Length)
            {
                return CheckValid(spring.Replace('?', '#'), nums) ? 1ul : 0;
            }
            ulong validPermutations = 0ul;
            int length = qmIdx.Length;
            int[] permutation = new int[budged];
            for (int i = 0; i < permutation.Length; i++)
            {
                permutation[i] = i;
            }
            string permStr = GetSpringReplacedQM(spring, permutation, qmIdx);
            if (CheckValid(permStr, nums) == true) { validPermutations++; }
            while (permutation[0] < length - permutation.Length)
            {
                permutation = GetNextPermutation(permutation, permutation.Length - 1, length);
                permStr = GetSpringReplacedQM(spring, permutation, qmIdx);
                if (CheckValid(permStr, nums) == true) { validPermutations++; }
            }
            return validPermutations;
        }

        private static int[] GetNextPermutation(int[] permutation, int incIdx, int length)
        {
            int[] newPermutation = new List<int>(permutation).ToArray();
            if (newPermutation[incIdx] < length - (newPermutation.Length - incIdx))
            {
                newPermutation[incIdx]++;
                return newPermutation;
            }
            newPermutation = GetNextPermutation(newPermutation, incIdx - 1, length);
            newPermutation[incIdx] = newPermutation[incIdx - 1] + 1;
            return newPermutation;
        }

        private static string GetSpringReplacedQM(string spring, int[] permutation, int[] _qmIdx)
        {
            StringBuilder sb = new StringBuilder(spring);
            int[] qmIdx = new List<int>(_qmIdx).ToArray();
            for (int i = 0; i < permutation.Length; i++)
            {
                sb[qmIdx[permutation[i]]] = '#';
                qmIdx[permutation[i]] = -1;
            }
            for (int i = 0; i < qmIdx.Length; i++)
            {
                if (qmIdx[i] > -1)
                {
                    sb[qmIdx[i]] = '.';
                }
            }
            return sb.ToString();
        }
    }
}
