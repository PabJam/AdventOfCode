using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day01 : IDay
    {
        private static int StrToNum(string s, bool reverse)
        {
            string[] digits = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            if (reverse)
            {
                s = Utils.ReverseString(s);
            }
            for (int i = 0; i < digits.Length; i++)
            {
                if (s.Contains(digits[i]) == true)
                {
                    return i + 1;
                }
            }
            return -1;
        }

        public static long Part_1(string input)
        {
            string[] codes = input.Split('\n');
            int count = 0;
            for (int i = 0; i < codes.Length; i++)
            {
                int first = 0, last = 0;
                for (int j = 0; j < codes[i].Length; j++)
                {
                    if (Char.IsDigit(codes[i][j]) == true)
                    {
                        first = (int)Char.GetNumericValue(codes[i][j]);
                        break;
                    }
                }
                for (int j = codes[i].Length - 1; j > -1; j--)
                {
                    if (Char.IsDigit(codes[i][j]) == true)
                    {
                        last = (int)Char.GetNumericValue(codes[i][j]);
                        break;
                    }
                }
                count += Int32.Parse(first.ToString() + last.ToString());
            }
            return count;
        }

        public static long Part_2(string input)
        {
            string[] codes = input.Split('\n');
            int count = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < codes.Length; i++)
            {
                int first = 0, last = 0;
                sb.Clear();
                for (int j = 0; j < codes[i].Length; j++)
                {
                    if (Char.IsDigit(codes[i][j]) == true)
                    {
                        first = (int)Char.GetNumericValue(codes[i][j]);
                        break;
                    }
                    else
                    {
                        sb.Append(codes[i][j]);
                        int x = StrToNum(sb.ToString(), false);
                        if (x > 0)
                        {
                            first = x;
                            break;
                        }
                    }
                }
                sb.Clear();
                for (int j = codes[i].Length - 1; j > -1; j--)
                {
                    if (Char.IsDigit(codes[i][j]) == true)
                    {
                        last = (int)Char.GetNumericValue(codes[i][j]);
                        break;
                    }
                    else
                    {
                        sb.Append(codes[i][j]);
                        int x = StrToNum(sb.ToString(), true);
                        if (x > 0)
                        {
                            last = x;
                            break;
                        }
                    }
                }
                count += Int32.Parse(first.ToString() + last.ToString());
            }
            return count;
        }
    }
}
