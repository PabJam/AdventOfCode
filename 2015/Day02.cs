using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2015
{
    public class Day02 : IDay
    {
        public static long Part_1(string input)
        {
            string[] dimensionsStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[][] dimensions = new string[dimensionsStr.Length][];
            long area = 0;
            for (int i = 0; i < dimensions.Length; i++)
            {
                dimensions[i] = dimensionsStr[i].Split("x", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                long length = long.Parse(dimensions[i][0]);
                long width = long.Parse(dimensions[i][1]);
                long height = long.Parse(dimensions[i][2]);
                long smallest;
                if (length >= width && length >= height) { smallest = height * width; }
                else if (width >= length && width >= height) { smallest = length * height; }
                else { smallest = length * width; }
                area += 2 * length * width + 2 * width * height + 2 * height * length + smallest;
            }
            return area;
        }

        public static long Part_2(string input)
        {
            string[] dimensionsStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[][] dimensions = new string[dimensionsStr.Length][];
            long ribbon = 0;
            for (int i = 0; i < dimensions.Length; i++)
            {
                dimensions[i] = dimensionsStr[i].Split("x", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                long length = long.Parse(dimensions[i][0]);
                long width = long.Parse(dimensions[i][1]);
                long height = long.Parse(dimensions[i][2]);
                long smallest;
                if (length >= width && length >= height) { smallest = height + height + width + width; }
                else if (width >= length && width >= height) { smallest = length + length + height + height; }
                else { smallest = length + length + width + width; }
                ribbon += length * width * height + smallest;
            }
            return ribbon;
        }
    }
}
