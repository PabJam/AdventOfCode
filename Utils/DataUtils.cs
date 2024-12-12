using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class DataUtils
    {
        /// <summary>
        /// Turns an int into a string with a base of the characters specified in baseChars
        /// </summary>
        public static string IntToString(int value, char[] baseChars)
        {
            // 32 is the worst cast buffer size for base 2 and int.MaxValue
            int i = 32;
            char[] buffer = new char[i];
            int targetBase = baseChars.Length;

            do
            {
                buffer[--i] = baseChars[value % targetBase];
                value = value / targetBase;
            }
            while (value > 0);

            char[] result = new char[32 - i];
            Array.Copy(buffer, i, result, 0, 32 - i);

            return new string(result);
        }

        // Checks if position is in bounds of grid
        public static bool InBound((int, int) pos, int xLen, int yLen)
        {
            if (pos.Item1 < 0 || pos.Item1 > xLen - 1) { return false; }
            if (pos.Item2 < 0 || pos.Item2 > yLen - 1) { return false; }
            return true;
        }
    }
}
