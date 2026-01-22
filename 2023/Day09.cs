using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day09 : IDay
    {
        public static long Part_1(string input)
        {
            string[] sequenceStr = input.Split('\n');
            List<int[]> sequences = new List<int[]>();
            long result = 0;
            for (int i = 0; i < sequenceStr.Length; i++)
            {
                sequences.Add(Utils.NumStringToIntArr(sequenceStr[i]));
            }

            int[] subSequence;
            List<int> nextSubSequence = new List<int>();
            List<int> lastNumInSequences = new List<int>();
            for (int i = 0; i < sequences.Count; i++)
            {
                subSequence = sequences[i];
                lastNumInSequences.Clear();
                lastNumInSequences.Add(sequences[i][sequences[i].Length - 1]);
                while (true)
                {
                    for (int j = 0; j < subSequence.Length - 1; j++)
                    {
                        nextSubSequence.Add(subSequence[j + 1] - subSequence[j]);
                    }
                    lastNumInSequences.Add(nextSubSequence[nextSubSequence.Count - 1]);
                    bool allZero = true;
                    for (int j = 0; j < nextSubSequence.Count; j++)
                    {
                        if (nextSubSequence[j] != 0)
                        {
                            allZero = false; break;
                        }
                    }
                    subSequence = nextSubSequence.ToArray();
                    nextSubSequence.Clear();
                    if (allZero == true)
                    {
                        for (int j = 0; j < lastNumInSequences.Count; j++)
                        {
                            result += lastNumInSequences[j];
                        }
                        break;
                    }
                }
            }

            return result;
        }

        public static long Part_2(string input)
        {
            string[] sequenceStr = input.Split('\n');
            List<int[]> sequences = new List<int[]>();
            long result = 0;
            for (int i = 0; i < sequenceStr.Length; i++)
            {
                sequences.Add(Utils.NumStringToIntArr(sequenceStr[i]));
            }

            int[] subSequence;
            List<int> nextSubSequence = new List<int>();
            List<int> firstNumInSequence = new List<int>();
            for (int i = 0; i < sequences.Count; i++)
            {
                subSequence = sequences[i];
                firstNumInSequence.Clear();
                firstNumInSequence.Add(sequences[i][0]);
                while (true)
                {
                    for (int j = 0; j < subSequence.Length - 1; j++)
                    {
                        nextSubSequence.Add(subSequence[j + 1] - subSequence[j]);
                    }
                    firstNumInSequence.Add(nextSubSequence[0]);
                    bool allZero = true;
                    for (int j = 0; j < nextSubSequence.Count; j++)
                    {
                        if (nextSubSequence[j] != 0)
                        {
                            allZero = false; break;
                        }
                    }
                    subSequence = nextSubSequence.ToArray();
                    nextSubSequence.Clear();
                    if (allZero == true)
                    {
                        int subResult = 0;
                        for (int j = firstNumInSequence.Count - 1; j > -1; j--)
                        {
                            subResult = firstNumInSequence[j] - subResult;
                        }
                        result += subResult;
                        break;
                    }
                }
            }

            return result;
        }
    }
}
