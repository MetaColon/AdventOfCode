using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day11
{
    public static class Part2
    {
        public static int Solve()
        {
            var white = new HashSet<(int X, int Y)> {(0, 0)};
            Part1.PaintPanels(Data.Code, white);
            Draw(white);
            return 0;
        }

        private static void Draw(HashSet<(int X, int Y)> black)
        {
            var normal = Normalize(black);
            var sizeX = MaxX(normal) + 1;
            var sizeY = MaxY(normal) + 1;

            for (var y = 0; y < sizeY; y++)
            {
                for (var x = 0; x < sizeX; x++)
                    Console.Write(ParseColor(Part1.Look((x, y), normal)));
                Console.WriteLine();
            }
        }

        private static string ParseColor(int color) => color == 0 ? "." : "█";

        private static HashSet<(int X, int Y)> Normalize(HashSet<(int X, int Y)> set)
        {
            var minX = MinX(set);
            var minY = MinY(set);

            return set.Select(tuple => (tuple.X - minX, tuple.Y - minY)).ToHashSet();
        }

        private static int MaxX(HashSet<(int X, int Y)> set) => set.Max(t => t.X);
        private static int MaxY(HashSet<(int X, int Y)> set) => set.Max(t => t.Y);
        private static int MinX(HashSet<(int X, int Y)> set) => set.Min(t => t.X);
        private static int MinY(HashSet<(int X, int Y)> set) => set.Min(t => t.Y);
    }
}