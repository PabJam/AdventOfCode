using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day02 : IDay
    {
        public static long Part_1(string input)
        {
            string[] games = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int output = 0;
            for (int i = 0; i < games.Length; i++)
            {
                bool legal = true;
                string[] pulls = games[i].Split("; ");
                List<char> pull0 = new List<char>();
                pull0.AddRange(pulls[0]);
                while(true)
                {
                    if (pull0[0] == ':')
                    {
                        pull0.RemoveAt(0);
                        pull0.RemoveAt(0);
                        pulls[0] = new string(pull0.ToArray());
                        break;
                    }
                    pull0.RemoveAt(0);
                }
                for (int j = 0; j < pulls.Length; j++)
                {
                    string[] pull = pulls[j].Split(", ");
                    for (int k = 0; k < pull.Length; k++)
                    {
                        string[] info = pull[k].Split(' ');
                        int count = Int32.Parse(info[0]);
                        switch (info[1])
                        {
                            case "red":
                                legal = (count <= 12);
                                break;
                            case "green":
                                legal = (count <= 13);
                                break;
                            case "blue":
                                legal = (count <= 14);
                                break;
                            default:
                                Console.WriteLine("Error");
                                break;
                        }
                        if (legal == false)
                        {
                            break;
                        }
                    }
                    if (legal == false)
                    {
                        break;
                    }
                }
                if (legal == true)
                {
                    output += i + 1;
                }
            }
            return output;
        }

        public static long Part_2(string input)
        {
            string[] games = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            int output = 0;
            for (int i = 0; i < games.Length; i++)
            {
                int red = 0, green = 0, blue = 0;
                string[] pulls = games[i].Split("; ");
                List<char> pull0 = new List<char>();
                pull0.AddRange(pulls[0]);
                while (true)
                {
                    if (pull0[0] == ':')
                    {
                        pull0.RemoveAt(0);
                        pull0.RemoveAt(0);
                        pulls[0] = new string(pull0.ToArray());
                        break;
                    }
                    pull0.RemoveAt(0);
                }
                for (int j = 0; j < pulls.Length; j++)
                {
                    string[] pull = pulls[j].Split(", ");
                    for (int k = 0; k < pull.Length; k++)
                    {
                        string[] info = pull[k].Split(' ');
                        int count = Int32.Parse(info[0]);
                        switch (info[1])
                        {
                            case "red":
                                red = count > red ? count : red;
                                break;
                            case "green":
                                green = count > green ? count : green;
                                break;
                            case "blue":
                                blue = count > blue ? count : blue;
                                break;
                            default:
                                Console.WriteLine("Error");
                                break;
                        }
                    }
                }
                output += red * green * blue;
            }
            return output;
        }
    }
}
