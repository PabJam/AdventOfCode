using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace _2023
{
    public class Day10 : IDay
    {
        public static long Part_1(string input)
        {
            input = input.Replace("\r", "");
            string[] lines = input.Split('\n');
            char[][] map = new char[lines.Length][];
            Vector2Int startPos = new Vector2Int(-1, -1);
            List<Vector2Int> mainLoop = new List<Vector2Int>();
            List<char> chars = new List<char>();
            for (int y = 0; y < map.Length; y++)
            {
                chars.Clear();
                for (int x = 0; x < lines[y].Length; x++)
                {
                    chars.Add(lines[y][x]);
                    if (lines[y][x] == 'S')
                    {
                        startPos = new Vector2Int(x, y);
                    }
                }
                map[y] = chars.ToArray();
            }
            bool isMainLoop = false;
            for (long x = startPos.x - 1; x < startPos.x + 2; x++)
            {
                for (long y = startPos.y - 1; y < startPos.y + 2; y++)
                {
                    if (Math.Abs(x - startPos.x) + Math.Abs(y - startPos.y) != 1)
                    {
                        continue;
                    }
                    mainLoop.Clear();
                    Vector2Int lastPos = startPos;
                    Vector2Int nextPos = new Vector2Int(x, y);
                    ValueTuple<Vector2Int, Vector2Int> neighbours;
                    while (true)
                    {
                        if (nextPos.x < 0 || nextPos.x >= lines[0].Length)
                        {
                            break;
                        }
                        if (nextPos.y < 0 || nextPos.y >= lines.Length)
                        {
                            break;
                        }
                        mainLoop.Add(lastPos);
                        neighbours = GetConnection(nextPos, map);
                        if (neighbours.Item1 != lastPos && neighbours.Item2 != lastPos)
                        {
                            break;
                        }
                        if (neighbours.Item1 == lastPos)
                        {
                            lastPos = nextPos;
                            nextPos = neighbours.Item2;
                        }
                        else
                        {
                            lastPos = nextPos;
                            nextPos = neighbours.Item1;
                        }
                        if (nextPos == startPos)
                        {
                            isMainLoop = true;
                            mainLoop.Add(lastPos);
                            break;
                        }
                        if (mainLoop.Contains(nextPos) == true)
                        {
                            break;
                        }

                    }
                    if (isMainLoop == true)
                    {
                        break;
                    }
                }
                if (isMainLoop == true)
                {
                    break;
                }
            }

            return mainLoop.Count / 2;
        }

        public static long Part_2(string input)
        {
            string[] lines = input.Split('\n');
            char[][] map = new char[lines.Length][];
            Vector2Int startPos = new Vector2Int(-1, -1);
            List<Vector2Int> mainLoop = new List<Vector2Int>();
            List<Vector2Int> open = new List<Vector2Int>();
            List<Vector2Int[]> enclosed = new List<Vector2Int[]>();
            List<Vector2Int> designated = new List<Vector2Int>();
            List<char> chars = new List<char>();
            for (int y = 0; y < map.Length; y++)
            {
                chars.Clear();
                for (int x = 0; x < lines[y].Length; x++)
                {
                    chars.Add(lines[y][x]);
                    if (lines[y][x] == 'S')
                    {
                        startPos = new Vector2Int(x, y);
                    }
                }
                map[y] = chars.ToArray();
            }
            bool isMainLoop = false;
            for (long x = startPos.x - 1; x < startPos.x + 2; x++)
            {
                for (long y = startPos.y - 1; y < startPos.y + 2; y++)
                {
                    if (MathF.Abs(x - startPos.x) + MathF.Abs(y - startPos.y) != 1)
                    {
                        continue;
                    }
                    mainLoop.Clear();
                    Vector2Int lastPos = startPos;
                    Vector2Int nextPos = new Vector2Int(x, y);
                    ValueTuple<Vector2Int, Vector2Int> neighbours;
                    while (true)
                    {
                        if (nextPos.x < 0 || nextPos.x >= lines[0].Length)
                        {
                            break;
                        }
                        if (nextPos.y < 0 || nextPos.y >= lines.Length)
                        {
                            break;
                        }
                        mainLoop.Add(lastPos);
                        neighbours = GetConnection(nextPos, map);
                        if (neighbours.Item1 != lastPos && neighbours.Item2 != lastPos)
                        {
                            break;
                        }
                        if (neighbours.Item1 == lastPos)
                        {
                            lastPos = nextPos;
                            nextPos = neighbours.Item2;
                        }
                        else
                        {
                            lastPos = nextPos;
                            nextPos = neighbours.Item1;
                        }
                        if (nextPos == startPos)
                        {
                            isMainLoop = true;
                            mainLoop.Add(lastPos);
                            break;
                        }
                        if (mainLoop.Contains(nextPos) == true)
                        {
                            break;
                        }

                    }
                    if (isMainLoop == true)
                    {
                        break;
                    }
                }
                if (isMainLoop == true)
                {
                    break;
                }
            }
            designated.AddRange(mainLoop);

            List<Vector2Int> startDirections = [mainLoop[0] - mainLoop[1], mainLoop[0] - mainLoop[mainLoop.Count - 1]];
            if (startDirections.Contains(Vector2Int.Down) && startDirections.Contains(Vector2Int.Up))
            {
                map[mainLoop[0].y][mainLoop[0].x] = '|';
            }
            else if (startDirections.Contains(Vector2Int.Left) && startDirections.Contains(Vector2Int.Right))
            {
                map[mainLoop[0].y][mainLoop[0].x] = '-';
            }
            else if (startDirections.Contains(Vector2Int.Left) && startDirections.Contains(Vector2Int.Up))
            {
                map[mainLoop[0].y][mainLoop[0].x] = 'L';
            }
            else if (startDirections.Contains(Vector2Int.Right) && startDirections.Contains(Vector2Int.Up))
            {
                map[mainLoop[0].y][mainLoop[0].x] = 'J';
            }
            else if (startDirections.Contains(Vector2Int.Right) && startDirections.Contains(Vector2Int.Down))
            {
                map[mainLoop[0].y][mainLoop[0].x] = '7';
            }
            else if (startDirections.Contains(Vector2Int.Left) && startDirections.Contains(Vector2Int.Down))
            {
                map[mainLoop[0].y][mainLoop[0].x] = 'F';
            }


            Vector2Int pos;
            List<ValueTuple<Vector2Int, Vector2Int>> inOutList = new List<(Vector2Int, Vector2Int)>();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    pos = new Vector2Int(x, y);
                    if (designated.Contains(pos) == true)
                    {
                        continue;
                    }
                    if (FloodFill(pos, mainLoop, out Vector2Int[] fill, map, out ValueTuple<Vector2Int, Vector2Int> inOut) == true)
                    {
                        open.AddRange(fill);
                    }
                    else
                    {
                        enclosed.Add(fill);
                        inOutList.Add(inOut);
                    }
                    designated.AddRange(fill);
                }
            }

            Vector2Int direction = new Vector2Int(0, 0);
            int idx = 0;
            bool isOut = false;
            Vector2Int[,] mainLoopDirections = new Vector2Int[mainLoop.Count, 2];
            List<Vector2Int> trueEnclosed = new List<Vector2Int>();
            for (int i = 0; i < mainLoop.Count; i++)
            {
                if (map[mainLoop[i].y][mainLoop[i].x] == '-')
                {
                    idx = i;
                    direction = new Vector2Int(0, 1);
                }
                else if (map[mainLoop[i].y][mainLoop[i].x] == '|')
                {
                    idx = i;
                    direction = new Vector2Int(1, 0);
                }
            }
            for (int i = 0; i < mainLoop.Count; i++)
            {
                mainLoopDirections[idx, 0] = direction;
                Vector2Int check = mainLoop[idx] + direction;
                if (open.Contains(check) == true)
                {
                    isOut = true;
                }
                else if (check.x < 0 || check.x >= map[0].Length || check.y < 0 || check.y >= map.Length)
                {
                    isOut = true;
                }
                Vector2Int tempDirection = direction;
                switch (map[mainLoop[idx].y][mainLoop[idx].x])
                {
                    case '-':
                        break;
                    case '|':
                        break;
                    case 'L':
                        direction.x = tempDirection.y * -1;
                        direction.y = tempDirection.x * -1;
                        break;
                    case 'J':
                        direction.x = tempDirection.y;
                        direction.y = tempDirection.x;
                        break;
                    case '7':
                        direction.x = tempDirection.y * -1;
                        direction.y = tempDirection.x * -1;
                        break;
                    case 'F':
                        direction.x = tempDirection.y;
                        direction.y = tempDirection.x;
                        break;
                }
                mainLoopDirections[idx, 1] = direction;
                idx++;
                if (idx == mainLoop.Count) { idx = 0; }

            }
            bool match = false;
            for (int i = 0; i < inOutList.Count; i++)
            {
                match = false;
                idx = mainLoop.IndexOf(inOutList[i].Item1);
                if (mainLoopDirections[idx, 0] == inOutList[i].Item2 || mainLoopDirections[idx, 1] == inOutList[i].Item2)
                {
                    match = true;
                }
                if (match ^ isOut)
                {
                    trueEnclosed.AddRange(enclosed[i]);
                }
            }

            return trueEnclosed.Count;
        }

        private static (Vector2Int, Vector2Int) GetConnection(Vector2Int pos, char[][] map)
        {
            Vector2Int connection1, connection2;
            switch (map[pos.y][pos.x])
            {
                case '|':
                    connection1 = new Vector2Int(pos.x, pos.y + 1);
                    connection2 = new Vector2Int(pos.x, pos.y - 1);
                    break;
                case '-':
                    connection1 = new Vector2Int(pos.x + 1, pos.y);
                    connection2 = new Vector2Int(pos.x - 1, pos.y);
                    break;
                case 'L':
                    connection1 = new Vector2Int(pos.x, pos.y - 1);
                    connection2 = new Vector2Int(pos.x + 1, pos.y);
                    break;
                case 'J':
                    connection1 = new Vector2Int(pos.x, pos.y - 1);
                    connection2 = new Vector2Int(pos.x - 1, pos.y);
                    break;
                case '7':
                    connection1 = new Vector2Int(pos.x - 1, pos.y);
                    connection2 = new Vector2Int(pos.x, pos.y + 1);
                    break;
                case 'F':
                    connection1 = new Vector2Int(pos.x + 1, pos.y);
                    connection2 = new Vector2Int(pos.x, pos.y + 1);
                    break;
                case '.':
                    connection1 = new Vector2Int(-1, -1);
                    connection2 = new Vector2Int(-1, -1);
                    break;
                case 'S':
                    throw new Exception("Should not check start tile");
                default:
                    throw new Exception("Character should not be in map");
            }

            return (connection1, connection2);
        }

        private static bool FloodFill(Vector2Int startPos, List<Vector2Int> mainLoop, out Vector2Int[] fill, char[][] map, out ValueTuple<Vector2Int, Vector2Int> inOut)
        {
            bool isOpen = false;
            inOut = new(new Vector2Int(-1, -1), new Vector2Int(-1, -1));
            List<Vector2Int> flood = new List<Vector2Int>();
            List<Vector2Int> uncheckedFlood = new List<Vector2Int>();
            uncheckedFlood.Add(startPos);
            Vector2Int pos;
            while (uncheckedFlood.Count > 0)
            {
                for (long x = uncheckedFlood[0].x - 1; x < uncheckedFlood[0].x + 2; x++)
                {
                    for (long y = uncheckedFlood[0].y - 1; y < uncheckedFlood[0].y + 2; y++)
                    {
                        if (Math.Abs(x - uncheckedFlood[0].x) + Math.Abs(y - uncheckedFlood[0].y) != 1) { continue; }
                        if (x < 0 || x >= map[0].Length || y < 0 || y >= map.Length) { isOpen = true; continue; }
                        pos = new Vector2Int(x, y);
                        if (mainLoop.Contains(pos) == true)
                        {
                            inOut.Item1 = pos;
                            inOut.Item2 = uncheckedFlood[0] - pos;
                            continue;
                        }
                        if (flood.Contains(pos) || uncheckedFlood.Contains(pos)) { continue; }
                        uncheckedFlood.Add(pos);
                    }
                }
                flood.Add(uncheckedFlood[0]);
                uncheckedFlood.RemoveAt(0);
            }
            fill = flood.ToArray();
            return isOpen;
        }
    }
}
