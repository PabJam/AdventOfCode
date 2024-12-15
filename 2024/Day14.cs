using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public static class Day14
    {
        public const string test_input_1 = "p=0,4 v=3,-3\r\np=6,3 v=-1,-3\r\np=10,3 v=-1,2\r\np=2,0 v=2,-1\r\np=0,0 v=1,3\r\np=3,0 v=-2,-2\r\np=7,6 v=-1,-3\r\np=3,0 v=-1,-2\r\np=9,3 v=2,3\r\np=7,3 v=-1,2\r\np=2,4 v=2,-3\r\np=9,5 v=-3,-3"; 
        public const string input_1 = "p=1,64 v=-25,73\r\np=35,82 v=18,61\r\np=39,59 v=-79,71\r\np=85,27 v=-78,-84\r\np=67,19 v=65,-3\r\np=44,40 v=-82,14\r\np=82,14 v=-12,-15\r\np=45,1 v=-68,78\r\np=61,91 v=-27,35\r\np=53,40 v=14,-91\r\np=46,63 v=74,-52\r\np=42,39 v=23,20\r\np=33,58 v=-84,88\r\np=5,80 v=31,65\r\np=69,52 v=38,-4\r\np=94,58 v=85,-20\r\np=40,76 v=-14,39\r\np=28,68 v=31,-39\r\np=59,78 v=-23,37\r\np=36,65 v=9,91\r\np=61,19 v=24,-29\r\np=96,21 v=39,-41\r\np=59,66 v=42,67\r\np=61,29 v=-74,-78\r\np=5,83 v=-11,-70\r\np=6,84 v=54,-54\r\np=40,23 v=-90,-14\r\np=39,38 v=56,48\r\np=34,49 v=3,-30\r\np=43,31 v=-60,-47\r\np=91,73 v=-43,2\r\np=27,10 v=32,54\r\np=13,63 v=63,-18\r\np=77,59 v=34,-41\r\np=87,87 v=-35,-58\r\np=87,54 v=-26,30\r\np=79,89 v=-17,-40\r\np=32,79 v=63,-48\r\np=95,23 v=-8,11\r\np=75,16 v=-49,-15\r\np=30,35 v=-65,-87\r\np=9,47 v=-86,-88\r\np=95,97 v=-44,-61\r\np=50,2 v=-28,-3\r\np=27,42 v=-4,-43\r\np=87,44 v=-62,-69\r\np=97,98 v=-94,17\r\np=81,29 v=2,60\r\np=80,79 v=56,-62\r\np=37,51 v=73,20\r\np=61,64 v=-77,10\r\np=63,1 v=-49,3\r\np=70,38 v=93,-61\r\np=97,87 v=-94,51\r\np=50,70 v=17,-15\r\np=59,63 v=-82,89\r\np=29,46 v=13,-85\r\np=25,62 v=-14,-12\r\np=25,0 v=9,-69\r\np=83,45 v=-45,-1\r\np=31,26 v=73,-39\r\np=6,36 v=-30,-77\r\np=79,1 v=-17,3\r\np=21,42 v=-57,8\r\np=38,84 v=13,54\r\np=34,2 v=-9,3\r\np=57,76 v=97,-36\r\np=60,43 v=-13,-89\r\np=13,44 v=-54,42\r\np=99,42 v=57,-27\r\np=69,6 v=-45,62\r\np=0,101 v=-23,92\r\np=80,76 v=-49,67\r\np=96,53 v=-48,-2\r\np=50,80 v=31,2\r\np=53,51 v=-23,18\r\np=10,26 v=-89,98\r\np=14,4 v=8,27\r\np=62,9 v=-34,-91\r\np=34,63 v=82,51\r\np=51,50 v=-98,-42\r\np=92,56 v=-1,-76\r\np=54,28 v=-59,-45\r\np=76,51 v=4,-45\r\np=17,34 v=-93,-83\r\np=75,60 v=-72,57\r\np=24,49 v=22,-95\r\np=59,97 v=-73,-88\r\np=91,68 v=-53,-46\r\np=53,19 v=-55,-21\r\np=70,5 v=-27,25\r\np=50,37 v=55,48\r\np=56,14 v=88,90\r\np=81,30 v=-39,-39\r\np=32,60 v=-47,-85\r\np=80,78 v=-38,-1\r\np=57,32 v=88,-55\r\np=39,70 v=-96,-60\r\np=58,33 v=19,22\r\np=48,97 v=37,-56\r\np=85,76 v=66,89\r\np=24,5 v=81,-82\r\np=42,85 v=74,72\r\np=40,69 v=-60,77\r\np=43,20 v=35,-78\r\np=96,29 v=58,53\r\np=53,14 v=-18,-3\r\np=33,12 v=86,-15\r\np=71,57 v=68,-32\r\np=4,86 v=-93,-94\r\np=8,26 v=-98,60\r\np=2,50 v=85,-81\r\np=31,77 v=96,57\r\np=13,16 v=55,19\r\np=81,30 v=-86,62\r\np=60,99 v=24,43\r\np=88,57 v=-90,-14\r\np=12,25 v=-66,66\r\np=79,78 v=-90,-44\r\np=54,3 v=-36,-60\r\np=43,28 v=55,-75\r\np=70,73 v=11,-54\r\np=11,97 v=-98,-3\r\np=70,60 v=-72,99\r\np=17,2 v=3,3\r\np=83,32 v=52,-45\r\np=50,69 v=69,-28\r\np=54,10 v=50,5\r\np=74,6 v=-36,62\r\np=94,82 v=7,-54\r\np=37,101 v=74,-45\r\np=69,69 v=-73,-64\r\np=52,4 v=-18,84\r\np=7,51 v=-20,18\r\np=91,4 v=11,5\r\np=35,73 v=59,-88\r\np=44,91 v=32,41\r\np=41,61 v=33,-27\r\np=67,28 v=-22,-65\r\np=79,65 v=75,-36\r\np=53,59 v=69,-12\r\np=2,13 v=81,10\r\np=44,42 v=23,-89\r\np=86,49 v=-21,-4\r\np=60,27 v=33,-45\r\np=8,46 v=-89,-93\r\np=76,71 v=-95,-50\r\np=89,1 v=-99,-70\r\np=68,38 v=-36,-2\r\np=22,47 v=-6,26\r\np=2,81 v=69,-88\r\np=90,6 v=30,5\r\np=84,52 v=-71,-79\r\np=52,15 v=-64,-59\r\np=96,89 v=-8,-39\r\np=6,67 v=-2,-14\r\np=90,75 v=-71,-12\r\np=93,72 v=52,59\r\np=52,89 v=-83,41\r\np=25,95 v=-29,90\r\np=5,49 v=3,22\r\np=91,98 v=6,86\r\np=32,86 v=64,-50\r\np=67,91 v=48,67\r\np=52,78 v=1,-70\r\np=93,43 v=-30,-65\r\np=18,3 v=-2,-79\r\np=58,30 v=51,-55\r\np=70,74 v=-45,-40\r\np=65,88 v=14,45\r\np=15,95 v=31,-90\r\np=13,33 v=54,55\r\np=63,31 v=-32,66\r\np=41,97 v=-5,-82\r\np=50,101 v=-28,84\r\np=81,73 v=80,75\r\np=12,25 v=86,37\r\np=18,29 v=-67,49\r\np=34,30 v=64,50\r\np=20,26 v=27,86\r\np=55,6 v=14,23\r\np=83,89 v=89,51\r\np=12,48 v=-20,6\r\np=62,66 v=-41,64\r\np=12,90 v=-27,-8\r\np=83,83 v=74,81\r\np=70,86 v=47,37\r\np=86,33 v=57,20\r\np=7,20 v=26,80\r\np=61,99 v=-37,72\r\np=34,58 v=-79,-42\r\np=17,10 v=91,54\r\np=7,102 v=-24,92\r\np=40,48 v=-14,-99\r\np=1,66 v=-50,-97\r\np=0,18 v=-57,-35\r\np=99,89 v=25,63\r\np=63,0 v=69,19\r\np=12,8 v=86,-45\r\np=70,60 v=-3,2\r\np=9,43 v=-75,-75\r\np=28,88 v=72,-38\r\np=69,90 v=-91,33\r\np=93,35 v=2,22\r\np=42,63 v=41,79\r\np=68,76 v=83,-50\r\np=58,80 v=-87,-42\r\np=11,19 v=-52,90\r\np=77,3 v=-49,15\r\np=81,94 v=-58,-76\r\np=34,94 v=48,-56\r\np=45,18 v=17,-67\r\np=60,86 v=33,-34\r\np=9,100 v=35,44\r\np=23,70 v=-34,94\r\np=76,24 v=-90,26\r\np=97,64 v=23,40\r\np=71,78 v=79,-93\r\np=11,38 v=-61,30\r\np=41,67 v=-44,-71\r\np=20,96 v=-61,-72\r\np=45,34 v=65,93\r\np=43,16 v=46,82\r\np=78,64 v=-49,-89\r\np=76,31 v=70,50\r\np=64,38 v=92,-47\r\np=88,34 v=99,-38\r\np=88,94 v=-44,17\r\np=75,67 v=-99,4\r\np=45,70 v=55,69\r\np=55,16 v=-9,86\r\np=100,30 v=95,42\r\np=38,41 v=31,-65\r\np=93,40 v=22,80\r\np=28,73 v=3,-99\r\np=35,97 v=-59,35\r\np=67,36 v=-63,44\r\np=9,81 v=90,53\r\np=59,27 v=-73,51\r\np=23,100 v=45,-62\r\np=45,57 v=60,-97\r\np=68,67 v=78,40\r\np=14,88 v=-20,-66\r\np=21,81 v=-47,-58\r\np=30,40 v=-27,78\r\np=29,81 v=-7,-76\r\np=55,71 v=70,57\r\np=87,52 v=15,26\r\np=80,61 v=56,-16\r\np=75,22 v=38,60\r\np=44,29 v=-82,-55\r\np=31,69 v=-61,-46\r\np=52,79 v=83,-25\r\np=30,97 v=96,47\r\np=15,29 v=-34,-87\r\np=83,98 v=6,-58\r\np=61,11 v=28,-51\r\np=82,18 v=20,-25\r\np=68,42 v=-22,-69\r\np=54,47 v=74,20\r\np=79,96 v=-26,-66\r\np=23,4 v=-1,11\r\np=69,37 v=93,16\r\np=45,59 v=42,-14\r\np=46,81 v=-11,-65\r\np=47,2 v=37,-5\r\np=4,19 v=-57,-13\r\np=90,25 v=-76,36\r\np=89,78 v=-12,47\r\np=95,67 v=-39,-32\r\np=32,14 v=-60,94\r\np=14,99 v=-86,64\r\np=61,63 v=33,-18\r\np=53,94 v=41,39\r\np=58,25 v=28,56\r\np=31,10 v=-74,84\r\np=57,74 v=-55,8\r\np=51,3 v=21,-76\r\np=73,80 v=-4,21\r\np=11,62 v=-75,-6\r\np=31,87 v=54,-52\r\np=85,63 v=-99,-20\r\np=43,19 v=23,-51\r\np=14,12 v=40,72\r\np=68,76 v=83,-37\r\np=97,19 v=7,-3\r\np=4,98 v=87,-37\r\np=91,50 v=-92,25\r\np=66,88 v=-42,93\r\np=38,97 v=-18,35\r\np=48,77 v=27,-60\r\np=26,33 v=49,-31\r\np=25,95 v=-88,11\r\np=64,100 v=90,-28\r\np=96,27 v=-99,-37\r\np=11,30 v=67,-51\r\np=59,95 v=52,-46\r\np=60,12 v=10,86\r\np=66,61 v=-31,99\r\np=22,49 v=-31,49\r\np=64,72 v=92,-70\r\np=90,65 v=66,11\r\np=75,5 v=-26,-39\r\np=97,29 v=11,-23\r\np=87,73 v=55,45\r\np=52,58 v=5,79\r\np=54,65 v=74,-18\r\np=1,62 v=-34,-24\r\np=11,22 v=-33,-89\r\np=43,90 v=55,39\r\np=48,40 v=92,1\r\np=52,102 v=-52,70\r\np=81,70 v=50,54\r\np=67,17 v=-63,86\r\np=81,36 v=-39,-37\r\np=89,70 v=73,-63\r\np=28,41 v=-96,-51\r\np=68,44 v=65,28\r\np=31,60 v=-33,-46\r\np=81,1 v=-26,94\r\np=69,51 v=-81,-67\r\np=3,48 v=-81,-22\r\np=94,24 v=76,-41\r\np=3,90 v=-17,-60\r\np=30,1 v=-84,80\r\np=41,60 v=53,78\r\np=82,60 v=-16,81\r\np=10,18 v=72,39\r\np=62,19 v=42,-41\r\np=51,71 v=-22,97\r\np=64,51 v=79,8\r\np=71,21 v=15,62\r\np=74,68 v=-8,83\r\np=89,28 v=25,-39\r\np=24,42 v=-61,-10\r\np=100,19 v=-80,64\r\np=64,13 v=-54,-27\r\np=30,76 v=-24,-40\r\np=29,102 v=50,15\r\np=1,99 v=99,15\r\np=1,1 v=-25,7\r\np=31,0 v=-19,74\r\np=55,77 v=51,-48\r\np=89,76 v=16,-41\r\np=71,77 v=-31,67\r\np=57,102 v=74,23\r\np=58,49 v=2,-16\r\np=41,47 v=83,83\r\np=4,45 v=99,24\r\np=90,42 v=57,-79\r\np=59,45 v=-12,75\r\np=68,13 v=98,-16\r\np=93,54 v=-88,-59\r\np=32,19 v=-19,88\r\np=95,91 v=-51,31\r\np=20,62 v=8,95\r\np=84,15 v=-68,-49\r\np=66,2 v=61,5\r\np=42,57 v=33,27\r\np=31,29 v=50,-39\r\np=6,94 v=-12,-67\r\np=25,20 v=-10,-41\r\np=99,81 v=-2,15\r\np=78,23 v=-71,-79\r\np=30,33 v=-23,-95\r\np=91,88 v=-4,55\r\np=45,33 v=-78,24\r\np=89,92 v=-38,-96\r\np=65,14 v=98,-68\r\np=65,39 v=-4,-63\r\np=92,21 v=-51,74\r\np=6,4 v=-77,82\r\np=23,91 v=77,-62\r\np=40,94 v=-69,-82\r\np=54,95 v=23,-48\r\np=12,44 v=-66,-77\r\np=4,9 v=-76,-1\r\np=91,58 v=-51,-45\r\np=89,75 v=-32,-59\r\np=15,45 v=-71,44\r\np=34,58 v=-49,-24\r\np=76,46 v=65,-69\r\np=97,55 v=-16,95\r\np=20,36 v=-61,46\r\np=49,43 v=-24,-83\r\np=41,72 v=-27,-13\r\np=16,69 v=87,48\r\np=91,49 v=-30,10\r\np=92,19 v=12,-57\r\np=52,89 v=-41,-58\r\np=13,23 v=13,54\r\np=32,34 v=46,-83\r\np=95,54 v=-93,55\r\np=23,48 v=41,24\r\np=74,82 v=4,-62\r\np=63,86 v=88,28\r\np=64,3 v=75,-82\r\np=52,44 v=23,-89\r\np=63,61 v=-36,-34\r\np=22,79 v=22,63\r\np=80,96 v=11,88\r\np=65,99 v=-91,-21\r\np=8,100 v=-52,-78\r\np=67,68 v=24,67\r\np=36,62 v=-74,69\r\np=8,23 v=-84,-75\r\np=27,65 v=-77,30\r\np=21,88 v=-5,45\r\np=87,23 v=11,46\r\np=74,9 v=84,-27\r\np=30,74 v=4,-40\r\np=0,34 v=-75,-33\r\np=22,26 v=-24,87\r\np=7,99 v=-43,-21\r\np=80,97 v=98,-80\r\np=8,14 v=-55,14\r\np=65,79 v=65,-6\r\np=83,27 v=-10,-46\r\np=66,79 v=88,-36\r\np=61,10 v=-6,92\r\np=44,60 v=-37,3\r\np=81,50 v=-32,41\r\np=6,97 v=-11,31\r\np=12,28 v=-43,44\r\np=66,75 v=56,-42\r\np=49,95 v=78,-88\r\np=75,59 v=-45,-83\r\np=4,83 v=-80,27\r\np=13,56 v=-71,17\r\np=0,55 v=17,24\r\np=8,99 v=-47,82\r\np=2,100 v=-25,1\r\np=100,66 v=85,6\r\np=66,92 v=-59,-70\r\np=95,50 v=-25,-93\r\np=28,85 v=-46,-94\r\np=100,63 v=90,79\r\np=55,99 v=-14,-11\r\np=85,62 v=70,-91\r\np=83,79 v=-17,67\r\np=27,75 v=59,-44\r\np=79,11 v=9,88\r\np=86,0 v=93,11\r\np=21,61 v=-79,99\r\np=6,31 v=-30,46\r\np=62,55 v=29,6\r\np=20,40 v=86,34\r\np=19,67 v=40,89\r\np=76,12 v=15,74\r\np=11,19 v=87,-50\r\np=73,43 v=-31,18\r\np=17,76 v=95,49\r\np=100,97 v=-40,-1\r\np=33,69 v=-83,-46\r\np=30,12 v=-74,68\r\np=38,30 v=22,79\r\np=68,16 v=6,88\r\np=27,35 v=-47,38\r\np=62,46 v=-77,-49\r\np=70,25 v=28,38\r\np=4,61 v=-2,-26\r\np=99,2 v=7,-7\r\np=30,0 v=-10,-88\r\np=34,79 v=42,77\r\np=6,72 v=-57,-22\r\np=51,31 v=69,48\r\np=30,88 v=45,89\r\np=96,3 v=-16,-98\r\np=87,60 v=-4,-64\r\np=98,20 v=-94,-45\r\np=73,58 v=66,22\r\np=79,71 v=84,-38\r\np=38,92 v=46,41\r\np=56,2 v=-73,-11\r\np=62,50 v=-59,-6\r\np=99,36 v=-73,34\r\np=78,34 v=-56,-6\r\np=93,78 v=-71,-48\r\np=81,68 v=20,75\r\np=41,78 v=-14,-18\r\np=34,22 v=72,8\r\np=3,80 v=53,98\r\np=17,60 v=-29,93\r\np=96,33 v=3,66\r\np=92,24 v=-99,-39\r\np=7,59 v=32,-18\r\np=10,54 v=-20,-18\r\np=61,19 v=-38,-99\r\np=26,46 v=-92,93\r\np=18,19 v=-93,60\r\np=56,40 v=-82,36\r\np=19,93 v=-79,-72\r\np=63,9 v=88,74\r\np=72,91 v=-38,-12\r\np=38,34 v=-46,-49\r\np=2,98 v=3,84\r\np=74,44 v=56,-14\r\np=9,28 v=75,44\r\np=84,16 v=-4,-48\r\np=17,93 v=-30,-36";
        public const string test_input_2 = test_input_1;
        public const string input_2 = input_1;
        private static readonly (int, int) gridSize = (101, 103);

        public static long Part_1(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            ((int, int), (int, int))[] robots = new ((int, int), (int, int))[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
                robots[i].Item1.Item1 = int.Parse(match.Groups[1].Value);
                robots[i].Item1.Item2 = int.Parse(match.Groups[2].Value);
                robots[i].Item2.Item1 = int.Parse(match.Groups[3].Value);
                robots[i].Item2.Item2 = int.Parse(match.Groups[4].Value);
            }
            long TL = 0, TR = 0, BL = 0, BR = 0;
            for (int i = 0; i < robots.Length; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    robots[i].Item1 = MoveRobot(robots[i].Item1, robots[i].Item2);
                }
                if (robots[i].Item1.Item1 > gridSize.Item1 / 2)
                {
                    if (robots[i].Item1.Item2 > gridSize.Item2 / 2)
                    {
                        BR++;
                    }
                    else if (robots[i].Item1.Item2 < gridSize.Item2 / 2)
                    {
                        TR++;
                    }
                }
                else if (robots[i].Item1.Item1 < gridSize.Item1 / 2)
                {
                    if (robots[i].Item1.Item2 > gridSize.Item2 / 2)
                    {
                        BL++;
                    }
                    else if (robots[i].Item1.Item2 < gridSize.Item2 / 2)
                    {
                        TL++;
                    }
                }
            }


            return BR * TR * BL * TL;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            ((int, int), (int, int))[] robots = new ((int, int), (int, int))[lines.Length];
            char[,] grid = new char[gridSize.Item1, gridSize.Item2];
            List<string> possiblePrints = new List<string> ();
            for (int i = 0; i < lines.Length; i++)
            {
                Match match = Regex.Match(lines[i], @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)");
                robots[i].Item1.Item1 = int.Parse(match.Groups[1].Value);
                robots[i].Item1.Item2 = int.Parse(match.Groups[2].Value);
                robots[i].Item2.Item1 = int.Parse(match.Groups[3].Value);
                robots[i].Item2.Item2 = int.Parse(match.Groups[4].Value);
            }
            while (true)
            {
                
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        grid[x, y] = '.';
                    }
                }
                for (int i = 0; i < robots.Length; i++)
                {
                    grid[robots[i].Item1.Item1, robots[i].Item1.Item2] = '#';
                    robots[i].Item1 = MoveRobot(robots[i].Item1, robots[i].Item2);
                }
                string render = "";

                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    for (int x = 0; x < grid.GetLength(0); x++)
                    {
                        render += grid[x, y];
                    }
                    render += "\r\n";
                    
                }

                if (possiblePrints.Contains(render) == true)
                {
                    break;
                }
                else
                {
                    possiblePrints.Add(render);
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < possiblePrints.Count; i++)
            {
                stringBuilder.Append(i.ToString() + "\r\n");
                stringBuilder.Append(possiblePrints[i]);
            }
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\Day14Part2.txt", stringBuilder.ToString());
            return 0;
        }

        private static (int,int) MoveRobot((int, int) pos, (int, int) velocity) 
        {
            pos.Item1 += velocity.Item1;
            pos.Item2 += velocity.Item2;
            if (pos.Item1 >= gridSize.Item1) { pos.Item1 -= gridSize.Item1; }
            if (pos.Item1 < 0) { pos.Item1 += gridSize.Item1; }
            if (pos.Item2 >= gridSize.Item2) { pos.Item2 -= gridSize.Item2; }
            if (pos.Item2 < 0) { pos.Item2 += gridSize.Item2; }
            return pos;
        }
    }
}
