using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day06 : IDay
    {
        public static long Part_1(string input)
        {
            string[] timeDist = input.Split('\n');
            int[] times = Utils.StringColonNumsToIntArr(timeDist[0]);
            int[] distances = Utils.StringColonNumsToIntArr(timeDist[1]);
            int result = 1;
            int low, high;
            double sqrt;
            for (int i = 0; i < times.Length; i++)
            {
                sqrt = Math.Sqrt(times[i] * times[i] - 4 * distances[i]);
                low = (int)(0.5f * (times[i] - sqrt)) + 1;
                high = (int)(0.5f * (times[i] + sqrt));
                result *= high - low + 1;
            }
            return result;
        }

        public static long Part_2(string input)
        {
            string[] timeDist = input.Split('\n');
            int[] times = Utils.StringColonNumsToIntArr(timeDist[0]);
            int[] distances = Utils.StringColonNumsToIntArr(timeDist[1]);
            StringBuilder timeStr = new StringBuilder();
            StringBuilder distString = new StringBuilder();
            for (int i = 0; i < times.Length; i++)
            {
                timeStr.Append(times[i].ToString());
                distString.Append(distances[i].ToString());
            }
            long time = Int64.Parse(timeStr.ToString());
            long distance = Int64.Parse(distString.ToString());
            double sqrt = Math.Sqrt(time * time - 4 * distance);
            int low = (int)(0.5f * (time - sqrt)) + 1;
            int high = (int)(0.5f * (time + sqrt));
            return high - low + 1;
        }
    }
}
