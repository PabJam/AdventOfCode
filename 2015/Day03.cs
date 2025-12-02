using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2015
{
    public class Day03 : IDay
    {
        
        public static long Part_1(string input)
        {
            Dictionary<(int,int), int> timesAtHouse = new Dictionary<(int,int), int>();
            (int, int) position = (0, 0);
            timesAtHouse.TryAdd(position, 1);
            for (int i = 0; i < input.Length; i++)
            {
                switch(input[i])
                {
                    case '>':
                        position.Item1++;
                        break;
                    case 'v':
                        position.Item2--;
                        break;
                    case '<':
                        position.Item1--;
                        break;
                    case '^':
                        position.Item2++;
                        break;
                }
                if (timesAtHouse.TryAdd(position, 1) == false)
                {
                    timesAtHouse[position]++;
                }
            }
            return timesAtHouse.Count;
        }

        public static long Part_2(string input)
        {
            Dictionary<(int, int), int> timesAtHouse = new Dictionary<(int, int), int>();
            (int, int) position = (0, 0);
            (int, int) positionRobo = (0, 0);
            timesAtHouse.TryAdd(position, 2);
            for (int i = 0; i < input.Length; i++)
            {
                if (i % 2 == 0)
                {
                    MovePos(ref position, input[i], timesAtHouse);
                }
                else
                {
                    MovePos(ref positionRobo, input[i], timesAtHouse);
                }
            }
            return timesAtHouse.Count;
        }

        private static void MovePos(ref (int, int) pos, char dir, Dictionary<(int, int), int> visited)
        {
            switch (dir)
            {
                case '>':
                    pos.Item1++;
                    break;
                case 'v':
                    pos.Item2--;
                    break;
                case '<':
                    pos.Item1--;
                    break;
                case '^':
                    pos.Item2++;
                    break;
            }
            if (visited.TryAdd(pos, 1) == false)
            {
                visited[pos]++;
            }
        }
    }
}
