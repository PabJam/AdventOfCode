using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day15 : IDay
    {
        public static long Part_1(string input)
        {
            string[] codes = input.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long result = 0;
            for (int i = 0; i < codes.Length; i++)
            {
                result += GetHashValue(codes[i]);
            }
            return result;
        }

        public static long Part_2(string input)
        {
            string[] codes = input.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long result = 0;
            InsertionOrderDictionary<string, int>[] boxes = new InsertionOrderDictionary<string, int>[256];

            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i] = new InsertionOrderDictionary<string, int>();
            }
            StringBuilder label = new StringBuilder();
            char sign = '-';
            int focalLength = 0;
            for (int i = 0; i < codes.Length; i++)
            {
                label.Clear();
                for (int j = 0; j < codes[i].Length; j++)
                {
                    if (codes[i][j] == '-' || codes[i][j] == '=')
                    {
                        sign = codes[i][j];
                        if (sign == '=')
                        {
                            focalLength = Int32.Parse(codes[i].Substring(j + 1));
                        }
                        break;
                    }
                    label.Append(codes[i][j]);
                }
                string labelStr = label.ToString();
                int hash = GetHashValue(labelStr);
                if (sign == '=')
                {
                    if (boxes[hash].Contains(labelStr) == true)
                    {
                        boxes[hash][labelStr] = focalLength;
                    }
                    else
                    {
                        boxes[hash].Add(labelStr, focalLength);
                    }
                }
                else if (sign == '-')
                {
                    if (boxes[hash].Contains(labelStr) == true)
                    {
                        boxes[hash].Remove(labelStr);
                    }
                }
                else
                {
                    throw new Exception("Sign should always be = or -");
                }
            }
            for (int i = 0; i < boxes.Length; i++)
            {
                for (int j = 0; j < boxes[i].Count; j++)
                {
                    result += (i + 1) * (j + 1) * boxes[i][j];
                }
            }
            return result;
        }

        private static int GetHashValue(string code)
        {
            int result = 0;
            byte[] bytes = Encoding.UTF8.GetBytes(code);
            for (int i = 0; i < bytes.Length; i++)
            {
                result += bytes[i];
                result *= 17;
                result %= 256;
            }

            return result;
        }
    }
}
