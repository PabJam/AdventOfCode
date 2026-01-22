using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day08 : IDay
    {
        public static long Part_1(string input)
        {
            string[] LRMapStrings = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string LRStr = LRMapStrings[0];
            string[] maps = LRMapStrings[1].Split('\n');
            Dictionary<string, StringPair> mapDic = new Dictionary<string, StringPair>();
            string key, first, second;
            int count = 0;
            int lrIdx = 0;
            for (int i = 0; i < maps.Length; i++)
            {
                key = maps[i].Substring(0, 3);
                first = maps[i].Substring(7, 3);
                second = maps[i].Substring(12, 3);
                mapDic.Add(key, new StringPair(first, second));
            }
            key = "AAA";
            while (true)
            {
                key = mapDic[key].strPair[LRStr[lrIdx] == 'L' ? 0 : 1];
                count++;
                lrIdx++;
                if (key == "ZZZ")
                {
                    break;
                }
                if (lrIdx == LRStr.Length)
                {
                    lrIdx = 0;
                }

            }
            return count;
        }

        public static long Part_2(string input)
        {
            string[] LRMapStrings = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string LRStr = LRMapStrings[0];
            string[] maps = LRMapStrings[1].Split('\n');
            SortedList<string, StringPair> mapDic = new SortedList<string, StringPair>();
            string key, first, second;
            int lrIdx;
            for (int i = 0; i < maps.Length; i++)
            {
                key = maps[i].Substring(0, 3);
                first = maps[i].Substring(7, 3);
                second = maps[i].Substring(12, 3);
                mapDic.Add(key, new StringPair(first, second));
            }
            List<string> keys = new List<string>();
            for (int i = 0; i < mapDic.Count; i++)
            {
                if (mapDic.GetKeyAtIndex(i)[2] == 'A')
                {
                    keys.Add(mapDic.GetKeyAtIndex(i));
                }
            }
            ulong[] movesToHitZ = new ulong[keys.Count];
            ulong moveCount = 0;
            for (int i = 0; i < keys.Count; i++)
            {
                key = keys[i];
                moveCount = 0;
                lrIdx = 0;
                while (true)
                {
                    key = mapDic[key].strPair[LRStr[lrIdx] == 'L' ? 0 : 1];
                    moveCount++;
                    lrIdx++;
                    if (key[2] == 'Z')
                    {
                        movesToHitZ[i] = moveCount;
                        break;
                    }
                    if (lrIdx == LRStr.Length)
                    {
                        lrIdx = 0;
                    }

                }
            }

            return (long)Utils.LCMArr(movesToHitZ);
        }

        private struct Move
        {
            public Move(string current, string next, int lrIdx)
            {
                this.current = current;
                this.next = next;
                this.lrIdx = lrIdx;
            }
            public string current { get; }
            public string next { get; }
            public int lrIdx { get; }
        }
    }
}
