using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public struct StringPair
    {
        public string[] strPair { get; }
        public StringPair(string first, string second)
        {
            strPair = [first, second];
        }
    }

    public enum Relation
    {
        SmallerThan = 0,
        BiggerThan = 1,
        Equal = 2,
    }

    public enum Directions
    {
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3,
        DownRight = 4,
        DownLeft = 5,
        UpLeft = 6,
        UpRight = 7,
    }

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

        /// <summary>
        /// Turns an input string into a grid with the origin in the bottom left corner.
        /// first idx is x coord and second index is y coord
        /// </summary>
        /// <param name="str">puzzle input string</param>
        /// <returns></returns>
        public static Dictionary<Vector2Int, char> StringToGrid(string str)
        {
            string[] lines = str.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Dictionary<Vector2Int, char> grid = new Dictionary<Vector2Int, char>();
            for (int x = 0; x < lines[0].Length; x++)
            {
                for (int y = 0; y < lines.Length; y++)
                {
                    Vector2Int pos = new Vector2Int(x, y);
                    grid.Add(pos, lines[lines.Length - 1 - y][x]);
                }
            }

            return grid;
        }
    }
}
