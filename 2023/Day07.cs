using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day07 : IDay
    {
        public static long Part_1(string input)
        {
            string[] handValStr = input.Split('\n');
            HandValPair[] handValPairs = new HandValPair[handValStr.Length];
            string[] currenthHandVal;
            int result = 0;
            for (int i = 0; i < handValStr.Length; i++)
            {
                currenthHandVal = handValStr[i].Split(" ");
                handValPairs[i] = new HandValPair(currenthHandVal[0], Int32.Parse(currenthHandVal[1]), true);
            }
            Utils.QuickSort(handValPairs);
            for (int i = 0; i < handValPairs.Length; i++)
            {
                result += handValPairs[i].value * (i + 1);
            }
            return result;
        }

        public static long Part_2(string input)
        {
            string[] handValStr = input.Split('\n');
            HandValPair[] handValPairs = new HandValPair[handValStr.Length];
            string[] currenthHandVal;
            int result = 0;
            for (int i = 0; i < handValStr.Length; i++)
            {
                currenthHandVal = handValStr[i].Split(" ");
                handValPairs[i] = new HandValPair(currenthHandVal[0], Int32.Parse(currenthHandVal[1]), false);
            }
            Utils.QuickSort(handValPairs);
            for (int i = 0; i < handValPairs.Length; i++)
            {
                result += handValPairs[i].value * (i + 1);
            }
            return result;
        }

        private class HandValPair : IComparable<HandValPair>
        {
            public HandValPair(string hand, int value, bool first)
            {
                this.hand = hand;
                this.value = value;
                if (first == true)
                {
                    cardVal = cardVal1;
                }
                else
                {
                    cardVal = cardVal2;
                }
                CalcRank(first);
            }
            public string hand;
            public int value;
            public int Rank { get { return _rank; } }

            private int _rank;
            private Dictionary<char, int> cardVal = cardVal1;
            private static Dictionary<char, int> cardVal1 = new Dictionary<char, int>
        {
            {'2', 0},
            {'3', 1},
            {'4', 2},
            {'5', 3},
            {'6', 4},
            {'7', 5},
            {'8', 6},
            {'9', 7},
            {'T', 8},
            {'J', 9},
            {'Q', 10},
            {'K', 11},
            {'A', 12}
        };
            private static Dictionary<char, int> cardVal2 = new Dictionary<char, int>
        {
            {'J', 0},
            {'2', 1},
            {'3', 2},
            {'4', 3},
            {'5', 4},
            {'6', 5},
            {'7', 6},
            {'8', 7},
            {'9', 8},
            {'T', 9},
            {'Q', 10},
            {'K', 11},
            {'A', 12}
        };

            private void CalcRank(bool first)
            {
                int[] cardCounts = new int[13];
                List<int> dupeCounts = new List<int>();
                for (int i = 0; i < hand.Length; i++)
                {
                    switch (hand[i])
                    {
                        case '2':
                            cardCounts[0]++;
                            break;
                        case '3':
                            cardCounts[1]++;
                            break;
                        case '4':
                            cardCounts[2]++;
                            break;
                        case '5':
                            cardCounts[3]++;
                            break;
                        case '6':
                            cardCounts[4]++;
                            break;
                        case '7':
                            cardCounts[5]++;
                            break;
                        case '8':
                            cardCounts[6]++;
                            break;
                        case '9':
                            cardCounts[7]++;
                            break;
                        case 'T':
                            cardCounts[8]++;
                            break;
                        case 'J':
                            cardCounts[9]++;
                            break;
                        case 'Q':
                            cardCounts[10]++;
                            break;
                        case 'K':
                            cardCounts[11]++;
                            break;
                        case 'A':
                            cardCounts[12]++;
                            break;
                    }
                }
                if (first || cardCounts[9] == 0)
                {
                    for (int i = 0; i < cardCounts.Length; i++)
                    {
                        if (cardCounts[i] < 2)
                        {
                            continue;
                        }
                        dupeCounts.Add(cardCounts[i]);
                    }

                    switch (dupeCounts.Count)
                    {
                        case 0:
                            _rank = 0;
                            return;
                        case 1:
                            switch (dupeCounts[0])
                            {
                                case 2:
                                    _rank = 1;
                                    return;
                                case 3:
                                    _rank = 3;
                                    return;
                                case 4:
                                    _rank = 5;
                                    return; ;
                                case 5:
                                    _rank = 6;
                                    return;
                            }
                            break;
                        case 2:
                            if (dupeCounts[0] == dupeCounts[1])
                            {
                                _rank = 2;
                                return;
                            }
                            else
                            {
                                _rank = 4;
                                return;
                            }
                    }
                }

                for (int i = 0; i < cardCounts.Length; i++)
                {
                    if (cardCounts[i] < 2)
                    {
                        continue;
                    }
                    if (i == 9)
                    {
                        continue;
                    }
                    dupeCounts.Add(cardCounts[i]);
                }

                switch (dupeCounts.Count)
                {
                    case 0:
                        switch (cardCounts[9])
                        {
                            case 1:
                                _rank = 1;
                                return;
                            case 2:
                                _rank = 3;
                                return;
                            case 3:
                                _rank = 5;
                                return;
                            case 4:
                                _rank = 6;
                                return;
                            case 5:
                                _rank = 6;
                                return;
                        }
                        return;
                    case 1:
                        switch (dupeCounts[0] + cardCounts[9])
                        {
                            case 2:
                                _rank = 1;
                                return;
                            case 3:
                                _rank = 3;
                                return;
                            case 4:
                                _rank = 5;
                                return; ;
                            case 5:
                                _rank = 6;
                                return;
                        }
                        break;
                    case 2:
                        _rank = 4;
                        return;
                }


                throw new ArgumentException("Did not Assign Rank");
            }

            private static Relation CompareCards(char c1, char c2, Dictionary<char, int> dic)
            {
                int c1val = dic[c1];
                int c2val = dic[c2];
                if (c1val < c2val)
                {
                    return Relation.SmallerThan;
                }
                if (c1val > c2val)
                {
                    return Relation.BiggerThan;
                }
                return Relation.Equal;
            }

            public int CompareTo(HandValPair? other)
            {
                if (other == null)
                {
                    throw new ArgumentException("Object is not a HandValuePair");
                }
                if (this.Rank < other.Rank)
                {
                    return -1;
                }
                if (this.Rank > other.Rank)
                {
                    return 1;
                }
                for (int i = 0; i < this.hand.Length; i++)
                {
                    Relation relation = HandValPair.CompareCards(this.hand[i], other.hand[i], this.cardVal);
                    switch (relation)
                    {
                        case Relation.Equal:
                            continue;
                        case Relation.SmallerThan:
                            return -1;
                        case Relation.BiggerThan:
                            return 1;
                    }
                }
                throw new ArgumentException("hands are equal");
            }
        }
    }
}
