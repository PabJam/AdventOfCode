using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2015
{
    public class Day01 : IDay
    {

        public static long Part_1(string input)
        {
            long floor = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(') { floor++; }
                else if (input[i] == ')') { floor--; }
            }
            return floor;
        }

        public static long Part_2(string input)
        {
            long floor = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(') { floor++; }
                else if (input[i] == ')') { floor--; }

                if (floor < 0) { return i + 1; }
            }
            return -1;
        }
    }
}
