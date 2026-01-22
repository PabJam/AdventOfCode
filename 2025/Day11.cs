using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2025
{
    public class Day11 : IDay
    {
        private class Node
        {
            public string id;
            public Node? parent;
            public List<string> connections;

            public Node(string id, Node? parent, List<string> connections)
            {
                this.id = id;
                this.parent = parent;
                this.connections = connections;
            }

            public bool CheckParents(string idOther)
            {
                if (id == idOther) { return true; }
                if (parent == null) {  return false; }
                return parent.CheckParents(idOther);
            }

            public override bool Equals(object? obj)
            {
                if (obj is not Node) { return false; }
                Node? other = obj as Node;
                if ( other == null) { return false; }
                if (other.id != id) { return false; }
                if (other.parent != parent) { return false; }
                if (parent == null && other.parent == null) { return true; }
                return (parent!.Equals(other.parent));
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(id, connections);
            }
        }

        public static long Part_1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            for (int i = 0; i < lines.Length; i++)
            {
                string point = lines[i].Substring(0, lines[i].IndexOf(':'));
                List<string> connections = lines[i].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
                connections.RemoveAt(0);
                nodes.Add(point, new Node(point, null, connections));
            }

            return Explore(nodes["you"], "out", nodes);
            
        }

        private static long Explore(Node start, string target, Dictionary<string, Node> nodes)
        {
            if (start.connections.Contains(target)) { return 1; }
            long count = 0;
            for (int i = 0; i < start.connections.Count; i++)
            {
                string nextId = start.connections[i];
                if (nextId == "out") { continue; }
                Node next = new Node(nextId, start, nodes[nextId].connections);
                count += Explore(next, target, nodes);
            }
            return count;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();
            for (int i = 0; i < lines.Length; i++)
            {
                string point = lines[i].Substring(0, lines[i].IndexOf(':'));
                List<string> connections = lines[i].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();
                connections.RemoveAt(0);
                nodes.Add(point, new Node(point, null, connections));
            }

            Console.WriteLine("FFT-OUT: " +  Explore(nodes["fft"], "out", nodes));
            Console.WriteLine("DAC-OUT: " + Explore(nodes["dac"], "out", nodes));
            Console.WriteLine("FFT-DAC: " + Explore(nodes["fft"], "dac", nodes));
            Console.WriteLine("DAC-FFT: " + Explore(nodes["dac"], "fft", nodes));
            Console.WriteLine("SVR-FFT: " + Explore(nodes["svr"], "fft", nodes));
            Console.WriteLine("SVR-DAC: " + Explore(nodes["svr"], "dac", nodes));

            return 0;
        }

        private static long ExploreWithFftAndDac(Node start, Dictionary<string, Node> nodes)
        {
            if (start.connections.Contains("out")) 
            {
                if (start.CheckParents("fft") == true && start.CheckParents("dac") == true)
                {
                    return 1;
                }
                return 0;
            }
            long count = 0;
            for (int i = 0; i < start.connections.Count; i++)
            {
                string nextId = start.connections[i];
                Node next = new Node(nextId, start, nodes[nextId].connections);
                count += ExploreWithFftAndDac(next, nodes);
            }
            return count;
        }
    }
}
