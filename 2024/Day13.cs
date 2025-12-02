using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public class Day13 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long[] x = new long[lines.Length];
            long[] y = new long[lines.Length];
            long[] xa = new long[lines.Length];
            long[] ya = new long[lines.Length];
            long[] xb = new long[lines.Length];
            long[] yb = new long[lines.Length];
            long[] na = new long[lines.Length];
            long[] nb = new long[lines.Length];
            long sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                x[i] = long.Parse(Regex.Match(lines[i], @"X=(\d+)").Groups[1].Value);
                y[i] = long.Parse(Regex.Match(lines[i], @"Y=(\d+)").Groups[1].Value);
                xa[i] = long.Parse(Regex.Match(lines[i], @"Button A: X\+(\d+)").Groups[1].Value);
                ya[i] = long.Parse(Regex.Match(lines[i], @"Button A: X\+\d+, Y\+(\d+)").Groups[1].Value);
                xb[i] = long.Parse(Regex.Match(lines[i], @"Button B: X\+(\d+)").Groups[1].Value);
                yb[i] = long.Parse(Regex.Match(lines[i], @"Button B: X\+\d+, Y\+(\d+)").Groups[1].Value);
                long numerator = x[i] * ya[i] - y[i] * xa[i];
                long denumerator = xb[i] * ya[i] - yb[i] * xa[i];
                if (numerator % denumerator != 0) { na[i] = -1; nb[i] = -1; continue; }
                nb[i] = numerator / denumerator;
                numerator = x[i] - nb[i] * xb[i];
                denumerator = xa[i];
                if (numerator % denumerator != 0) { na[i] = -1; nb[i] = -1; continue; }
                na[i] = numerator / denumerator;
                sum += na[i] * 3 + nb[i];
            }
            return sum;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            long[] x = new long[lines.Length];
            long[] y = new long[lines.Length];
            long[] xa = new long[lines.Length];
            long[] ya = new long[lines.Length];
            long[] xb = new long[lines.Length];
            long[] yb = new long[lines.Length];
            long[] na = new long[lines.Length];
            long[] nb = new long[lines.Length];
            long sum = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                x[i] = long.Parse(Regex.Match(lines[i], @"X=(\d+)").Groups[1].Value) + 10000000000000;
                y[i] = long.Parse(Regex.Match(lines[i], @"Y=(\d+)").Groups[1].Value) + 10000000000000;
                xa[i] = long.Parse(Regex.Match(lines[i], @"Button A: X\+(\d+)").Groups[1].Value);
                ya[i] = long.Parse(Regex.Match(lines[i], @"Button A: X\+\d+, Y\+(\d+)").Groups[1].Value);
                xb[i] = long.Parse(Regex.Match(lines[i], @"Button B: X\+(\d+)").Groups[1].Value);
                yb[i] = long.Parse(Regex.Match(lines[i], @"Button B: X\+\d+, Y\+(\d+)").Groups[1].Value);
                long numerator = x[i] * ya[i] - y[i] * xa[i];
                long denumerator = xb[i] * ya[i] - yb[i] * xa[i];
                if (numerator % denumerator != 0) { na[i] = -1; nb[i] = -1; continue; }
                nb[i] = numerator / denumerator;
                numerator = x[i] - nb[i] * xb[i];
                denumerator = xa[i];
                if (numerator % denumerator != 0) { na[i] = -1; nb[i] = -1; continue; }
                na[i] = numerator / denumerator;
                sum += na[i] * 3 + nb[i];
            }
            return sum;
        }

        
    }
}
