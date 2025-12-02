using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day05 : IDay
    {
        public static long Part_1(string input)
        {
            long result = long.MaxValue;
            string[] categories = input.Split("\n\n");
            long[] seeds = Utils.StringColonNumsToLongArr(categories[0]);
            List<string[]> mapsStr = new List<string[]>();

            List<long[]> seedsToSoilMap = new List<long[]>();
            List<long[]> soilToFertilizerMap = new List<long[]>();
            List<long[]> fertelizerToWaterMap = new List<long[]>();
            List<long[]> waterToLightMap = new List<long[]>();
            List<long[]> lightToTemperatureMap = new List<long[]>();
            List<long[]> temperatureToHumidityMap = new List<long[]>();
            List<long[]> humidityToLocationMap = new List<long[]>();
            List<long[]>[] mapsI = { seedsToSoilMap, soilToFertilizerMap, fertelizerToWaterMap, waterToLightMap, lightToTemperatureMap, temperatureToHumidityMap, humidityToLocationMap };
            List<KeyValuePair<long, long>[]> seedsToSoil = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> soilToFertilizer = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> fertelizerToWater = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> waterToLight = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> lightToTemperature = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> temperatureToHumidity = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> humidityToLocation = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]>[] keyValuePairs = { seedsToSoil, soilToFertilizer, fertelizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation };
            for (int i = 1; i < categories.Length; i++)
            {
                mapsStr.Add(categories[i].Split('\n'));
            }
            for (int i = 0; i < mapsStr.Count; i++)
            {
                for (int j = 1; j < mapsStr[i].Length; j++)
                {
                    mapsI[i].Add(Utils.NumStringToLongArr(mapsStr[i][j]));
                }
            }
            for (int i = 0; i < mapsI.Length; i++)
            {
                for (int j = 0; j < mapsI[i].Count; j++)
                {
                    keyValuePairs[i].Add([new KeyValuePair<long, long>(mapsI[i][j][1], mapsI[i][j][0]), new KeyValuePair<long, long>(mapsI[i][j][1] + mapsI[i][j][2] - 1, mapsI[i][j][0] + mapsI[i][j][2] - 1)]);
                }
            }
            for (long i = 0; i < seeds.Length; i++)
            {
                long tempResult = GetLandFromSeed(0, keyValuePairs, seeds[i]);
                if (tempResult < result)
                {
                    result = tempResult;
                }
            }
            return result;
        }

        public static long Part_2(string input)
        {
            long result = long.MaxValue;
            string[] categories = input.Split("\n\n");
            long[] seeds = Utils.StringColonNumsToLongArr(categories[0]);
            List<string[]> mapsStr = new List<string[]>();

            List<long[]> seedsToSoilMap = new List<long[]>();
            List<long[]> soilToFertilizerMap = new List<long[]>();
            List<long[]> fertelizerToWaterMap = new List<long[]>();
            List<long[]> waterToLightMap = new List<long[]>();
            List<long[]> lightToTemperatureMap = new List<long[]>();
            List<long[]> temperatureToHumidityMap = new List<long[]>();
            List<long[]> humidityToLocationMap = new List<long[]>();
            List<long[]>[] mapsI = { seedsToSoilMap, soilToFertilizerMap, fertelizerToWaterMap, waterToLightMap, lightToTemperatureMap, temperatureToHumidityMap, humidityToLocationMap };
            List<KeyValuePair<long, long>[]> seedsToSoil = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> soilToFertilizer = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> fertelizerToWater = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> waterToLight = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> lightToTemperature = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> temperatureToHumidity = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]> humidityToLocation = new List<KeyValuePair<long, long>[]>();
            List<KeyValuePair<long, long>[]>[] keyValuePairs = { seedsToSoil, soilToFertilizer, fertelizerToWater, waterToLight, lightToTemperature, temperatureToHumidity, humidityToLocation };

            for (int i = 1; i < categories.Length; i++)
            {
                mapsStr.Add(categories[i].Split('\n'));
            }
            for (int i = 0; i < mapsStr.Count; i++)
            {
                for (int j = 1; j < mapsStr[i].Length; j++)
                {
                    mapsI[i].Add(Utils.NumStringToLongArr(mapsStr[i][j]));
                }
            }
            for (int i = 0; i < mapsI.Length; i++)
            {
                for (int j = 0; j < mapsI[i].Count; j++)
                {
                    keyValuePairs[i].Add([new KeyValuePair<long, long>(mapsI[i][j][1], mapsI[i][j][0]), new KeyValuePair<long, long>(mapsI[i][j][1] + mapsI[i][j][2] - 1, mapsI[i][j][0] + mapsI[i][j][2] - 1)]);
                }
            }
            for (int i = 0; i < seeds.Length - 1; i += 2)
            {
                for (long j = 0; j < seeds[i + 1]; j++)
                {
                    long tempResult = GetLandFromSeed(0, keyValuePairs, seeds[i] + j);
                    if (tempResult < result)
                    {
                        result = tempResult;
                    }
                }
            }
            return result;
        }

        private static long GetLandFromSeed(long step, List<KeyValuePair<long, long>[]>[] keyValuePairs, long key)
        {
            long value = -1;
            bool mappedVal = false;
            for (int i = 0; i < keyValuePairs[step].Count; i++)
            {
                if (key >= keyValuePairs[step][i][0].Key && key <= keyValuePairs[step][i][1].Key)
                {
                    value = keyValuePairs[step][i][0].Value + (key - keyValuePairs[step][i][0].Key);
                    mappedVal = true;
                    break;
                }
            }
            if (mappedVal == false)
            {
                value = key;
            }
            if (value < 0)
            {
                Console.WriteLine("Error");
            }
            step++;
            if (step == keyValuePairs.Length)
            {
                return value;
            }
            else
            {
                return GetLandFromSeed(step, keyValuePairs, value);
            }
        }
    }
}
