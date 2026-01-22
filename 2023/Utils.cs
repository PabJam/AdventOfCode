using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public static class Utils
    {
        public static bool CheckOOB(string[] map, Vector2Int pos)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= map[0].Length || pos.y >= map.Length)
            {
                return true;
            }
            return false;
        }

        public static bool CheckOOB<T>(T[][] map, Vector2Int pos)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= map[0].Length || pos.y >= map.Length)
            {
                return true;
            }
            return false;
        }

        public static string ReverseString(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = s.Length - 1; i > -1; i--)
            {
                sb.Append(s[i]);
            }
            return sb.ToString();
        }

        public static void ReverseArray<T>(ref T[] arr)
        {
            T temp;
            for (int i = 0; i < arr.Length / 2; i++)
            {
                temp = arr[arr.Length - 1 - i];
                arr[arr.Length - 1 - i] = arr[i];
                arr[i] = temp;
            }
        }

        public static void Populate<T>(T[] arr, T value)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                arr[i] = value;
            }
        }

        public static void QuickSort<T>(T[] arr) where T : IComparable<T>
        {

            QuickSort(arr, 0, arr.Length);
        }

        private static void QuickSort<T>(T[] arr, int startIdx, int length) where T : IComparable<T>
        {
            if (length < 2)
            {
                return;
            }
            int pivotIdx = startIdx + (int)(length / 2.0f);
            T pivot = arr[pivotIdx];
            List<T> low = new List<T>();
            List<T> high = new List<T>();
            for (int i = startIdx; i < pivotIdx; i++)
            {
                if (arr[i].CompareTo(pivot) < 0)
                {
                    low.Add(arr[i]);
                }
                else
                {
                    high.Add(arr[i]);
                }
            }
            for (int i = startIdx + length - 1; i > pivotIdx; i--)
            {
                if (arr[i].CompareTo(pivot) < 0)
                {
                    low.Add(arr[i]);
                }
                else
                {
                    high.Add(arr[i]);
                }
            }
            arr[startIdx + low.Count] = pivot;
            Array.Copy(low.ToArray(), 0, arr, startIdx, low.Count);
            Array.Copy(high.ToArray(), 0, arr, startIdx + low.Count + 1, high.Count);
            int lowCount, highCount;
            lowCount = low.Count;
            highCount = high.Count;
            low.Clear();
            high.Clear();
            QuickSort(arr, startIdx, lowCount);
            QuickSort(arr, startIdx + lowCount + 1, highCount);
        }

        /// <summary>
        /// convertes a string of form Text: num1 num2 num3 
        /// to int array
        /// </summary>
        /// <returns>long array {num1, num2, num3}</returns>
        public static long[] StringColonNumsToLongArr(string str)
        {
            StringBuilder sb = new StringBuilder();
            bool hitColon = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ':') { hitColon = true; continue; }
                if (hitColon == false) { continue; }
                sb.Append(str[i]);
            }
            return NumStringToLongArr(sb.ToString());
        }

        /// <summary>
        /// converts a string of form : num1 num2 num3
        /// to int array
        /// </summary>
        /// <param name="str"></param>
        /// <returns>long array {num1, num2, num3}</returns>
        public static long[] NumStringToLongArr(string str)
        {
            string[] numsStr = str.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long[] numsI = new long[numsStr.Length];
            for (int i = 0; i < numsStr.Length; i++)
            {
                numsI[i] = Int64.Parse(numsStr[i]);
            }
            return numsI;
        }

        /// <summary>
        /// converts a string of form : Num1SeperatorNum2SeperatorNum3
        /// to int array
        /// </summary>
        /// <param name="str"></param>
        /// <returns>long array {num1, num2, num3}</returns>
        public static long[] NumStringToLongArr(string str, char seperator)
        {
            string[] numsStr = str.Split(seperator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long[] numsI = new long[numsStr.Length];
            for (int i = 0; i < numsStr.Length; i++)
            {
                numsI[i] = Int64.Parse(numsStr[i]);
            }
            return numsI;
        }

        /// <summary>
        /// convertes a string of form Text: num1 num2 num3 
        /// to int array
        /// </summary>
        /// <returns>int array {num1, num2, num3}</returns>
        public static int[] StringColonNumsToIntArr(string str)
        {
            StringBuilder sb = new StringBuilder();
            bool hitColon = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ':') { hitColon = true; continue; }
                if (hitColon == false) { continue; }
                sb.Append(str[i]);
            }
            return NumStringToIntArr(sb.ToString());
        }

        /// <summary>
        /// converts a string of form : num1 num2 num3
        /// to int array
        /// </summary>
        /// <param name="str"></param>
        /// <returns>int array {num1, num2, num3}</returns>
        public static int[] NumStringToIntArr(string str)
        {
            string[] numsStr = str.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[] numsI = new int[numsStr.Length];
            for (int i = 0; i < numsStr.Length; i++)
            {
                numsI[i] = Int32.Parse(numsStr[i]);
            }
            return numsI;
        }

        /// <summary>
        /// converts a string of form : Num1SeperatorNum2SeperatorNum3
        /// to int array
        /// </summary>
        /// <param name="str"></param>
        /// <returns>int array {num1, num2, num3}</returns>
        public static int[] NumStringToIntArr(string str, char seperator)
        {
            string[] numsStr = str.Split(seperator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[] numsI = new int[numsStr.Length];
            for (int i = 0; i < numsStr.Length; i++)
            {
                numsI[i] = Int32.Parse(numsStr[i]);
            }
            return numsI;
        }

        public static ulong GCD(ulong x, ulong y)
        {
            ulong q, r;
            if (y > x)
            {
                ulong temp = y; y = x; x = temp;
            }
            (q, r) = ulong.DivRem(x, y);
            if (r == 0)
            {
                return y;
            }
            return GCD(y, r);
        }

        public static ulong LCM(ulong x, ulong y)
        {
            return (x * y) / GCD(x, y);
        }

        public static ulong LCMArr(ulong[] nums)
        {
            if (nums.Length < 2)
            {
                throw new Exception("nums array must at least have length of two");
            }
            ulong lcm;
            lcm = LCM(nums[0], nums[1]);
            for (int i = 2; i < nums.Length; i++)
            {
                lcm = LCM(nums[i], lcm);
            }
            return lcm;
        }

        public static SortedList<Vector2Int, char> StringToCharMap(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            SortedList<Vector2Int, char> map = new SortedList<Vector2Int, char>();
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    map.Add(new Vector2Int(x, y), lines[lines.Length - y - 1][x]);
                }
            }
            return map;
        }

        public static Dictionary<Vector2Int, char> StringToCharMapDic(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Dictionary<Vector2Int, char> map = new Dictionary<Vector2Int, char>();
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    map.Add(new Vector2Int(x, y), lines[lines.Length - y - 1][x]);
                }
            }
            return map;
        }

        public static SortedList<Vector2Int, char> StringToCharMap(string[] lines)
        {
            SortedList<Vector2Int, char> map = new SortedList<Vector2Int, char>();
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    map.Add(new Vector2Int(x, y), lines[lines.Length - y - 1][x]);
                }
            }
            return map;
        }

        public static ulong Factorial(ulong x)
        {
            ulong result = 1;
            for (ulong i = x; i > 0; i--)
            {
                result *= i;
            }
            return result;
        }

        public static ulong Binominal(ulong n, ulong k)
        {
            if (k > n) { throw new Exception("n must be >= k"); }
            if (k == 0) { return 1; }
            if (k > n / 2) { return Binominal(n, n - k); }
            return n * Binominal(n - 1, k - 1) / k;
        }

    }
}
