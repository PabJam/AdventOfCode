using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2015
{
    public class Day07 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            bool containsA = false;
            Dictionary<string, ushort> signals = new Dictionary<string, ushort>();
            do
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string reciever = Regex.Match(lines[i], @"-> (\w+)").Groups[1].Value;
                    if (reciever == "a") { containsA = true; }
                    if (lines[i].Contains("NOT"))
                    {
                        string sender = Regex.Match(lines[i], @"^NOT (\w+) ->").Groups[1].Value;
                        if (signals.ContainsKey(sender) == false) { continue; }
                        if (signals.ContainsKey(reciever) == false)
                        {
                            signals.Add(reciever, 0);
                        }
                        signals[reciever] = (ushort)~signals[sender];
                    }
                    else if (lines[i].Contains("AND"))
                    {
                        Match match = Regex.Match(lines[i], @"^(\w+) AND (\w+) ->");
                        string sender1 = match.Groups[1].Value;
                        string sender2 = match.Groups[2].Value;
                        if (signals.ContainsKey(sender1) == false || signals.ContainsKey(sender2) == false) { continue; }
                        if (signals.ContainsKey(reciever) == false)
                        {
                            signals.Add(reciever, 0);
                        }
                        signals[reciever] = (ushort)(signals[sender1] & signals[sender2]);
                    }
                    else if (lines[i].Contains("OR"))
                    {
                        Match match = Regex.Match(lines[i], @"^(\w+) OR (\w+) ->");
                        string sender1 = match.Groups[1].Value;
                        string sender2 = match.Groups[2].Value;
                        if (signals.ContainsKey(sender1) == false || signals.ContainsKey(sender2) == false) { continue; }
                        if (signals.ContainsKey(reciever) == false)
                        {
                            signals.Add(reciever, 0);
                        }
                        signals[reciever] = (ushort)(signals[sender1] | signals[sender2]);
                    }
                    else if (lines[i].Contains("LSHIFT"))
                    {
                        Match match = Regex.Match(lines[i], @"^(\w+) LSHIFT (\d+) ->");
                        string sender = match.Groups[1].Value;
                        ushort shift = ushort.Parse(match.Groups[2].Value);
                        if (signals.ContainsKey(sender) == false) { continue; }
                        if (signals.ContainsKey(reciever) == false)
                        {
                            signals.Add(reciever, 0);
                        }
                        signals[reciever] = (ushort)(signals[sender] << shift);
                    }
                    else if (lines[i].Contains("RSHIFT"))
                    {
                        Match match = Regex.Match(lines[i], @"^(\w+) RSHIFT (\d+) ->");
                        string sender = match.Groups[1].Value;
                        ushort shift = ushort.Parse(match.Groups[2].Value);
                        if (signals.ContainsKey(sender) == false) { continue; }
                        if (signals.ContainsKey(reciever) == false)
                        {
                            signals.Add(reciever, 0);
                        }
                        signals[reciever] = (ushort)(signals[sender] >> shift);
                    }
                    else
                    {
                        Match match = Regex.Match(lines[i], @"^(.+) ->");
                        if (ushort.TryParse(match.Groups[1].Value, out ushort value) == false)
                        {
                            string sender = match.Groups[1].Value;
                            if (signals.ContainsKey(sender) == false) { continue; }
                            value = signals[sender];
                        }
                        if (signals.TryAdd(reciever, value) == false)
                        {
                            signals[reciever] = value;
                        }
                    }
                }
                if (signals.ContainsKey("a")) { return signals["a"]; }
            } while (containsA == true);
            return -1; 
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
