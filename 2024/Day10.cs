using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2024
{
    public static class Day10
    {
        public const string mini_test_input_1 = "0123\r\n1234\r\n8765\r\n9876";
        public const string test_input_1 = "89010123\r\n78121874\r\n87430965\r\n96549874\r\n45678903\r\n32019012\r\n01329801\r\n10456732";
        public const string input_1 = "3212345678121056780310106543218765210109876\r\n7301106769012349891210237858909650987212345\r\n8943219878734217894354345997434501896398730\r\n7654306765025606765969876786521432345485621\r\n1233456834110715610870945431010676541014678\r\n0321067923230894320101234894321289239823549\r\n7450478810301129831230128765045658178101630\r\n8964329234532034761945219652136789017652721\r\n7875012187645695610876106543221054787243890\r\n4976701094556786921065987233234565692104323\r\n3289898763216547836976856100149874563495414\r\n4122305603407436545885445765454703454386901\r\n5001414512518921056794039870363212325677842\r\n6980523347676545121603128981278101210543234\r\n7879601258989236730512789974329892105601100\r\n0968708769076107845676697865012783416782321\r\n1651219940145045912389546521323676545699450\r\n2340367831231236705489030430654541236598769\r\n4565456328900987876018121046543010167894378\r\n3678995417811432980127630198672187098765298\r\n2980987606321501676534548767981096523030167\r\n1011876545450650101456759854102345614123454\r\n9872345034968763212321876543201298705323456\r\n8960101123879454789410989812120398676311237\r\n1051211012567345673508989401032367985200398\r\n2340349873498212232679876501001456810123478\r\n1232456780341000141089845432132345210194569\r\n0541542191232567657897894012201106761087654\r\n5670233088943498766501703025672239872398743\r\n4589104567652567898432612134984346765489232\r\n5498010988961043765430543210789655159896101\r\n4321023870870152896321432323658705014765012\r\n2349834561431161067816521874503216723454101\r\n1056767652521078154907890963212345894323221\r\n0148988943652189143878903454503454513411230\r\n3297803456789321012363212367696543201500549\r\n4586912369878934101454201008987012102676678\r\n5675001078767765210323123412108991343989654\r\n6784100981059874349014056503234780256978703\r\n7693201452342103458005347854345650107869012\r\n8543542367033012567176218961259654398054327\r\n9232632198124988943287307870568765212167898\r\n0101789087235677656896478969876890103456787\r\n";
        public const string test_input_2 = test_input_1;
        public const string input_2 = input_1;
        private static readonly (int, int)[] directions = { (1, 0), (0, -1), (-1, 0), (0, 1) };

        public static long Part_1(string input)
        {
            string[] lineStr = input.Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            int[][] grid = new int[lineStr[0].Length][];
            List<(int, int)> trailHeads = new List<(int, int)>();
            for (int x = 0; x < lineStr[0].Length; x++)
            {
                grid[x] = new int[lineStr.Length];
                for (int y = lineStr.Length - 1; y > -1; y--)
                {
                    int _y = lineStr.Length - 1 - y;
                    grid[x][_y] = lineStr[y][x] - '0';
                    if (grid[x][_y] == 0)
                    {
                        trailHeads.Add((x, _y));
                    }
                }
            }
            List<(int, int)[]> paths = new List<(int, int)[]>();
            for (int i = 0; i < trailHeads.Count; i++)
            {
                paths.Add(new (int, int)[10]);
                paths[paths.Count - 1][0] = trailHeads[i];
                CheckPath(grid, 0, ref paths);
            }
            Dictionary<(int, int), HashSet<(int, int)>> uniquePaths = new Dictionary<(int, int), HashSet<(int, int)>>();
            for (int i = 0; i < paths.Count; i++)
            {
                if (uniquePaths.ContainsKey(paths[i][0]) == false)
                {
                    uniquePaths.Add(paths[i][0], new HashSet<(int, int)>());
                }
                uniquePaths[paths[i][0]].Add(paths[i][9]);
            }
            return uniquePaths.Count;
        }

        public static long Part_2(string input)
        {
            return 0;
        }

        private static void CheckPath(int[][] grid, int idx, ref List<(int, int)[]> paths)
        {
            if (idx == 9) { return; }
            int newPathsCnt = 0;
            (int, int) pos = paths[paths.Count - 1][idx];
            for (int i = 0; i < directions.Length; i++)
            {
                if ( pos.Item1 + directions[i].Item1 < 0 || pos.Item1 + directions[i].Item1 >= grid.Length)
                {
                    continue;
                }
                if (pos.Item2 + directions[i].Item2 < 0 || pos.Item2 + directions[i].Item2 >= grid[0].Length)
                {
                    continue;
                }
                if (grid[pos.Item1 + directions[i].Item1][pos.Item2 + directions[i].Item2] == idx + 1)
                {
                    if (newPathsCnt > 0)
                    {
                        paths.Add(new (int, int)[10]);
                        Array.Copy(paths[paths.Count - 2], paths[paths.Count - 1], idx + 1);
                    }
                    paths[paths.Count - 1][idx + 1] = (pos.Item1 + directions[i].Item1, pos.Item2 + directions[i].Item2);
                    newPathsCnt++;
                    CheckPath(grid, idx + 1, ref paths);
                }
            }
            if (newPathsCnt == 0) paths.RemoveAt(paths.Count - 1);
        }

        
    }
}
