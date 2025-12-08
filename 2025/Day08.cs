using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2025
{
    public class Day08 : IDay
    {
        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<Vector3Int> positions = new List<Vector3Int>();
            foreach (string line in lines)
            {
                string[] coords = line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                int x = int.Parse(coords[0]);
                int y = int.Parse(coords[1]);
                int z = int.Parse(coords[2]);
                positions.Add(new Vector3Int(x, y, z));
            }
            List<List<int>> Circuits = new List<List<int>>();
            List<int>?[] MapPositionIdxToCircuits = new List<int>?[positions.Count]; 
            for (int i = 0; i < MapPositionIdxToCircuits.Length; i++)
            {
                MapPositionIdxToCircuits[i] = null;
            }
            SortedDictionary<double, (int, int)> distances = new SortedDictionary<double, (int, int)>();
            for (int i = 0; i < positions.Count - 1; i++)
            {
                for (int j = i + 1; j < positions.Count; j++)
                {
                    double distance = Vector3Int.Distance(positions[i], positions[j]);
                    distances.Add(distance, (i, j));
                }
            }

            int counter = 0;
            foreach (var kvp in distances)
            {
                if (counter == 1000) { break; }
                if (MapPositionIdxToCircuits[kvp.Value.Item1] == null && MapPositionIdxToCircuits[kvp.Value.Item2] == null)
                {
                    Circuits.Add(new List<int>() { kvp.Value.Item1, kvp.Value.Item2 });
                    MapPositionIdxToCircuits[kvp.Value.Item1] = Circuits[Circuits.Count - 1];
                    MapPositionIdxToCircuits[kvp.Value.Item2] = Circuits[Circuits.Count - 1];
                }
                else if (MapPositionIdxToCircuits[kvp.Value.Item1] != null && MapPositionIdxToCircuits[kvp.Value.Item2] == null)
                {
                    MapPositionIdxToCircuits[kvp.Value.Item1]!.Add(kvp.Value.Item2);
                    MapPositionIdxToCircuits[kvp.Value.Item2] = MapPositionIdxToCircuits[kvp.Value.Item1];
                }
                else if (MapPositionIdxToCircuits[kvp.Value.Item1] == null && MapPositionIdxToCircuits[kvp.Value.Item2] != null)
                {
                    MapPositionIdxToCircuits[kvp.Value.Item2]!.Add(kvp.Value.Item1);
                    MapPositionIdxToCircuits[kvp.Value.Item1] = MapPositionIdxToCircuits[kvp.Value.Item2];
                }
                else
                {
                    if (MapPositionIdxToCircuits[kvp.Value.Item1] == MapPositionIdxToCircuits[kvp.Value.Item2]) { counter++; continue; }
                    MapPositionIdxToCircuits[kvp.Value.Item1]!.AddRange(MapPositionIdxToCircuits[kvp.Value.Item2]!);
                    List<int> oldCircuit = MapPositionIdxToCircuits[kvp.Value.Item2]!;
                    for (int i = 0; i < oldCircuit.Count; i++)
                    {
                        MapPositionIdxToCircuits[oldCircuit[i]] = MapPositionIdxToCircuits[kvp.Value.Item1];
                    }
                    Circuits.Remove(oldCircuit);
                }
                counter++;
            }
            long[] top3 = new long[3] { 1,1,1};
            foreach (List<int> ciurcit in Circuits)
            {
                if (ciurcit.Count > top3[0])
                {
                    top3[2] = top3[1];
                    top3[1] = top3[0];
                    top3[0] = ciurcit.Count;
                }
                else if (ciurcit.Count > top3[1])
                {
                    top3[2] = top3[1];
                    top3[1] = ciurcit.Count;
                }
                else if (ciurcit.Count > top3[2])
                {
                    top3[2] = ciurcit.Count;
                }
            }

            return top3[0] * top3[1] * top3[2];
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<Vector3Int> positions = new List<Vector3Int>();
            foreach (string line in lines)
            {
                string[] coords = line.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                int x = int.Parse(coords[0]);
                int y = int.Parse(coords[1]);
                int z = int.Parse(coords[2]);
                positions.Add(new Vector3Int(x, y, z));
            }
            List<List<int>> Circuits = new List<List<int>>();
            List<int>?[] MapPositionIdxToCircuits = new List<int>?[positions.Count];
            for (int i = 0; i < MapPositionIdxToCircuits.Length; i++)
            {
                MapPositionIdxToCircuits[i] = null;
            }
            SortedDictionary<double, (int, int)> distances = new SortedDictionary<double, (int, int)>();
            for (int i = 0; i < positions.Count - 1; i++)
            {
                for (int j = i + 1; j < positions.Count; j++)
                {
                    double distance = Vector3Int.Distance(positions[i], positions[j]);
                    distances.Add(distance, (i, j));
                }
            }

            foreach (var kvp in distances)
            {
                if (MapPositionIdxToCircuits[kvp.Value.Item1] == null && MapPositionIdxToCircuits[kvp.Value.Item2] == null)
                {
                    Circuits.Add(new List<int>() { kvp.Value.Item1, kvp.Value.Item2 });
                    MapPositionIdxToCircuits[kvp.Value.Item1] = Circuits[Circuits.Count - 1];
                    MapPositionIdxToCircuits[kvp.Value.Item2] = Circuits[Circuits.Count - 1];
                }
                else if (MapPositionIdxToCircuits[kvp.Value.Item1] != null && MapPositionIdxToCircuits[kvp.Value.Item2] == null)
                {
                    MapPositionIdxToCircuits[kvp.Value.Item1]!.Add(kvp.Value.Item2);
                    MapPositionIdxToCircuits[kvp.Value.Item2] = MapPositionIdxToCircuits[kvp.Value.Item1];
                }
                else if (MapPositionIdxToCircuits[kvp.Value.Item1] == null && MapPositionIdxToCircuits[kvp.Value.Item2] != null)
                {
                    MapPositionIdxToCircuits[kvp.Value.Item2]!.Add(kvp.Value.Item1);
                    MapPositionIdxToCircuits[kvp.Value.Item1] = MapPositionIdxToCircuits[kvp.Value.Item2];
                }
                else
                {
                    if (MapPositionIdxToCircuits[kvp.Value.Item1] == MapPositionIdxToCircuits[kvp.Value.Item2]) { continue; }
                    MapPositionIdxToCircuits[kvp.Value.Item1]!.AddRange(MapPositionIdxToCircuits[kvp.Value.Item2]!);
                    List<int> oldCircuit = MapPositionIdxToCircuits[kvp.Value.Item2]!;
                    for (int i = 0; i < oldCircuit.Count; i++)
                    {
                        MapPositionIdxToCircuits[oldCircuit[i]] = MapPositionIdxToCircuits[kvp.Value.Item1];
                    }
                    Circuits.Remove(oldCircuit);
                }
                if (Circuits.Count == 1)
                {
                    bool allConnedcted = true;
                    for (int i = 0; i < MapPositionIdxToCircuits.Length; i++)
                    {
                        if (MapPositionIdxToCircuits[i] == null)
                        {
                            allConnedcted = false;
                            break;
                        }
                    }
                    if (allConnedcted)
                    {
                        return positions[kvp.Value.Item1].x * positions[kvp.Value.Item2].x;
                    }
                }
            }
            return -1;
        }
    }
}
