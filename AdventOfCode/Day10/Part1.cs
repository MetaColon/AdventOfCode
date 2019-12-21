using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Day10
{
    public static class Part1
    {
        public static int Solve() => GetMaxVisibleAsteroidCount(Parse(Data.Map));

        private static int GetMaxVisibleAsteroidCount(bool[,] map)
        {
            var max = 0;

            for (var x = 0; x < map.GetLength(0); x++)
            for (var y = 0; y < map.GetLength(1); y++)
            {
                if (!map[x,y])
                    continue;

                var value = CountVisibleAsteroids(map, (x, y));
                if (value > max)
                    max = value;
            }

            return max - 1;
        }

        private static ReducedPair[,] CreateReducedDistanceMap((int X, int Y) pos, (int X, int Y) size)
        {
            var fin = new ReducedPair[size.X, size.Y];
            for (var x = 0; x < size.X; x++)
            for (var y = 0; y < size.Y; y++)
                fin[x, y] = new ReducedPair(x - pos.X, y - pos.Y);

            return fin;
        }

        private static int CountVisibleAsteroids(bool[,] map, (int X, int Y) pos)
        {
            var fractionMap = CreateReducedDistanceMap(pos, (map.GetLength(0), map.GetLength(1)));
            var visible = RemoveBlocked(map, fractionMap, pos);

            return CountAsteroids(visible);
        }

        private static int CountAsteroids(bool[,] map)
            => Enumerable.Range(0, map.GetLength(0)).Sum(x
                => Enumerable.Range(0, map.GetLength(1)).Count(y
                    => map[x, y]));

        private static bool[,] RemoveBlocked(bool[,] map, ReducedPair[,] fractions, (int X, int Y) pos)
        {
            var fin = Copy(map);

            for (var x = 0; x < map.GetLength(0); x++)
            for (var y = 0; y < map.GetLength(1); y++)
            {
                if (!fin[x, y] || x == pos.X && y == pos.Y)
                    continue;

                var similar = SimilarFractions((x, y), fractions);

                foreach (var sim in similar)
                {
                    if (!map[sim.X, sim.Y])
                        continue;

                    if (!SameQuadrant((x, y), sim, pos))
                        continue;

                    if (Distance(sim, pos) > Distance((x, y), pos))
                        continue;

                    fin[x, y] = false;
                }
            }

            return fin;
        }

        private static T[,] Copy<T>(T[,] a)
        {
            var fin = new T[a.GetLength(0), a.GetLength(1)];

            for (var x = 0; x < a.GetLength(0); x++)
            for (var y = 0; y < a.GetLength(1); y++)
                fin[x, y] = a[x, y];

            return fin;
        }

        private static bool SameQuadrant((int X, int Y) a, (int X, int Y) b, (int X, int Y) center)
            => Logic.Xor(a.X > center.X, b.X > center.X) && Logic.Xor(a.Y > center.Y, b.Y > center.Y);

        private static IEnumerable<(int X, int Y)> SimilarFractions((int X, int Y) pos, ReducedPair[,] fractions)
        {
            for (var x = 0; x < fractions.GetLength(0); x++)
            for (var y = 0; y < fractions.GetLength(1); y++)
                if ((x != pos.X || y != pos.Y) && fractions[x, y].InLine(fractions[pos.X, pos.Y]))
                    yield return (x, y);
        }

        private static double Distance((int X, int Y) a, (int X, int Y) b) => Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));

        private static bool[,] Parse(string raw)
        {
            var lines = raw.Split('\n');
            var fin = new bool [lines[0].Length, lines.Length];

            for (var y = 0; y < fin.GetLength(1); y++)
            {
                var line = lines[y];
                for (var x = 0; x < line.Length; x++)
                    fin[x, y] = line[x] == '#';
            }

            return fin;
        }
    }
}