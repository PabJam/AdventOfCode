using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public class Day05 : Day
    {
        private static bool CheckLegal((long, long)[] rules, long[] print)
        {
            bool legal = true;
            for (int i = 0; i < print.Length; i++)
            {
                long[] before = new long[i];
                long[] after = new long[print.Length - i - 1];
                Array.Copy(print, before, i);
                Array.Copy(print, i + 1, after, 0, after.Length);
                for (int j = 0; j < rules.Length; j++)
                {
                    if (print[i] != rules[j].Item1 && print[i] != rules[j].Item2) { continue; }
                    if (print[i] == rules[j].Item1 && (before.Contains(rules[j].Item2) == false)) { continue; }
                    if (print[i] == rules[j].Item2 && (after.Contains(rules[j].Item2) == false)) { continue; }
                    legal = false;
                    break;
                }
                if (legal == false) { break; }
            }
            return legal;
        }

        private static long[] FixPrint((long, long)[] rules, long[] errPrint)
        {
            Dictionary<long, List<long>> before = new Dictionary<long, List<long>>();
            Dictionary<long, List<long>> after = new Dictionary<long, List<long>>();
            long[] fixedPrint = new long[errPrint.Length]; // TODO replace

            for (int i = 0; i < errPrint.Length; i++)
            {
                before.Add(errPrint[i], new List<long>());
                after.Add(errPrint[i], new List<long>());
                for (int j = 0; j < rules.Length; j++)
                {
                    if (rules[j].Item1 == errPrint[i] && errPrint.Contains(rules[j].Item2))
                    {
                        after[errPrint[i]].Add(rules[j].Item2);
                    }
                    else if (rules[j].Item2 == errPrint[i] && errPrint.Contains(rules[j].Item1))
                    {
                        before[errPrint[i]].Add(rules[j].Item1);
                    }
                }
            }
            for (int i = 0; i < errPrint.Length; i++)
            {
                (long, long) lowest = (0, long.MaxValue);
                foreach (KeyValuePair<long, List<long>> kvp in before)
                {
                    if (kvp.Value.Count < lowest.Item2) { lowest.Item1 = kvp.Key; lowest.Item2 = kvp.Value.Count; }
                }
                fixedPrint[i] = lowest.Item1;
                before.Remove(lowest.Item1);
            }
            //Array.Sort
            return fixedPrint;
        }

        public override long Part_1(string input)
        {
            string[] linesStr = input.Split("\r\n", StringSplitOptions.TrimEntries);
            List<string> rulesStr = new List<string>();
            List<string> printsStr = new List<string>();
            bool readRules = true;
            for (int i = 0; i < linesStr.Length; i++)
            {
                if (linesStr[i] == "") { readRules = false; continue; }
                if (readRules) { rulesStr.Add(linesStr[i]); }
                else { printsStr.Add(linesStr[i]); }
            }

            (long, long)[] rules = new (long, long)[rulesStr.Count];
            for (int i = 0; i < rulesStr.Count; i++)
            {
                string[] ruleStr = rulesStr[i].Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                rules[i].Item1 = long.Parse(ruleStr[0]);
                rules[i].Item2 = long.Parse(ruleStr[1]);
            }

            List<long>[] prints = new List<long>[printsStr.Count];
            for (int i = 0; i < printsStr.Count; i++)
            {
                string[] printStr = printsStr[i].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                prints[i] = new List<long>();
                for (int j = 0; j < printStr.Length; j++)
                {
                    prints[i].Add(long.Parse(printStr[j]));
                }
            }

            long sum = 0;
            for (int i = 0; i < prints.Length; i++)
            {
                long[] printsArr = prints[i].ToArray();
                if (CheckLegal(rules, printsArr))
                {
                    sum += printsArr[printsArr.Length / 2];
                }
            }
            return sum;
        }

        public override long Part_2(string input)
        {
            string[] linesStr = input.Split("\r\n", StringSplitOptions.TrimEntries);
            List<string> rulesStr = new List<string>();
            List<string> printsStr = new List<string>();
            bool readRules = true;
            for (int i = 0; i < linesStr.Length; i++)
            {
                if (linesStr[i] == "") { readRules = false; continue; }
                if (readRules) { rulesStr.Add(linesStr[i]); }
                else { printsStr.Add(linesStr[i]); }
            }

            (long, long)[] rules = new (long, long)[rulesStr.Count];
            for (int i = 0; i < rulesStr.Count; i++)
            {
                string[] ruleStr = rulesStr[i].Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                rules[i].Item1 = long.Parse(ruleStr[0]);
                rules[i].Item2 = long.Parse(ruleStr[1]);
            }

            List<long>[] prints = new List<long>[printsStr.Count];
            for (int i = 0; i < printsStr.Count; i++)
            {
                string[] printStr = printsStr[i].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                prints[i] = new List<long>();
                for (int j = 0; j < printStr.Length; j++)
                {
                    prints[i].Add(long.Parse(printStr[j]));
                }
            }

            long sum = 0;
            for (int i = 0; i < prints.Length; i++)
            {
                long[] printsArr = prints[i].ToArray();
                if (CheckLegal(rules, printsArr) == false)
                {
                    long[] fixedPrint = FixPrint(rules, printsArr);
                    sum += fixedPrint[fixedPrint.Length / 2];
                }
            }
            return sum;
        }
    }
}
