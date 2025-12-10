using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;
namespace _2025
{
    public class Day10 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] targets = new string[lines.Length];
            List<int>[][] buttons = new List<int>[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                targets[i] = parts[0].Substring(1, parts[0].Length - 2);
                buttons[i] = new List<int>[parts.Length - 2];
                for (int j = 1; j < parts.Length - 1; j++)
                {
                    buttons[i][j - 1] = new List<int>(); 
                    parts[j] = parts[j].Substring(1, parts[j].Length - 2);
                    string[] numbers = parts[j].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    for (int k = 0; k < numbers.Length; k++)
                    {
                        buttons[i][j-1].Add(int.Parse(numbers[k]));
                    }
                }

            }

            long sum = 0;
            Dictionary<string, long> map = new Dictionary<string, long>();
            List<string> currentLayer = new List<string>();
            for (int i = 0; i < targets.Length; i++)
            {
                map.Clear();
                currentLayer.Clear();

                string baseState = new string('.', targets[i].Length);
                
                map.Add(baseState, 0);
                currentLayer.Add(baseState);
                while (map.ContainsKey(targets[i]) == false)
                {
                    currentLayer = ExplorePaths(map, buttons[i], currentLayer);
                }
                sum += map[targets[i]];
            }
            return sum;
        }

        private static List<string> ExplorePaths(Dictionary<string, long> map, List<int>[] buttons, List<string> states)
        {
            List<string> addedStates = new List<string>();
            for (int i = 0; i < states.Count; i++)
            {
                for (int j = 0; j < buttons.Length; j++)
                {
                    string state = states[i];
                    state = ApplyButton(state, buttons[j]);
                    if (map.TryAdd(state, map[states[i]] + 1) == true)
                    {
                        addedStates.Add(state);
                    }
                }
            }
            return addedStates;
        }

        private static string ApplyButton(string state, List<int> button)
        {
            StringBuilder sbState = new StringBuilder(state);
            for (int i = 0; i < button.Count; i++)
            {
                sbState[button[i]] = sbState[button[i]] == '#' ? '.' : '#';
            }
            return sbState.ToString();
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] targets = new string[lines.Length];
            List<List<int>>[] buttons = new List<List<int>>[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                targets[i] = parts[parts.Length - 1].Substring(1, parts[parts.Length - 1].Length - 2);
                buttons[i] = new List<List<int>>();
                for (int j = 1; j < parts.Length - 1; j++)
                {
                    buttons[i].Add(new List<int>());
                    parts[j] = parts[j].Substring(1, parts[j].Length - 2);
                    string[] numbers = parts[j].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                    for (int k = 0; k < numbers.Length; k++)
                    {
                        buttons[i][j - 1].Add(int.Parse(numbers[k]));
                    }
                }

            }

            long sum = 0;
            //List<int> presses;
            List<int> target = new List<int>();
            //List<List<int>> sortedButtons;
            //List<int> state;
            List<List<(int, List<int>)>> maximumPresses = new List<List<(int, List<int>)>>();
            List<List<int>> maximumPressesPerButton = new List<List<int>>();
            for (int i = 0; i < targets.Length; i++)
            {
                maximumPressesPerButton.Add(Enumerable.Repeat(int.MaxValue, buttons[i].Count).ToList());
                maximumPresses.Add(new List<(int, List<int>)>());
                string[] nums = targets[i].Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                for(int j = 0; j < nums.Length; j++)
                {
                    target.Add(int.Parse(nums[j]));
                }
                for (int j = 0; j < target.Count; j++)
                {
                    List<int> buttonsWithTargetIdx = new List<int>();
                    for (int k = 0; k < buttons[i].Count; k++)
                    {
                        
                        if (buttons[i][k].Contains(j))
                        {
                            buttonsWithTargetIdx.Add(k);
                        }
                        
                    }
                    maximumPresses[i].Add((target[j], buttonsWithTargetIdx));
                }

                for (int j = 0; j < maximumPresses[i].Count; j++)
                {
                    for(int k = 0; k < maximumPresses[i][j].Item2.Count; k++)
                    {
                        if (maximumPressesPerButton[i][maximumPresses[i][j].Item2[k]] > maximumPresses[i][j].Item1)
                        {
                            maximumPressesPerButton[i][maximumPresses[i][j].Item2[k]] = maximumPresses[i][j].Item1;
                        }
                    }
                }
            }
            return sum;
        }

        private static bool CeckValid(List<int> state, List<int> target)
        {
            for (int i = 0; i < state.Count; i++)
            {
                if (state[i]  > target[i]) { return false; }
            }
            return true;
        }
        private static List<int> GetState(List<List<int>> buttons, List<int> presses, int length)
        {
            List<int> state = Enumerable.Repeat(0, length).ToList();
            for (int i = 0; i < presses.Count; i++)
            {
                for (int j = 0; j < buttons[i].Count; j++)
                {
                    state[buttons[i][j]] += presses[i]; 
                }
            }
            return state;
        }

        private static List<List<int>> ExploreJoltage(Dictionary<string, long> map, List<List<int>> buttons, List<List<int>> states, List<int> target)
        {
            List<List<int>> addedStates = new List<List<int>>();
            for (int i = 0; i < states.Count; i++)
            {
                for (int j = 0; j < buttons.Count; j++)
                {
                    List<int> state = new List<int>(states[i]); 
                    string baseStateStr = string.Join(",", state.Select(n => n.ToString()).ToArray());
                    state = ApplyButtonJoltage(state, buttons[j]);
                    bool tooLarge = false;
                    for (int k = 0; k < state.Count; k++)
                    {
                        if (state[k] > target[k]) { tooLarge = true; break; }
                    }
                    if (tooLarge) { continue; }
                    string stateStr = string.Join(",", state.Select(n => n.ToString()).ToArray());
                    if (map.TryAdd(stateStr, map[baseStateStr] + 1) == true)
                    {
                        addedStates.Add(state);
                    }
                }
            }
            return addedStates;
        }

        private static List<int> ApplyButtonJoltage(List<int> state, List<int> button)
        {
       
            for (int i = 0; i < button.Count; i++)
            {
                state[button[i]]++;
            }
            return state;
        }
    }
}
