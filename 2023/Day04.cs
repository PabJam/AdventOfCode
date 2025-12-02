using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day04 : IDay
    {
        public static long Part_1(string input)
        {
            string[] cards = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            List<StringBuilder> cardsSB = new List<StringBuilder>();
            int points = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                bool hit = false;
                cardsSB.Add(new StringBuilder());
                for (int j = 0; j < cards[i].Length; j++)
                {
                    if (cards[i][j] == ':')
                    {
                        hit = true;
                        continue;
                    }
                    if (hit == false)
                    {
                        continue;
                    }
                    cardsSB[i].Append(cards[i][j]);
                }
                int cardPoints = 0;
                string[] nums = cardsSB[i].ToString().Split('|');
                string[] solutions = nums[0].Split(' ');
                string[] entries = nums[1].Split(' ');
                List<int> solutionsI = new List<int>();
                for (int k = 0; k < solutions.Length; k++)
                {
                    if (solutions[k] == " " || solutions[k] == "")
                    {
                        continue;
                    }
                    solutionsI.Add(Int32.Parse(solutions[k]));
                }
                for (int k = 0; k < entries.Length; k++)
                {
                    if (entries[k] == " " || entries[k] == "")
                    {
                        continue;
                    }
                    int entryI = Int32.Parse(entries[k]);
                    if (solutionsI.Contains(entryI) == true)
                    {
                        cardPoints++;
                    }
                }
                if (cardPoints > 0)
                {
                    points += (int)Math.Pow(2, cardPoints - 1);
                }
            }
            return points;
        }

        public static long Part_2(string input)
        {
            string[] cards = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            List<StringBuilder> cardsSB = new List<StringBuilder>();
            int[] points = new int[cards.Length];
            int[] cardAmmount = new int[cards.Length];
            Utils.Populate(cardAmmount, 1);
            int result = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                bool hit = false;
                cardsSB.Add(new StringBuilder());
                for (int j = 0; j < cards[i].Length; j++)
                {
                    if (cards[i][j] == ':')
                    {
                        hit = true;
                        continue;
                    }
                    if (hit == false)
                    {
                        continue;
                    }
                    cardsSB[i].Append(cards[i][j]);
                }
                int cardPoints = 0;
                string[] nums = cardsSB[i].ToString().Split('|');
                string[] solutions = nums[0].Split(' ');
                string[] entries = nums[1].Split(' ');
                List<int> solutionsI = new List<int>();
                for (int k = 0; k < solutions.Length; k++)
                {
                    if (solutions[k] == " " || solutions[k] == "")
                    {
                        continue;
                    }
                    solutionsI.Add(Int32.Parse(solutions[k]));
                }
                for (int k = 0; k < entries.Length; k++)
                {
                    if (entries[k] == " " || entries[k] == "")
                    {
                        continue;
                    }
                    int entryI = Int32.Parse(entries[k]);
                    if (solutionsI.Contains(entryI) == true)
                    {
                        cardPoints++;
                    }
                }
                points[i] = cardPoints;
            }
            for (int i = 0; i < points.Length; i++)
            {
                for (int k = 0; k < cardAmmount[i]; k++)
                {
                    for (int j = i + 1; j <= i + points[i]; j++)
                    {
                        if (j >= cardAmmount.Length)
                        {
                            continue;
                        }
                        cardAmmount[j]++;
                    }
                }

            }
            for (int i = 0; i < cardAmmount.Length; i++)
            {
                result += cardAmmount[i];
            }
            return result;
        }
    }
}
