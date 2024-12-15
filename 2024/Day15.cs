using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2024
{
    public static class Day15
    {
        public const string mini_test_input = "#######\r\n#...#.#\r\n#.....#\r\n#..OO@#\r\n#..O..#\r\n#.....#\r\n#######\r\n\r\n<vv<<^^<<^^";
        public const string test_input_1 = "##########\r\n#..O..O.O#\r\n#......O.#\r\n#.OO..O.O#\r\n#..O@..O.#\r\n#O#..O...#\r\n#O..O..O.#\r\n#.OO.O.OO#\r\n#....O...#\r\n##########\r\n\r\n<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^\r\nvvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v\r\n><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<\r\n<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^\r\n^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><\r\n^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^\r\n>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^\r\n<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>\r\n^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>\r\nv^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^";
        public const string input_1 = "##################################################\r\n#......O.O..O......O......O..OO...O..O.O.O..O..O.#\r\n#....O.O..#.O....O#.#.#......O.O#O.O#O#..O.O.....#\r\n#.........O..#O......O...OO..O..O..O#....O.......#\r\n#.O.O......O#.#..#.#.........O...OOO.O..O..O....O#\r\n#..O......O.......O..O.#O......OO.O.O#..O.OO..O..#\r\n#O.O.....O.O......O....O..OO...#.#...O##OO#O.....#\r\n#O..O....OOOO...#.O.O.O#.....##.....O#.##..O.....#\r\n#O..O.O....O.#..O.O.OO#O.#..#.O....O..O.#.....O..#\r\n##..OOOO..O..#O.#.O....O...............OO.....O..#\r\n#O.O#.........OO..O..#..O.....O..O...O#.....O....#\r\n#..#O#O#...#....O...OOOO..O.....#O.......O.......#\r\n#..........O...O.....#OOOO#O..#..O..#O.........O.#\r\n##..O.OO.O......#...#OOO.OO..O.O.O.OO#.#OO..O.OO.#\r\n#....O.O..OO.O.OOO..OO...O...O..O.O.O....O...O...#\r\n#O..#OO.........O#......OOO....O.....O.OO.#.O.##.#\r\n#.O.OOO.....O....O...#O.O.O.#.O.O..........O...OO#\r\n#.OO.OO...O...O..#........O.O...O.O...#OO.O....O.#\r\n#OO...O.OO.#O.....OOO.#O#.O.#......OO.....OO#..O.#\r\n#..O......O.....#...#O.....O.....OOO.....O.#OO...#\r\n#.....#O..O..#...OOO.#O.O..O.O#...#OO.O......O..##\r\n#..OO.OOO#.O.....OO..#.OO......OO.O....O...O.....#\r\n#.O.OO..............#..O.....O.O...OO.....#.O#...#\r\n#......OO.....OOO.O..O#OO...OO..O#..O..O.OO......#\r\n#.#O....O....O..#O..O..O@..#..#...#......O.......#\r\n#.#..O.O.OO...O.O#....O..#..O...OOOOO...#.OOO#...#\r\n##O..O.O....#O....OO#O.#O......O.....O..O...#O...#\r\n#...O...........O.#O..O.#...#.O.O.O...........#.##\r\n#O#O.......O..O.....OO.#O.O.#OOOO#.O.O.O.O.......#\r\n##O.O..OO...#.O..#.O.O..OOO.O.O#.....O.O..#.O.O.O#\r\n#..OO...O..#O.O.#.OOO.....OO........#..OO.#...O.##\r\n#O.#.#..#.O.O...O..O.#O..O.O.OO......O.OO.O...O.##\r\n#.O.OO...OO.........#....O.OO.OO..O#O.#O........##\r\n#.......O.......OO#...#....#.OOO#.#........#.O...#\r\n#...O.O.#.O.O.........OOO.O.O...O....O....#...O#.#\r\n#....O.O.......#.......#..O##.O.O.....O...O.O...O#\r\n#.O.O.O..O#.O.O...##O#O...O.....#OO#O.O....O..O..#\r\n#O.O.O.#..O...O.O.......#.OO..#..........O...O.O.#\r\n#...O...O.....O....##......#..OO....##.O..OO.....#\r\n#..OO..O..O..OOO.....#O..OO.O.OOOO.#.O...O.#.....#\r\n#...O...O.O.O...O.#O.#.O...O#.#......O..OO..O.##O#\r\n#...O.O#......OO.#.O.....OO..O.......#...#.O...#.#\r\n##OO..#O.....OO..............#...O#..O#......OOO.#\r\n##OOOO..O.#..O.O....O....O..O#.O#....O....O.O....#\r\n#.....#......O...OOO.O........O....#O.OO.....OO.O#\r\n#O.#....OO#....OO.O....#O#........#.#.OO.#.O...OO#\r\n#.O.....O.OO.....OO...#O..OOO.O...OO....O.O##O...#\r\n##O..OO.OO..#..O.O.O.##O......O..#....OOO....OO..#\r\n#O..O....O.........OO#.#......OO...##.O.......O..#\r\n##################################################\r\n\r\n>v><^>^v^v^^v><><>^><>>v<><<^<><>>v<^><^>><<<^v<>v>^<v>>><<<<^^<<^v>vv><>>v^<^>^^^^>v<<^<<>v<^^>v^v>>^v^v<<vv>^v^>v^v^^^>>vv<<><^^v<v^v<<<v^<v><<^>>^v<>vv^v<v<vv><<^^vvv<<^^v>^vv>v<v>>v>^v<^<^>^>><>><^>><>^>>v<vv<v^^<>^vv><v^<><>^vv^<<^>><<<>v^>^vv<v<^^^^^^<><v<<^vv^><>^<vv^<>v<>v<v<^<><<>^<v>v><<>^<<<^>vv<<^v^<<v>v<<v^v<v>v>v^<>^v<<v^><^>v^<>vv<v^^>>>>^v<v^><<vv^><v<>v^<^>^>v>>>>^^<<^<<<^^>>^><<v^>>^^^vv>^<<vvv<>v^<v^vv>vvv^<^>v>^<>>vv^^^><v>v<<vv<^<^vv>>>^v>>^>>^^^v<>^vv<<^<>vv>^<vv^>^<>v<^<<^>><>v><>vv<^v^>^v>>><<>^^<<<^><<^^^^>vv^^v^><<v^^v^v>v>^<^^<>v^><v^>^<>^<>>^<><^^<>><>^^^^v^v<<><^><^^v>v>>>>^^^>^^>^>vvv<>^>>v><vv<>^v>>^>^^v^v>^>^v^^>^<<<>v>vv<<<>><^><>><v^<><>>><>>>>>^<v><<>><>^><v><<>>^><^v><v>^<v>>v<vv>>>>vv^^^v^v<<<vv>^>vv^v^^^v><v^<>v<^^<^^>^<v^^<<>>v<^v^<v>^>^<<^<>^>v^^<vv<>^><>>vv^>v<vv><>>^v<<>^^>vvv>v^v<>^<^v^>>v>v^^<^>v>^^^<><>^^v^^>vv>^>v>^v<><<><<><>v>v><^>v^<>v<><^><><<>>>>^v^>><^><<>><>><vv^v>vv^^<<>>>>^v<^vv>>v>v<>^>^v^^<>>^>>v>v>^^><^<^^<^^><v^^<v<<^^<^<>^^v<<\r\nv<<<v><>vv^v<>vv<>>>><v^^v>^<<><^<^v<>>^^<^^<>>>><^v<^>^^^v^^<>>v>^>>vvvv^<>>>^<><<><><>^^<<><v<>v<<>^v<^v>>^<^v<v<v>>^^<<vv<><<^^^>v<<v><^^<><<<v<^vv^>>v<vv><vv>>v>><^v>>v<>>v<vv<v>>^<^^<>>^<v^>^><^>v><^<<^<^^<<>^<<v^^v^<^<v^><<<v>>^v><<v^^^vv<>>^^^v^>^^^^^>><><<<<^^^^<v^v><<<<v^>>^v>v^>>^>>^<v^<v><>^<<^v<^<>^^><v<>><<<^><vv^>^><vv<^>v<<<^<<<>v<^>>v^>v>>^<^^^v^>>v^<v<><>>^>>v^vv<v^v>><<<v<^^<vv>vvv>>>v^v<<>^^v>^>^<>vv^^<^<<>>^^>^v^>v^v^v><><^v<>>vvv<^>^>v^^<^>^^>v^^^v^v^><>v<><^^<>>^vv^>^^^<^<v<<>^<>vv<><^<>>^>^^>^>vv><v^>v>vv>^v^^v>v>^>^^^^^<<<v>vv>>^vv^v<<><^vv>>>><v<>^vv^<>^>^vv<vv>v^^v><v>v<<v<^^vvv>>^^v<<v>>v><>^v>^v<>^<v>>v<<<^<^<<v^v<><>v^<<>v>>^vv>v^vv<vv^^<><^^^^<^^>v><^<>>v>v>><<<v<<^^^<<>v>>><v<v^vv<v<v<<<^<>^^>^><v><^v><<vv>^^v^v<v>>v^>v>v<<^<^<><vv<<<^<<^^^<>>v<^^v^<<>^>^v<vv<<>>v>>v^v<^vv^>v^v<v>>>^v>>^<<^^<v^v^^<^>^^^v<v>^<^v<><vv^^v<<v>>v>v^>v^^<<<<^^<>^vvv<v<vvv^v><v<>^>^<^v><<^v<^<^>^v>^<<<<^>v^v^<vv<>>v<v^v^<v>>^<v^v^^v^>v<<<^>v<^<v<>>vvvv<<^v<>^^^>v<^vvvvv>><^^>>>^\r\n>>v<<^><>vv^v>vv<<><>><<vv<^<^^v^<<<>v^^v<^<^><v^><<>vv^>^vvv<^<v<v<>>^<><<<^v^><v>^v<v^^>><v<<>v<<<<>v<>^>v<v<<<v>>^>>><^vvv<^<>>^<^<vv<vvv^^<^^v<^<v>^>vv>v<<<>^>>v>>><^<^<^<<>>^v<>>>v>>^<^v>v>v^^><><^<<>v<>vv>^<<<^>><>v^^v<^vv^^vv<>v><^^v^<^<<^vv<<v>>v><<<v<vvv>^^>^<<>^^^^^v<^>^>^^^^^<^>v^<^^^v^^vv^v^<vvvv<vv<vvv^<vvv^v<>^^^<>v^<>>^<vv>>vvvv^<v^><^^>v^^>v^^<v>>>v>^^^<v^<v^^>>^^v^<^>^vv<>><><<<<>>><<<><v^>^<>^<>>>>v><vv><<<v<<^>>^^>><<^^v>>^>v>v^^<<>^<v>^vv^^<>vvv>v<^v^<^^^v><vv<>^<<<>^v^>^^>>v^vv<vv>vv>vv>v<>>v<<^^v>^<^v><^v<<<>^^v^<<^^>^><>^>^^<^>v^vv<<^><>vvv<^<^<<<vv^v>v^><<v><v>>^v^><>^>vv><<>^<^>>^<<<<<^v<v<^^v^^^v>vv>^v^<<>v>vv^>^><vv^v^><v^>><>^vvv>>^<^>^>^><<>>vvvvvvv>^v^<<<v<^v<v^v^><<^v>v^<v<<v><v<<<<><^^v><<<<><v<><^<^>^v^>v<>>v<^>^^>v>>^>>^<^^^>^^>>><vv^<>^<>>><^<>^>>v>>vv>v<vv^>v<><<^>v<><>^<><v<<>^><>^>>^vv>v<v>^><v>v>>^v<<>^>v>>^><><^<><<<>v^<^^^v^^>v<><<vv^<^^>v^v>>>>>v<<>>^>v<>vv>v>>v>>v<v>^<^><v^><>v><^^<<<^<<^v<^v^^^<<v^>>^<v>^^v>v^>>>>v>^v<^<<<v><<>v^<<>v<>vvvv>v^\r\n><v^^^<vv^<v><><^^vv>vv><v^>>^v^>^v<^^^<v><>vv>^v>^vvvv>v^>v<v<v<v<>vv^^v^v^v<v>^v>^><^>v^^<>><^vvv<v^^>>v<<^^<>><<v^v<<<<<^<<<>^^^>v^v^>>^<>vvv>^<>><<<v>v^v^v>^^<^v^>>>^^>>><>^>^vv><<^^^>vvv^^>>^^<<^v<<v>>vv^^<vvv<vv<<<<>>><<v^v>^><>^^>v^^><^^<v<<^^<^^^^v^<<>>><v<vvv<<>^v>>v>v<v^v<vv<><>^><v<v^<^^>v^<v<^>>vv^v^>^>>>^>>^<vvvv<<v>^v<<vvv>^v<<vvv<<<<>vv>v>>^><>^^>^^>v<^<v<>v>>v<>v<<vvv<>><><^>^vv><v^v>>>^^<^^><<><<v^^^>><><<>>v<v<^>>>>>>>^<>^<^<<v^v^^vvv^v>v>^v<^<<>><v<v^>vv^<v<>><<^<><v^^vvv<<vv>><<^v<>v<^^>>>v^vv>>>><<<^v>^^vv^<>^>^^^><>v^<>v^>v<v^v>>>^<v^^v^>^>^^^v<v^<><>>v^^>>>v<vv<v>v>>v^v>^vv<v^<>><><<v<v^>>v^>^>>^<><>v>>^<<<>>vv<>>>vv<v<<v^v>v<<<^<^^>v>^v>v<<^vv<^^v<><v^>^v<<v<^v^>>^<>v>v>><v<>vv>^^v>^^v<><<<^>>^<v>><^>^^^<^<<v<^^<><vv^<>v>^v<<<^<v<vv>>>v<<<v^<><^<><^<v^vv<v>v<>^><^^v><^v><>>^>v<<>>^>^v<>>^v>>v>v<<^<<>><<vv<>><<>>^>^<<><<><vv>^vv>^>v<^>v^^>v>>>>v<^>v>^v>>>v>vvvvvv<v^<v<^<^><vvv><>>><v>v<<<^^^^><^v>>vv>>^v>v<^>^^>v<><<^vv^vv>v^<v>v>v<v><v<^<v>^v><><<>^^>>vv<v>><<>>\r\n^v><>v^^<^>>>vv^>^^>^v>^^<v<v<>^<<^>^>vvv>><^v<^^><>^v>>^^>v<<^>><<>^<v>^><<>vv<^>>^<>v<vv^v>^>vvvv>v<>^<<^^<v<vv<^^v^v^^^<>^<^<v<>>v<>>^<<v<^vv^>^v^^>>^^^^<<^vvv<><<^>>><v<<v>^<^<>^<v^><<v^^^vv<<v^<vv>v<<><^^>>><^><<^^v><<^^>><>>^vv<v^v>^v<>v^v^<<<v^>^><<<^v>^>^^>^^>^vv<^vv^>><^^^<<<<v<v>^v^vv^<>><>^<vv<^^<v^^v^^<>v^v^^vvv^>>^^>vv>>><><<<<^v>>>^v<v<v<vvv<v^><<>>>>>><^^<^<^<v<v<vv^<v><v^^><v<^<<^^>>>>^^<><<^><><vv<^>v^v<<><<v>><><v><v><^><v<vvv>^v^^^^^<v<<>^<<^<^<>^><v<v>v><><vv>v^<v>><>v^^<>^^<v>^<^v<>>^^v^<v^<v>v^^^v<<<v^v^>v>v>v><v<<><v^<<<<<<vv>>^v^>^<v>>^^<v>>^<^<><>v><v>v>^<<v<v<v^<>><>>^^^<^v^<v<><v<v<^v<^vv>v>>^vv^><v<>^^>v^^>^>vvv<^v>^^>^<><>^<>v<<>><^<<<^v>^v^<vvv<><>>v^>^v<<<>v<><>><<vv^<>^<<<^^v>>^>v^<v^><^^vv>v<<>v^>vv^<><<^>v><>>^><^^^<v>>^>>v<^><<vvv^^<^>^^<v>vvv<<^v><v^^><^<<>v>v><<v^<>^<^v>>^^<<<>^>v>v^^^^<^<<<>vv<^vvv^^v<v>^><<<><^>vv^><v^>>>v<<>v>vv^^^><^<vv>>^><>^^>>v<v<>v<>>^>^>^<<v<<>v<v<^^v^<<v<><^v^^>>v^^><v><>^<^>>v><^<<<v>>^vv^<>>>^^><v^><<><v^<>>^vv^v^>^^v<^>\r\n^^^v^>vv^<<>^<^>>>^<<^vvv^>>>>><vv<>^<><vvvvv>v>v<<>^v<^v<v<v>^<v<<^><<^<^vv<<^v<>v<^<v>>>^v>><v<^^>v><^<v>^><<v<v<<<<^^>vvvvv^v>^^vv<<>^^v>v>vv><<<^^^^>^>vv>>v>>^vv<^<<><>v><v^^v<v^^>v><>^<>v^>vv<v^>v^>v>>v<v^>>vvv>v<><^vv<<vv>^>^<v^v^>v^><^<^>^<v^vvv>v<<^^>>v^>vvvvv<^^^><v^^>^<<v<v^>v<<<^^<<>v>v<<>^<^<v><v^<<^>><>^v<<^v<>^<<v>v>^<<>v<v^vv^>^>^^<v<<^<^^<<<v^>^>^v<^vv>v<<^v<>v<^vv<^^>>><^^^^<>><<^v<^<^<>>v<v<v<<>^^v^v^^v^<^^^>vv>>>^vvv<^>>^v><>v<^^v<><>v^^><^<^v<<>>^^<>^>^>v>><<v><><<<v<>v<v<>vv^vv^v<<<^^v^vv>^>^<^<>^>v>>><v<^^<v<<<^<<v>^<<^^v<^<<^^>vv<^^^^>>^^>^v><><^v<v^^^v<><>>v<<>^>^^v<^v^^>^<v^v<<<>^>>>v>^<<vv^><>^^><<^<>^>^^><<^<>^>^v>^^<^<>>^^v^v>v^>v^<^v^>v>v^^<^><v<vv^>^^>><<^v>^<<^^<^^<<^^>vv^>>^^v<>><<v^v<<<>^<>><^v^^^<<^vvv><^>>^>^>>v>^<><^<<^v>^<vvvv<>>^^^vv<^^v^<v^>>v>^^^>^>^v^<>v>>vv<^><>v><vv<<v<^^vv>^^><<><<v<^^^>>>vv><vv^^v<>>vv>^>>>^<<<>v^v>^vv^<<>>v<>v^<<v>>v^>>v<<^>^vv<>v^v^<v<^<vv>>><<v>v>^v>^v>^><^^v>>vv<>v^^v>><>>^^<^v^>^<vv>v><>^v<^^>v^^<>v^^v^>>^^>^<<<<^^>><^^\r\n<^<><<v^>>^<v^^<><><<>><^v<^<^<><v>>v<^^<<>><<>><v^>vv<vv^<>v^>v^v><^v^>>>^<^^><^<>><^>v^<v<<>v>^<v>v<^>^^<v><^<^^vv^>^^v<<>><<>>v^<>vv<<>^v><^<^^v<v<<<v<v>^^<><v^v^<<v^vv<v>v>v><^v>>^>^^>vv>>^v<<^^^^>^<<>>^>>^>^v^>^v<<^<>^v<<<^>><<v^<v<^v<><<>^v^>v>^^v>^^v>>v^>><<<^<^^>><^<><^v<^v<<v<>^v^v^>>v^^^<^<v<><vv>>v^<vvvv^v^v<v^>^<vv>><>v<<v<vv^^>>^>^vv^^v>><<>^^<^^v><>v^^v^>v<^^>>^<v>^vv^^^^^^vvv<>^v<^^><<<><<<^>v^>>^vv><>v><>vv^^^v><v^v^^>>>vvv>v><<<^<^^<^>^>v^<v^^v^v><v^v<>v^vv><v><<vvvv>>v>^^vv><v^<v<>^^v>><v<^^>^^v^v^vv^v>^v>^<v<>><><>^^<^<>>>><^>^v^^<><>v>^<^><><^<<vv^^vv<>^<v>>>vv<^<<^>><<^^^^>>v<v<<^>^v<^>v<<<v<v^<<v><<<<^v<<>v<^<^<^v<><<><^v<^^v><>v^^<>>>v^^v^>>>v^vvv>vv<^^^^><<^>v>>>^<v>v<v^v<<v>v<v^^>>>v^v>^v^<v^^v<^<<^>>^<><v><<^<^><^^^>vv^<v<v<><^v<>v>>^v<>^^^vv^^<<^^<<vv>^^^>v>>v>>v^><><>>^>vv^<v<<>><>vvvv<v<<^vv<<v<>v^v^^>>v<>v<<v^><^<^v<<<v^v<<^v<v^>v<<<^v>>>v^<<<<<<>v>^^>><^>>^<^><^>^<<<v^<<<>>^<>>>>vv>v<>^^v>>^v<^^<>v^v>^<v>^<<>^v^v^v^<<<^vv^^<><vv>vv^v^<><v<v>^<>^v><^^v<<vv\r\nv^<v<v<<^><><<v^>v<<v^<v<>^^>>v^>^v><<v^<v><<vv>><>^v^v><<v<^><^><<<><<^<^^>v><^v<v<v><v^^v^><^><>v<^>^v<v^><<^vv>>v<<v<^v><v^v^v><>v<v><v>>>^v>v^^^><^<v^v<vvvv<v^<v<v<><^^<v<<<^v<vv>>v^<<<^^>vv^vvv<<<<vvvv<<^>^<v<^<>^>>v<<v>vvvv>v>>^>>v<>v^vvv<><^^^>^<^>v^<<^<^v<<<vvv<^vvvvvv^<>v^^>^<^<<>^<v<^>^<^<^<<^v><v>vv<v>v^><^vv><^<<>vv^<>^>^v<<>>^v^<^^<><>^v<^><v<<vv<^><<><<v^><v<>><^v^v^v<<^>^v<>>^vv>^>>v^^vv>>>v^^^>vv>^<<<<>><>>^<<>^^^<<>>v^><^^>v^>^><^v<^^><^^^v^^v^<v<<^<<<<>>>><<<^<v^^>><>^^<^v<>^^^<><>vv>>^<><>>^v>v>>v^v^^^v>><><>><>>^><v^>^v><<v>>v^v^v<v><>v<vvvv^v<><v^>v>v>>>v<<<<>><><v><<v<>v<>v^<^^v^<^<>v^><<^>vv>>v<v>>>>^<>^^v>>^^^<>^<^v^>^v^>v><vv<><^^v>><><>>v^<<vv^>^><><^<vv^>^^<>^>v^^^v<vv><^v^>^v>>v>^vv^^><v>^^>>vv<^v^>^<>>>>>><>><>v>v><<v<<v>><><<^<v^^^^<^v>v^>v<>^<>vv<>v<<><v><^>v^><^>v^><<<>^><><^<^^^v^<<>^^<vv>>><^vv>>^v>><>v<v>^^v>v<^^><^<^>v<v^^vv>^<v<v<v<v^<^><v^<><><><>>><^^vvvvv^>>^^^<<>><>>v>>v>^^<<^^v>^^^v^v>><>>^<v>>^v^v><^>v><vv^^<vv>^v>v^<^^<>>^<<><>>>>>vvv>^><<^^v\r\n^><v<^<v^>v>^v<^^v^<><^<vv<>v>v^>vv^<v<<<^<v<<>^>>>v>vv^v><<><<v>>>^>><^>^<><>vv^^^>^<<^^>^^v<<v<^vv><v^vv<<v>^^^v>><v<^v>^<><^>^^vv>><>v>v^>><^^^>v>>>>v^>^>^^<^<^>>>>^v>v^>^<>v<<vv><<^<v^<^v>^v>^<v>v^><^>^v^<v>vvvv^<<v><<v<<<<v<^vv<vv>^v><>^^v>>^vv<v<v>><v<>>^^^v>>v<^>^><^vvv><<<^^^v^v<>v>>^>>>v^>>>^^>vv>>v^<<><<<<<v><^^^><><<><<v>vv<>>>^><vv^<><v<<^^^v>^v^v^>vv<>>v>^><^^^<vvv>^<v<<vvv><<<<<vv<<^><<v<v>^<^^v<^>^<>>^v>vv>>vv>v><<<<<>v<<v<^<<<^v<<v<>^<<v>^v^^>v<v>>>v<^^><>>^<^v<>^><>^^><<>v<vv>v>><^>v>>v<>>v^<v^>vvv^^^vvv<>^>>^v>v^<<v><v^^<^>vv><>v<>^>^<<><vv>^<<>^^>v><>^>><^><^><<v^^><vv>vvvvv<v>^<><vvv><vv<vv^v>^<v<v>>^vv<<v<><>>>><>^><v<v>^v><<^^^^<v>v^v^<v<<><v<>>><<>><v>^^^<><^v^^^^^v<^vv>vv<<^>>>>^v<^<v<>vvv>>v>>v^v>^<^v^vv<>^v^<<^<^>^<^<^^^^<^><^>>^vv>vv>^<v><<^v^^^^^<v>v>>v^>^<v>v<<v<>v><>v^^>v^<<^^>^><v^><>^>^v>v<<<<><<<vvv<><><<>^v<<<^>>v<<>^v>^<^^>>v^>^>vv<<><<^^v^vv>^^>v^^><v<^<<<^v>><<v^><^<<<v><<<<><v>><<v<>^><^><v>>vv^^<v<^><>v^^<>^vv>^v>^^v^^^^^<>^<>>>v<v<^><<<^>>v^<<<^<\r\n>^^<v>^^>vvv^^<^<<>>vv<^<<<^<vv><>^^<^^^<^^vvv<^v<>>v>^><v^>^^^v<<<^v>>^v>vv<^v^v<v><^>vv<>vvv^vvv<>^<<v><>>>^>v^>><^^^^v<v<<v>vvv>v^v>vvvv><v<^v<v<<<^v><<^^^<>v>^v^v^<>v^>v<<<><^^^<v>vv>^>^v>v><<v<>^^v<vv>v<><>v<vv<vvv>>>^<<^vv<<><<v^<vvv>v<<>vv^<^>^v^>^v<v^v>v>>vv^<v<v<<<^>>vv<<^<v>^<>v^>^>^^>v>v^<v<v^^<v<v>>v^>^v><^^><<^<^^<^^<^v><v<^>^>v>>^<<><>>v>v<^<^<v<^>>^v<^^<<<>^^v<^>>^v<>><^<<^<<<<>>>>v^^^^<>^>v<v>^>>>vv^v>v<><>v><vvv^>^v>><vvv^<<v^v>^vv^<<^v>>^<v<>>^v<><^v<v>><^><<^<^>v^>^^^v>^<^>^>^^><<>^><^>^^>v<>><v<<^<>>>>>v>><>^<^^^^<>>v<><>>><^<<>v^^<<vv>^^><<>><>^<>^^^<vvv^<v^^^^^>>^^<vv<<<v<<^v^<<v^><v>^^^><>v<^>>>^>>v<><v>v>>>v<>v<><vv^^><^<>^<><vv>vv>>vvv^v<>vv>><^>v<v>v<><><v<<v<vv^<>>v>>^v><<^>><^>>^<>><><<>v^<^>v<^^v>><<><>v^v<>>vv>>><vv<v^^<v^^>v^^vv>><^vvv^vv>>vv^^><<^^<v>v<^<<<<v^<<^<vv<^v^v>v^^^v<v>v><<<^>>v>><v>>vv<>>>>>^<<>v>v^>vv^>^^>>v<<v<^<><^>v>^vv><>^vv^<^>v>>vv<<vv>vvv>v^v<^^>^v>>^<><^v<^>^vv^^vvv>v<v>>v>^>>>^^<>vv><^v<vv>^><^><<<^^>v<<<><<v>^v<vv<vvv>^^<v<>v><<^<^<\r\n<v><<^vv>v<<>vv<v<<v^<>>>>^^^v^<^vv^><^<><v^<><^v^>^<^^<v><v>^>^v<>^<<><<^^<v<v<>^<>>>><<>>vvv^<v>^<v>vvv^^>><v<<^^>>^<v>^>^^<v>^>^<<<>^vv>^<<<^>^v<^v<v<>v^>^<<>^^v^v<<>>><^^>v>v<^^>>^><<v^><<>v^>^vv^>^>>>^<v<<^>><^vv<><^^<<>>v<><<v>vv>^v>v<vv>><<>><^<^<^^v<<^v<^>^><v^>v>>^><^^^^v<><v<^v<<><^>v<^vv<<>v>>>>^^<^^><>v^^<<^>^^^^>^>>><v<>v^>v^<>v^v^vv<<>v^<<><^<^<^>>>^<v^v>^v^>>>^^>^v><>vv<vv^<<v<vv><<<^v<vvv^<^^^vv<<v<v<>v^^v<>vv>v>v<>><^v>>v^<>v>>vv<^<>v^^<<^<>^<><<>vv>^>v>>^<<>v>v^v<>^>vv^<<vv<<^>>^vv^v<vvvv^v>^vvv<<v>><>^^v^<<^<v^v>v^<>^<>>vv>>>^^>><>v<^v^v^^<^>^>^v>>><<^^v><v>^v<v^>v>^vv<>^<<v<<<^vv^<^v<vvv>v^>><^><^^<v<>^v<v><vvv>vv<^>v^>>^v<<<^>>>v<^vv<^<v^>>><v^^v^<<v<^v>vv>v>v^^>^<<^><><v<>>v<v<^<>>^v<^^<>>^>^<v<v<^vvv^^<<v<^^^<v<>>>^^^<v<>v>^^v><<^<>><<>>vv>v<>^^><<^v>><<vv><v>^^>v<>>^v>vvv>>v<^<v>v^v^vvv^^vv<^>>v>^v^<<<v<>v<>^^vv^<<^^><<v>^v<^v<^>vv<vv>^<>v^>^v<<^<^>v<^<^v^><<>vvv><v><vv><v><v^v<^><>^^<v<<><v<<>^<>^<<^v^^>v<v>><^vvv^>>vvv^^^>>>^<<vv^><vvv><<^<>vv>vv>v<>>vvv<><><<\r\n^<><v^<vv^vv^vv^<v>^vv<>vvv<^<vv<<><^<vv^<<><>v^v^^vv^v>>v^v><<^^>v<><<^<>vv^<<^vvv>v^<^>^<^>^^><^vv>v^>v<>^vv^<^><v^^v<^^>v<>>v<^^><<>>><vv>v^^^v<<>v<<<v^v^^>v><<vvv>^<<^<<^>^v<v><v^v<>v^^v^v<><<<^<<^vv^v^<<<^vv>^<^<vv<v><<v<vv>v<<<v>>><v^<><^>v>^^^v<<<>>><^>v<v<<<^<<^^^>^^^<v>>>v^^<^v^^><>^<v^>v>v<^vvv^><>><^^^v><^<v>><>>v<^v^^<>>vv<<<>>>^^^^>>^^^>v>^><<>v>v<><^v^vvvvv><>vvv>^v><v>v>vvv>>vv<^<v>v>v^^v^>>^<<vvvv<vvv>>v<^<<<>v<><>^^v^<v<v<>>v<<<>>^v>^^<^>>v<^v^>v>^>>^vv><v>vvv<^>^^vv^^<>vv<><<v>^vv^>v><>v>^>v^<v<<v^<^v^v^vv^>^v>^v<^vv<>v^><^^><<^>^vv<vv><>vv<<<<<^vvv>^>^<vv^<><^v>vv><^>vvv>vvv>^vv^<^<><^^^<<<<vv>^<<<<<v^>>^<^<>>^<>>>v<<^^>vvv^^<<<><<<><>v^vv^<<vv^>>v><><<^<<^^<<^^>><v^>^v^vv<>>vvv^vvv<^<v><v>>v<<>>^v><v^v<><>><>v>^^>>>><<<>v<<>v^v>vv>><v<>^^^<>^^v<<^>>v<vv><v<v^v<^<>>vv>v<v<v<><>>v^v>^<^^^v<v^<vv<>^v>^>>>^^^<^>vv^>^^<>><^<>><>^><<^^^v<^^<<v<>^^<v^v<v<>^v<v<v>v^<<^>^<v>^><v^vv<<v>v><v<v>^<v<<^^^^v<^<^<vv<><v<><<vv><^^^<<>>vvv><^>>v<>vvv<v>v><<v^^^>^>^<<<v^>><v^>^<^^<v^>\r\nvv^<^^v>^<<^v>vvv<v<v^v^><<<v>^>^vvv>v>>>^<>^vvvvvvvv^<<>^>><v^^>v><v<>v^v><>>>>v><vv>^v<><vvv^vv^><>vv><<>>>v<v<<>vv>^v^>>^vv>vv^><^<><v^^v<v>v>^^>v>>>>v>v>^v^<>v>>v^><>>^<<>v><^^v>^^^vv^<^>^>>v^<^v>^v><>^<^^^^<>vv^^v^^^>^^><vv^^vv>^vv^<>>^<<>^v<v<<^vv<<>^^<^><>v<>^^v^^>vv<<v<>^<v>^<^^<>^><vv<><<>vv^^v^><>vv<<^^>>v>vv>^vv<v<<v<v>>^<^^<v^<^>^><>v>v>v>>>^v<>^^<^>v<v>>^v>>^v>>^^^v>^vvv^^v<>^<>^<^v<^<v>vv^<^<<^v><^vv><^^>vvvv<<<<>^>^>>^v>>><>><<v^^vv^^vv^<><>^>v^>^<^><<v^v<<^>><vv<><v>v^<vv^><^^^>v^><^^^<v<v>v^<>><<v>^<<^>v^<v^>^>>^vvv>>>v^v^<v>^<v<><<<>^v^^<v^vvv^^^>vv^v<^<v>v<vv^>^<<^^v^v^<v>vv><<>>v^<<vv>>>v^<>v^<>v<v<>^<>^v<>>^>>^<^v^>v^>^v<^^v<<v>>><>v>v<v^v>^^v^<^v^<>^^>>>v><>v<<<<^v>v>>^^<^>^<^<v<^<^>>^^>^<>v^><<^^v^<vv^><><><<^v<>v<><><vv^<>>^><>>vvv^<^^^^>^v<>v<^v<v><v><>><^<<<><>^^<>>^vv>>>v><^<^>><v^^>v><<><^><v<^v><>vv^v^^^^<^v<v>>^>>vv>v>>^^v<vvv^>^>v^>^<>v^vvv^^^<^vv^v^v>>vv^><^<^>>vv<^<vvv^>v^<<v><v<>^^vvvv>v>^^v^>>vv^^^<^>v<<<>^>v<>v^vv^><v^^>vv<>v>v^v>><^<><>v>>v>>^><v<^<\r\nv<vvvv<^>>v^v^>^<>>><v><><>v<^^v>v<>vv>^<><>^^v><<^v<>^<^>>^<>><<v>v^<>v>^<v>v<v<v<<>^<^v>>v<>^^<^<<^v<<>vv>v<<^<v>^v><v^^v>^^<^>>>v^<v^^v<^v<<v>v<v^<^^v<vvv>^><>v>^<v<<>^<vvv<vvvv^>^v>vv^^^><^>v>v<<^vv^<^^>^^vv^v<<^<<>>^>^v^v<v><><^v>v^v^<<^<v>^^<>^<^>^><><<>^>^^v^v<<><>v<>v^v>v<^<>^vv^^^vvv<v^>>>vvv>>v^>>>^^<^^><<>^v<>>^<^v^<><><vv^>^>^<^^>v>>^^v^vv^>>vv^^v><<v>><v<<<<>v<<<>>v<v^^v^^^^v>^vv>><>^>>>>><v<>>v<v^<vv<v>><<<^v>>>vv>v^<^<<^<v^>v^<>^^^<>>v<v<v<><>v>v<><>v<<v><^<>v^><>^vv<v^^<<<>^>>vv^^>^^<^<><vv<><^^><v<^v<>^>>><>^><vvv><^<<v^<>v^v<><<<^^>><<><v^<^vvv^v>><^^^><v^vvv>^v><^<<v^>^>v<v>>vv>^<^vv<<>v^<v>>^^^>^<<<^<v^>^^v<vvv^^>^<<^>><><v^vvv><><^^<<^<>^<>^v^v<<>^>^>>vv<v>>v>>v<vv<vv<v<^<v^v<<<^^>^v<<><>v^>vv<<vv^^^<>><v>^v>v<<vv<<^v^<^^^vv><vv^^^^^^vv<<>^v^vv>>><>^<>><<>>v>v^>>v>>v<<>v>^^v^^^<^v^^v>v>^>>v>><<v><^>><^<^>^>^<v^v^<<^>^v^<>^>v<>>^<>vv>v^<v<<vv><<v^v<<><<><vv><^>>v^><^v>^><<^>>><>>^<v><v^>v>v>>>>>vv<vvv<<>v>^^>v^^>v<v>^<^^<^>>^^>v><<v<<>^><^^><v>^^>>v<><^>v<v<><<>^v>v\r\n>vv>>>>v><<^v>^^vvvvv><^v<>^<<>^^<^vv^>^<^^<>^vv<<^<v>^v<^<><<v><<^<v<<<v<<vv^v<v^>^v<>><<^<<><<<>^vv<v><v<>^<^><>vv>v^v<v><^v<vv>^>^^^v>^^<<^v>v>^<<^^^v<v>v^<<^<<<>>>^>^v><<>v>^<^<<^vv>><<^<^^><<>^v<^^><vv^>><v><<v^v<<<><v>^<^<vv^>v<>>^vvv^^v>^v^^v>^>^^^^<>><vv>vv>>>vv<<^vv<>>><v><<^^<><<<>>^^^v>^v>v<^<^v^^<<^<^vv<<<<<<vv<^>v^vv^>v>v^<^^<vv<>^><v^^^v^>^><<^<^>^<<^<<>><v>^<^>^^><^v^^^^v>>v<>^^<><^<^^><>vv>>><vv<^<>^v>>vv>^^><vvv>v<>^>>>>>><<<^<>>^<^>^<^<v>v^v^^v^^^>>^<<>v>>v>>>v^<<v^><v>^>v>v>^>vvv<<<>v<^<>v<>>>^<v><<^<v><v>>vv>>><^v^<<<<<^v<^^vv>^vvv<^v>v^^<^^v>>^<<<>^>>^^>^>>>>^v>v^<^^><>v<<<<^<v>v>^>^>>v><<^^^>v>><vv>^><>>^vv^^^<^<>v^^<>^v>^<>>^^v^>^^^v<><v^>>^><<>^^>>v<>>^<>vvvv>>>^vvv>^vv><><>v^v<v<^vv^v<>>v>>vv><<<>vv<^v>v>^v><v^<<^^^^v<^<>^<><v>^>v<<^v^<v<^<vv>v^^v>^<<<>^v<v<^v^<v^<^^v^^>>v>v>v>^^v<<v>^v>v^>^<v>>^^<>^>^><^<><>^>v>^>><^^^vv^<><<<<^>>>><^^^>vv><^<>>><<v><^v>v<<^v^vv>><>v<><^v>v^v>><v<^^^^>^>v>v<>^^<>vv>>v><<>v>><^<^v<<^>vvvv<<v<<^^<v<^^<>>^v<>^^v<>^^>v<<<>>^^>vvv<\r\n>^v^v^>>vv><<^<>v>v^v^><<<>v<<^v<v<>v^^<^>^>^vv<<>v^>>^>v<^>^v>^<>v>v^><v><>>^<v<^>>v^<v^v^^^^v<<^>>>^>>><>v^v^>^>>vv^<^>v<>>v^^>>^><<<<^>>v><<<vvv>><^v^^><>v>vv^>v<<<<^^>>>><^>^>>>>^v<<><^^v^<v><^>>>>^>>v><^<><>>>>v><v>v>>>>v^^<v>>^v<<v<<>>^<v^<>>>^<<>>v>^><>v^^>^<<><^<>>>>v<vv^^v^v>^v^v>^><^<^v^v<^v^^>^^>><v><^>^>^>>^^>>^<>vv>><>v><^>>v<<^><<^<^>v^<v>vv>v^v^^>^>>v><^<>v^><>>>^^v>v^^>>>^v<><<<<<^v^><^>>^^vv^>><^<v>v><<v^><^>^>v<^^v^vv<<^>^<>^<v><v>v^vv^<>v<vv^vv>v>^><v>>vv>>^^>^<v<<v<<^^vvv<v<v<<<<<v>^^<>^v^><v>vv>^^^v^<<>^><v<<<<>^><vv^vvv^<<^vv><v^^<<<^<>><v<<^>vv>^^>v<<v>vvvv^<<<<<v>^^>^<>v><^<>v^>^<<>v^>><>>>^^<v>^<>v^>>>^^>><^<v><^<<>^<<<v>v^><<^v<v<><v^<^<><v^^<v^vv^>v^<v<v^><><<vv<>^v<<<^vv<>v^>^^v>><<<v<><vv><<v>^<v^^<>^<>vv<<v<<<><v><<v^^<v^><>^^^^v^vv^<>v>>>>v<>>><v>v>v>^>v^v>vvv^<<<>^^v>vv<v<v<><<>>^<>v<vv>>>^<<>^<v^>><v<v^vv>^^<<^v^v<<<<<<^>^^<<>v<<><<<^v><<<v^<^<v^><v<>>><>^><^<><vvv^v>v<^v><v>>vv<<>v^v<<^<^>^>^v<v<<v<v>><v^<^<^v>v<<<>^>^<>^<v^<v^v^v>^^^v^vv^v>^>vv><^<<>>\r\n^v><>v<^^^<>>>><>>>^v<vv<<^^<><<^<^>v>>^<<v>><v<<>><^>>v^v^^>v^>v^><^<vv^>><v<>v>vvv<<>v><^vvv>^^vv^<^>><v<>^vvv>^^^>^v^^vv^v^<>^^<>^<v>^^<<v>><<^^<<^<<<>^>v<v^<>>^>^v^^<>^><vvv^<vv<>v<^<vv><^^v<<<vv>^^^>v^^<v^^>v>><<<^<<><<<><><vv^v>>^^>^<<>>><<^>vv><v>^vv^<>^>><>^^<><><<><>^^<><^<>v>^^<<v>v^<<>>v>^>^^>vv^^v<^vv><<vv^^^^^<<<<v<^<<>v^^v^<v>v><<<<<>>v^>^>><<>^^><>v>>v^<^^^^>>v><<v>>^v<<v><vv>>v^<<^>>><^<^v<>vvv<<vv<^<v^v>v<v<<v^^v><<>v<v<>^<>><^>^^^>^v^<<>v^v^^>>v>^v>v<><<^>v^<><<<v^>>>>>vvv^v<>^^>^^^<vvv<vv^^>v<v><v<vv^><^v>>v<v^^>^vvv^^>>vvvv^><^vv^^vvv<<>>^>v^v^v><^^^^vv^^><^<<><<<<v><<^v<^<>>^^vv<><^^><v^<^><^>v^^^<^^>>vv^^>><v<<^<>>><v<^<<v<v>v>>^>>>>><v>v<><^^>^<v<^^vv><<<^<><v^<^v^^v<<<v<<<vvv>><>>>vvvv^<^^v<v^v^vv^v^<v^<<>v>><^^^v><^v^<v^^><vv^<><^^<^<<<<vvv<v><>>>v^<v>v>vvv>><vv>^>^<^><^v>vvv^v><><>>><^<v<v<<<>v<v<<v^>^<v^v<vvvvv>>^><v>>v>>>>vv^<vvv^v^v>>v>v^><<>v^vv^><^>^<vv^v<<v><vv<<^v<>^><v^<^>v>vv^<^vv<v^>^<vv<v^><^>vvv<v^<^<>^v<v^^>><^^<vv^>>vv><^>>^><>>v><>><<<^>>v>^^<<^\r\n>>^vv>^>^><>>>v>^><^<><>><<<<><<^<^<v^v^v>>>><><vv>>^<>v<v^>v<<^>>v^v>^>^>v<^v>>v<vv^vvv<<^^^<^v<vv<>>^^><<><<^v><^^v>><v>^<v<><^>>v><v^v<^<v^>>v>>>^v<^v><<>v>^<^^^<^vv><<<><>^v^v^^>^v>>v<v<^<<v^^><<^vv>v>^vv<^>v<><<^<<^<^^v^<vv>^<>vv<^>>^^^<^v><>vv^>vv^^vvvv^>v>^v>^<><v^^>v<><vv^><>^>v<>><v<<>^^>^<>vv<>^vvv>><^^v<>>^<v<v^>>v^>>>vvv<<vv^^^^>>^^<<>vv<<>>>v<>v>v^^v<vvv<><><<<^v^>^v^^^v>>v^>v>^v<>v^><>^><v^vv<^<^v^^v<><vv<<^^><v>>^<>^<v>v<<v^^v<v><<>^><>>><><><<>^^<^>vvvv<<v<^<^v><^<<>>vvv^^vvvv>vv<<v<>^^<><<><>^<^v<^v>^^<><>>v><^>><v<v>v>>vv<<v<v<vv>^v^<>>^v^<><<<vv^^>v^<<v^v<^v^>><vv><>^^>><<^>><<v>^<>^>v<^^^vv^<<<<^vv>><vv^><^><^<^^<><v<<^>^^><v>^>^vv><vv><^v>v<vv>^><v^<vv>>vv^v>v>><v^^><^<vvv<v^^<<v^>^<^^^vvv<^>^>^><^^<v^<>v^vvv>^<<v<^^v>v>>>><>vv<>^v<<v>>^<^<<<<^v>^^>>vv>>v>v<vv^><^^v<<<v<<<<^>v^<^v<^^^><^v>^><v^^<^<>>>v^^<^v^<v<^^>vvv>^<<^v<>>^v<<^^v^<v<v<<v^v^<<>v>^^^<v>>^>>^^>>v^>v><>vv^<v<vv^v<vv^^vv>^^>>v<^>>v>v^<>>v^^v>>>v<<>vvvvv>><><>^^<<v>^<>v^^^<><^>><^^v^v^^<^<v><><<<v><<>\r\nvv>>^v<v<^v<<<<^v<>v<<^v^<vv><>>v<<><v<<><<^vvvv<<^<<v<>v<v>>^^>^<<^v<^v<^<>v><<vv^<vv<v^>v><<>vv^v><vv<<<>><^<v<<><<>v>>v><>>^^<>>>^<>v><><>v^<v>v>>^v>v>^<>><>^^<<^v><>^<^><^>>v^^<^<v^<>^^v>>v<^v><v^>^>vvv>v<<vvv^<v<v>v>v>^^vv<<>v>><^v^v^^v<^vv<<><>>>^^^v>v^^^>v^<><^<>^><v>><<>v^>^>v>><v>v>><^v^v<<>^v<>>v^v^v^^v^^>>v>^<^>>><^v^<<^>^>^>>v>>^>vvvv>>>^^^<<vv>^v<v<^^<v<<>^v>^<^>>v<><<v>v><v>v^<><^<<^v^<^<<^^v>^<v<<>v>v^<><^v^^>><>^<^^><<v^^^v^>>>v>vv^v^v<<<>^v<>^^vv<>^v><^vv<^vv^<<<^<<^^><<><<>^v^<v>v<vv^^^^>vv^^>^<<v<><^>^<<<v<><vv><^v<<>>v>>>><^^>^>^>>>>^<vv>^^v^^>v<^^<^vv>>^^^^vv<^>v<><vv<^^v><<><><^v^><^^>>^^>vv><v^<>v^v>v>^<<v><<^v^v>v>^<v^>><v^<<^<>>^^>>><vv<vv<<>>v<<^<^>><v<v>v><>v^<v^v>^vv>>><^^>>v^^>^>^<^>^v<v<^^>v>v<<vv>>^<>v>v^<v^<<vvv>^^<>vv>>^^>>><<^v<<v>vvv><v>^>v^vvvv^>>^<^<>v<vv>^<v^><<<v^v^>vv><>^<<vv^>>^>>^^v<><v<v^>>v^^v^><^><v^>^^v<^>^v^>^<>>^^>vv^vv<^^v<<<<^<vv<<<^vv>^^<vv^<v<v^><^>v>v^<<>^<^^^^^v>^^>^>vv>>v><^v^^>v<^>v^>><v<<v^>^>>^^v><>>^><<v<>v<><^^v>^v>^>v<v<^v<>^\r\n>^vv>^<vvvv>v<><<vv>><vv<>v^<<>>v>v<^><>^v<><>>^<<<v>^>vvvvv^>vv^^<>><^<>v>^^><v<>^<>>vv^vv^^v^>vv^<v>v^>>vv>>v^><<<>>^<<^>vv>v>^<>>>^^<v^^>><v^v<v^><<v>^v<>>^vv<>v<vv>^^>>><^<^^>>>vvv><><vv<v>v^<^^<^><>>^<<><<>>v>v^^^><^>v<<>^^<<vv<>>>v>>v^v<<><<^v^^<<vvv^v^^<>v><<^v>>^v<>^^^^v>^>>^>>v^>^>>v>>><^><<^v><>><><v^<v<<>><^v<^>vv<v^<>v^<<^><<<^^>>v<^>>v>>v>>>^<v^v<v<<>v>^><<vv>vv><^>v>^v<v^v><^<^vv^<vv<^^v>^^v^v<v<>>>>>^v>>^>^>v<>^>^<v^<>>^^v>v<>^<><^^<v<v^><<^v>v><>v>^>^<>><>>^>v^<v>v<<<>^><^v<><v>^^^^v<><^^v^^<^v>^<<v^>^>><^v><><<^^^v>><^^<<^>^vv<<^<>>v<<>>><v>>>^<<v<<><v<^v<v^v<>>^>v<<v^<><<>v<^v^<vv>><vvv>v<^^<<^v<<^v><>^vv>v<><v^><v<^^<<<>v<<<^^v^><^^^^<^^v<<^<>v><>>v<vv<vv<v^v<<>><v^v>^v<><v<><v>v><>><^>^^^<v<<^^>>v<<<<v<<^v^vv>>v^>vv<><<<^^><v>^^>^^v<^>^^^v<^^>>>^<^<>v<vv<<^^><^^v^vv<v<v^<^v<<v><^^^<v<>v<^<<v><^><><>v^v>>>^vv^<<^v<<><<<><^^^>>^<^v^v<<>v<v<^<^v^<<^<^>^v^v>v><^^>^>>^<<><v^><<^<<<>v^><>><^<v^v>>^<^<^v^<<<^>^<>^^<>>^<^v^v>v^><v^<>v^>>vv>>>^<>vv<><>^>>^><>>v><^>>>^><^v<vv";
        public const string test_input_2 = test_input_1;
        public const string input_2 = input_1;
        private static readonly Dictionary<char, (int, int)> directions = new Dictionary<char, (int, int)>()
        {
            { '>', (1,0)},
            { 'v', (0, -1)},
            { '<', (-1, 0)},
            { '^', (0, 1)},
        };

        public static long Part_1(string input)
        {
            string[] inputStr = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] gridLines = inputStr[0].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string sequence = string.Join("", inputStr[1].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));

            char[][] grid = new char[gridLines[0].Length][];
            (int, int) robotPos = (0, 0);
            for (int x = 0; x < gridLines[0].Length; x++)
            {
                grid[x] = new char[gridLines.Length];
                for (int y = gridLines.Length - 1; y > -1; y--)
                {
                    int _y = gridLines.Length - 1 - y;
                    grid[x][_y] = gridLines[y][x];
                    if (grid[x][_y] == '@')
                    {
                        robotPos = (x, _y);
                    }
                }
            }

            for (int i = 0; i < sequence.Length; i++)
            {
                bool moved = Move(ref grid, robotPos, sequence[i]);
                if (moved == false) { continue; }
                robotPos = (robotPos.Item1 + directions[sequence[i]].Item1, robotPos.Item2 + directions[sequence[i]].Item2);
            }


            long sum = 0;
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] != 'O') { continue; }
                    sum += x + (grid[x].Length - y - 1) * 100;
                }
            }
            return sum;
        }

        public static long Part_2(string input)
        {
            string[] inputStr = input.Split("\r\n\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string[] gridLines = inputStr[0].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string sequence = string.Join("", inputStr[1].Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));

            char[][] grid = new char[gridLines[0].Length * 2][];
            (int, int) robotPos = (0, 0);
            for (int x = 0; x < gridLines[0].Length; x++)
            {
                grid[2 * x] = new char[gridLines.Length];
                grid[2 * x + 1] = new char[gridLines.Length];
                for (int y = gridLines.Length - 1; y > -1; y--)
                {
                    int _y = gridLines.Length - 1 - y;
                    switch (gridLines[y][x])
                    {
                        case '#':
                            grid[2 * x][_y] = '#';
                            grid[2 * x + 1][_y] = '#';
                            break;
                        case '.':
                            grid[2 * x][_y] = '.';
                            grid[2 * x + 1][_y] = '.';
                            break;
                        case 'O':
                            grid[2 * x][_y] = '[';
                            grid[2 * x + 1][_y] = ']';
                            break;
                        case '@':
                            grid[2 * x][_y] = '@';
                            robotPos = (2 * x, _y);
                            grid[2 * x + 1][_y] = '.';
                            break;
                    }
                }
            }

            //int i = 0;
            //sequence = "";
            for (int i = 0; i < sequence.Length; i++)//while (true)
            {

                // Move your self
                //string render = "";
                //for (int y = grid[0].Length - 1; y > -1; y--)
                //{
                //    for (int x = 0; x < grid.Length; x++)
                //    {
                //        render += grid[x][y];
                //    }
                //    render += "\r\n";
                //}
                //Console.Clear();
                //Console.WriteLine(render);
                //ConsoleKeyInfo keyInfo = Console.ReadKey();
                //switch (keyInfo.Key)
                //{
                //    case ConsoleKey.UpArrow:
                //        sequence += "^";
                //        break;
                //    case ConsoleKey.RightArrow:
                //        sequence += ">";
                //        break;
                //    case ConsoleKey.DownArrow:
                //        sequence += "v";
                //        break;
                //    case ConsoleKey.LeftArrow:
                //        sequence += "<";
                //        break;
                //}
                //Console.Clear();

                // Check sequence
                //Console.WriteLine(sequence[i]);
                //string render = "";
                //for (int y = grid[0].Length - 1; y > -1; y--)
                //{
                //    for (int x = 0; x < grid.Length; x++)
                //    {
                //        render += grid[x][y];
                //    }
                //    render += "\r\n";
                //}
                //Console.WriteLine(render);
                

                bool moved = MoveThin(ref grid, robotPos, sequence[i]);
                if (moved == true) { robotPos = (robotPos.Item1 + directions[sequence[i]].Item1, robotPos.Item2 + directions[sequence[i]].Item2); }
                
                //i++;
            }

            

            long sum = 0;
            for (int x = 0; x < grid.Length; x++)
            {
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] != '[') { continue; }
                    sum += x + (grid[x].Length - y - 1) * 100;
                }
            }
            return sum;
        }

        private static bool Move(ref char[][] grid, (int, int) pos, char dir)
        {
            (int, int) nexPos = (pos.Item1 + directions[dir].Item1, pos.Item2 + directions[dir].Item2);
            if (grid[nexPos.Item1][nexPos.Item2] == '#') { return false; }
            if (grid[nexPos.Item1][nexPos.Item2] == 'O') { Move(ref grid, nexPos, dir); }
            if (grid[nexPos.Item1][nexPos.Item2] == '.')
            {
                grid[nexPos.Item1][nexPos.Item2] = grid[pos.Item1][pos.Item2];
                grid[pos.Item1][pos.Item2] = '.';
                return true;
            }
            return false;
        }

        private static bool MoveThin(ref char[][] grid, (int, int) pos, char dir)
        {
            (int, int) nexPos = (pos.Item1 + directions[dir].Item1, pos.Item2 + directions[dir].Item2);
            if (directions[dir].Item2 == 0)
            {
                if (grid[nexPos.Item1][nexPos.Item2] == '#') { return false; }
                if (grid[nexPos.Item1][nexPos.Item2] == ']') { MoveThin(ref grid, nexPos, dir); }
                if (grid[nexPos.Item1][nexPos.Item2] == '[') { MoveThin(ref grid, nexPos, dir); }
                if (grid[nexPos.Item1][nexPos.Item2] == '.')
                {
                    grid[nexPos.Item1][nexPos.Item2] = grid[pos.Item1][pos.Item2];
                    grid[pos.Item1][pos.Item2] = '.';
                    return true;
                }
                return false;
            }
            else
            {
                if (grid[nexPos.Item1][nexPos.Item2] == '#') { return false; }
                if (grid[nexPos.Item1][nexPos.Item2] == ']') { MoveWide(ref grid, (nexPos.Item1 - 1, nexPos.Item2), dir, false); }
                if (grid[nexPos.Item1][nexPos.Item2] == '[') { MoveWide(ref grid, nexPos, dir, false); }
                if (grid[nexPos.Item1][nexPos.Item2] == '.')
                {
                    grid[nexPos.Item1][nexPos.Item2] = grid[pos.Item1][pos.Item2];
                    grid[pos.Item1][pos.Item2] = '.';
                    return true;
                }
                return false;
            }
        }

        private static bool MoveWide(ref char[][] grid, (int, int) posL, char dir, bool scouting)
        {
            (int, int) posR = (posL.Item1 + 1, posL.Item2);
            (int, int) nextPosL = (posL.Item1 + directions[dir].Item1, posL.Item2 + directions[dir].Item2);
            (int, int) nextPosR = (posR.Item1 + directions[dir].Item1, posR.Item2 + directions[dir].Item2);
            string front = grid[nextPosL.Item1][nextPosL.Item2].ToString() + grid[nextPosR.Item1][nextPosR.Item2].ToString();
            bool canMove = false;
            if (front.Contains('#')) { return false; }
            else if (front == "[]") { canMove = MoveWide(ref grid, nextPosL, dir, scouting); }
            else if (front == "].") { canMove = MoveWide(ref grid, (nextPosL.Item1 - 1, nextPosL.Item2), dir, scouting); }
            else if (front == ".[") { canMove = MoveWide(ref grid, nextPosR, dir, scouting); }
            else if (front == "][") 
            { 
                canMove = MoveWide(ref grid, (nextPosL.Item1 - 1, nextPosL.Item2), dir, true); 
                canMove = canMove && MoveWide(ref grid, nextPosR, dir, true);
                if (canMove)
                {
                    MoveWide(ref grid, (nextPosL.Item1 - 1, nextPosL.Item2), dir, false);
                    MoveWide(ref grid, nextPosR, dir, false);
                }
            }
            front = grid[nextPosL.Item1][nextPosL.Item2].ToString() + grid[nextPosR.Item1][nextPosR.Item2].ToString();
            if (front == "..")
            {
                canMove = true;
                if (scouting == false)
                {
                    grid[nextPosL.Item1][nextPosL.Item2] = grid[posL.Item1][posL.Item2];
                    grid[nextPosR.Item1][nextPosR.Item2] = grid[posR.Item1][posR.Item2];
                    grid[posL.Item1][posL.Item2] = '.';
                    grid[posR.Item1][posR.Item2] = '.';
                }
                
            }
            return canMove;
        }


    }
}
