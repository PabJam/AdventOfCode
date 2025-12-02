using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2025
{
    public class Day01 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long counter = 0;
            long idx = 50;
            for (long i = 0; i < lines.Length; i++)
            {
                long turns = long.Parse(lines[i].Substring(1, lines[i].Length - 1));
                long direction = lines[i][0] == 'R' ? 1 : lines[i][0] == 'L' ? -1 : 0;
                idx += turns * direction;
                while (idx < 0 || idx > 99)
                {
                    idx += 100 * direction * -1;
                }
                if (idx == 0) { counter++; }
            }
            return counter;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            long counter = 0;
            long idx = 50;
            for (long i = 0; i < lines.Length; i++)
            {
                long turns = long.Parse(lines[i].Substring(1, lines[i].Length - 1));
                long direction = lines[i][0] == 'R' ? 1 : lines[i][0] == 'L' ? -1 : 0;
                
                while (turns > 99)
                {
                    counter++;
                    turns -= 100;
                }
                if (turns != 0)
                {
                    idx += turns * direction;
                    if (idx == 0) { counter++; }
                    else if (idx == 100) { idx = 0; counter++; }
                    else if (idx < 0 || idx > 99)
                    {
                        if (idx - turns * direction == 0) { counter--; }
                        idx += 100 * direction * -1;
                        counter++;
                    }
                   
                }
            }
            return counter;
        }
    }
}
