using System;

namespace AdventOfCode.Day12
{
    public static class TupelUtil
    {
        public static (int X, int Y, int Z) Add(this (int X, int Y, int Z) a, (int X, int Y, int Z) b) =>
            (a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        public static (int X, int Y, int Z) Subtract(this (int X, int Y, int Z) a, (int X, int Y, int Z) b) =>
            (a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public static (int X, int Y, int Z) Sign(this (int X, int Y, int Z) t) =>
            (Math.Sign(t.X), Math.Sign(t.Y), Math.Sign(t.Z));

        public static (int X, int Y, int Z) Abs(this (int X, int Y, int Z) t) => (Math.Abs(t.X), Math.Abs(t.Y), Math.Abs(t.Z));

        public static int Sum(this (int X, int Y, int Z) t) => t.X + t.Y + t.Z;
    }
}