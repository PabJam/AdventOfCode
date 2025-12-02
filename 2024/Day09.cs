using _2025;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public class Day09 : IDay
    {
        public static long Part_1(string input)
        {
            List<int> memory = new List<int>();
            int id = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int digit = input[i] - '0';
                for (int j = 0; j < digit; j++)
                {
                    if (i % 2 == 0)
                    {
                        memory.Add(id);
                    }
                    else
                    {
                        memory.Add(-1);
                    }
                }
                if (i % 2 == 0) { id++; }
            }

            for (int i = 0; i < memory.Count; i++)
            {
                if (memory[i] == -1)
                {
                    while(true)
                    {
                        if (memory[memory.Count - 1] == -1)
                        {
                            memory.RemoveAt(memory.Count - 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    memory[i] = memory[memory.Count - 1];
                    memory.RemoveAt(memory.Count - 1);
                }
            }

            long sum = 0;
            for (int i = 0; i < memory.Count; i++)
            {
                sum += memory[i] * i;
            }
            return sum;
        }

        public static long Part_2(string input)
        {
            List<(int, int)> memory = new List<(int, int)>();
            int id = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int digit = input[i] - '0';
                if (i % 2 == 0)
                {
                    memory.Add((id, digit));
                    id++;
                }
                else
                {
                    memory.Add((-1, digit));
                }
            }

            HashSet<int> usedIds = new HashSet<int>();
            while (true)
            {
                for (int i = memory.Count - 1; i > -1; i--)
                {
                    bool swaped = false;
                    if (memory[i].Item1 == -1) { continue; }
                    if (usedIds.Contains(memory[i].Item1)) { continue; }
                    id = memory[i].Item1;
                    usedIds.Add(id);
                    int space = memory[i].Item2;
                    for (int j = 0; j < i; j++)
                    {
                        if (memory[j].Item1 != -1) { continue; }
                        if (memory[j].Item2 < space) { continue; }
                        if (memory[j].Item2 == space)
                        {
                            memory[j] = (id, space);
                            memory[i] = (-1, space);
                            JoinFreeSpace(i, ref memory);
                            swaped = true;
                            break;
                        }
                        else
                        {
                            memory.Insert(j, (id, space));
                            memory[j + 1] = (-1, memory[j + 1].Item2 - space);
                            memory[i + 1] = (-1, space);
                            JoinFreeSpace(i + 1, ref memory);
                            swaped = true;
                            break;
                        }
                    }
                    if (swaped == true) { break; }
                }
                if (usedIds.Contains(0)) { break; }
            }
            List<int> memList = new List<int>();

            for (int i = 0; i < memory.Count; i++)
            {
                for (int j = 0; j < memory[i].Item2; j++)
                {
                    memList.Add(memory[i].Item1);
                }
            }
            long sum = 0;
            for (int i = 0; i < memList.Count; i++)
            {
                if (memList[i] == -1) { continue; } 
                sum += memList[i] * i;
            }
            return sum;
        }

        private static void JoinFreeSpace(int idx, ref List<(int, int)> memory)
        {
            if (memory[idx].Item1 != -1) { throw new Exception("cant concat used memory"); }
            if (idx + 1 < memory.Count - 1 && memory[idx + 1].Item1 == -1)
            {
                memory[idx] = (-1, memory[idx].Item2 + memory[idx + 1].Item2);
                memory.RemoveAt(idx + 1);
            }
            if (idx - 1 > -1 && memory[idx - 1].Item1 == -1)
            {
                memory[idx] = (-1, memory[idx].Item2 + memory[idx - 1].Item2);
                memory.RemoveAt(idx - 1);
            }
        }

    }
}
