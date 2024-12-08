using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public static class Day08
    {
        public const string test_input_1 = "............\r\n........0...\r\n.....0......\r\n.......0....\r\n....0.......\r\n......A.....\r\n............\r\n............\r\n........A...\r\n.........A..\r\n............\r\n............";
        public const string input_1 = "...............3................d.................\r\n.........................s..7......i.....e........\r\n................C.......................e.........\r\n...............Z.......m....................e.....\r\n....................gC.....q......................\r\n...............Q....s..........................A..\r\n................................s........A........\r\n...........n.....3.C..F......w..m...d.............\r\n..E...............3.....m......d.i................\r\n............f.3.......C....d........A.............\r\n.........Z...........................n..A.........\r\n....Q......p..............g.i.....................\r\n.r......n...Q....p............S.7...........O.....\r\n..........r......K....p.....M..........7....G.....\r\n....................Fs...................G........\r\n..z.........D..........G.g........................\r\nrR.............F................M...............G.\r\n.........I..c.nr...............M................O.\r\n...I..............................................\r\n...................f......I.......................\r\nz.I...............f..K..........0................7\r\nk...................K......u.........O............\r\n.........Q...z.................ga......0.......o..\r\n....E.5..F..................u..b.P......a.1.......\r\n..........k9..................K.........H......1..\r\n.E.........h..........................0......a...H\r\n..........9...h..e........i......M....1...........\r\n.c.............z.......................j.........T\r\nc..D......................Pb.................2....\r\n....................w.y......W......j.........T.2.\r\n......ph...N..................y.......W.t.2.......\r\n............9.................................o..1\r\n.................Vq.......u....Pb.................\r\n.......6R.........................................\r\n........5............w...a.W.............H.j......\r\n......Z.......Y..........V............H..2........\r\n..........D.................v..y.........t...T..o.\r\n.......5...................t......................\r\n........8k...l...............v.........S....T...4.\r\n......6....U......PR........b.B....y..............\r\n..........6.V...U........................L........\r\n.......8.........N....4.Vq.v..t......oJ.....L.....\r\nN...........R.................w.JY................\r\n............N.....................................\r\n.....5Y.....................................j.....\r\n.98........Y.....l.............B...........S...L..\r\n.8...............U...............4................\r\n..................W.........U4....................\r\n...E........l..........B......................L..u\r\n.....D............l....J..q.....................S.";
        public const string test_input_2 = test_input_1;
        public const string input_2 = input_1;

        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                    char character = grid[x][_y];
                    if (character == '.') { continue; }
                    if (antennas.ContainsKey(character) == false)
                    {
                        antennas.Add(character, new List<(int, int)>());
                    }
                    antennas[character].Add((x, _y));
                }
            }
            HashSet<(int, int)> signals = new HashSet<(int, int)>();
            foreach(KeyValuePair<char,List<(int, int)>> kvp in antennas)
            {
                signals.UnionWith(GetSignalsFromAntennas(kvp.Value, grid.Length, grid[0].Length));
            }

            return signals.Count;
        }

        public static long Part_2(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            char[][] grid = new char[lineStr[0].Length][];
            Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int, int)>>();
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new char[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x];
                    char character = grid[x][_y];
                    if (character == '.') { continue; }
                    if (antennas.ContainsKey(character) == false)
                    {
                        antennas.Add(character, new List<(int, int)>());
                    }
                    antennas[character].Add((x, _y));
                }
            }
            HashSet<(int, int)> signals = new HashSet<(int, int)>();
            foreach (KeyValuePair<char, List<(int, int)>> kvp in antennas)
            {
                signals.UnionWith(GetAllSignalsFromAntennas(kvp.Value, grid.Length, grid[0].Length));
            }

            return signals.Count;
        }

        private static HashSet<(int, int)> GetSignalsFromAntennas(List<(int, int)> antennas, int gridSizeX, int gridSizeY)
        {
            HashSet<(int, int)> signals = new HashSet<(int, int)>();

            for (int i = 0; i < antennas.Count; i++) 
            {
                for (int j = 0; j < antennas.Count; j++)
                {
                    if (i == j) { continue; }
                    (int, int) vec;
                    (int, int) pos;
                    vec.Item1 = 2 * (antennas[j].Item1 - antennas[i].Item1);
                    vec.Item2 = 2* (antennas[j].Item2 - antennas[i].Item2);
                    pos.Item1 = antennas[i].Item1 + vec.Item1;
                    pos.Item2 = antennas[i].Item2 + vec.Item2;
                    if (pos.Item1 < 0 || pos.Item1 >= gridSizeX) { continue; }
                    if (pos.Item2 < 0 || pos.Item2 >= gridSizeY) { continue; }
                    signals.Add(pos);
                }
            }

            return signals;
        }

        private static HashSet<(int, int)> GetAllSignalsFromAntennas(List<(int, int)> antennas, int gridSizeX, int gridSizeY)
        {
            HashSet<(int, int)> signals = new HashSet<(int, int)>();

            for (int i = 0; i < antennas.Count; i++)
            {
                for (int j = 0; j < antennas.Count; j++)
                {
                    if (i == j) { continue; }
                    (int, int) vec;
                    (int, int) pos;
                    int counter = 0;
                    while(true)
                    {
                        counter++;
                        vec.Item1 = counter * (antennas[j].Item1 - antennas[i].Item1);
                        vec.Item2 = counter * (antennas[j].Item2 - antennas[i].Item2);
                        pos.Item1 = antennas[i].Item1 + vec.Item1;
                        pos.Item2 = antennas[i].Item2 + vec.Item2;
                        if (pos.Item1 < 0 || pos.Item1 >= gridSizeX) { break; }
                        if (pos.Item2 < 0 || pos.Item2 >= gridSizeY) { break; }
                        signals.Add(pos);
                    }
                    
                }
            }

            return signals;
        }
    }
}
