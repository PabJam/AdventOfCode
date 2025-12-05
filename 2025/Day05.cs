using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2025
{
    public class Day05 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int itemStart = 0;
            List<(long, long)> ranges = new List<(long, long)>();
            long counter = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int rangeIdx = lines[i].IndexOf('-');
                if (rangeIdx == -1)
                {
                    itemStart = i;
                    break;
                }
                long lower = long.Parse(lines[i].Substring(0, rangeIdx));
                long higher = long.Parse(lines[i].Substring(rangeIdx + 1, lines[i].Length - 1 - rangeIdx));
                ranges.Add((lower, higher));

            }
            for (int i = itemStart; i < lines.Length; i++)
            {
                long item = long.Parse(lines[i]);
                for (int j = 0; j < ranges.Count; j++)
                {
                    if (item < ranges[j].Item1) { continue; }
                    if (item <= ranges[j].Item2 ) { counter++; break; }
                }
            }

            return counter;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<(long, long)> ranges = new List<(long, long)>();
            long counter = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int rangeIdx = lines[i].IndexOf('-');
                if (rangeIdx == -1)
                {
                    break;
                }
                long lower = long.Parse(lines[i].Substring(0, rangeIdx));
                long higher = long.Parse(lines[i].Substring(rangeIdx + 1, lines[i].Length - 1 - rangeIdx));
                ranges.Add((lower, higher));

            }
            List<(long, long)> realRanges = new List<(long, long)>();
            while (true)
            {
                bool mergedAnything = false;
                for (int i = 0; i < ranges.Count; i++)
                {
                    bool merged = false;
                    for (int j = 0; j < realRanges.Count; j++)
                    {
                        if (ranges[i].Item1 >= realRanges[j].Item1 && ranges[i].Item1 <= realRanges[j].Item2)
                        {
                            mergedAnything = true; 
                            merged = true;
                            realRanges[j] = (realRanges[j].Item1, ranges[i].Item2 > realRanges[j].Item2 ? ranges[i].Item2 : realRanges[j].Item2);
                            break;
                        }
                        if (ranges[i].Item2 >= realRanges[j].Item1 && ranges[i].Item2 <= realRanges[j].Item2)
                        {
                            mergedAnything = true;
                            merged = true;
                            realRanges[j] = (ranges[i].Item1 < realRanges[j].Item1 ? ranges[i].Item1 : realRanges[j].Item1, realRanges[j].Item2);
                            break;
                        }
                        if (ranges[i].Item1 <= realRanges[j].Item1 && ranges[i].Item2 >= realRanges[j].Item2)
                        {
                            mergedAnything = true;
                            merged = true;
                            realRanges[j] = (ranges[i].Item1, ranges[i].Item2);
                            break;
                        }
                    }
                    if (merged == false)
                    {
                        realRanges.Add(ranges[i]);
                    }
                }

                if (mergedAnything == false)
                {
                    break;
                }
                else
                {
                    ranges.Clear();
                    ranges.AddRange(realRanges);
                    realRanges.Clear();
                }
            }
            

            foreach (var range in realRanges)
            {
                counter += range.Item2 - range.Item1 + 1;
            }

            return counter;
        }
    }
}
