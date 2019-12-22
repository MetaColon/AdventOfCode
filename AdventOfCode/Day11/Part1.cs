using System;
using System.Collections.Generic;
using System.Threading;
using AdventOfCode.Day09;

namespace AdventOfCode.Day11
{
    public static class Part1
    {
        public static int Solve() => GetPaintedPanels(Data.Code).Count;

        private static HashSet<(int X, int Y)> GetPaintedPanels(List<long> code)
        {
            Thread.CurrentThread.Name = "Main";

            var fin = new HashSet<(int X, int Y)>();
            var whitePanels = new HashSet<(int X, int Y)>();
            var pos = (0, 0);
            var direction = 0; // Up

            var computer = new AsyncBufferedRelativeComputer(code);
            var task = computer.ExecuteAsync();
            task.Start();

            do
            {
                fin.Add(pos);

                computer.Write(Look(pos, whitePanels));

                var color = (int) computer.Read();
                if (color == -1)
                    return fin;
                Draw(pos, color, whitePanels);

                var rotation = (int) computer.Read();
                if (rotation == -1)
                    return fin;
                direction = Rotate(direction, rotation);

                pos = Move(pos, direction);
            } while (computer.Executing);

            return fin;
        }

        private static int Look((int X, int Y) pos, HashSet<(int X, int Y)> white) => white.Contains(pos) ? 1 : 0;

        private static void Draw((int X, int Y) pos, int color, HashSet<(int X, int Y)> white)
        {
            switch (color)
            {
                case 0:
                    white.Remove(pos);
                    break;
                case 1:
                    white.Add(pos);
                    break;
            }
        }

        private static (int X, int Y) Move((int X, int Y) pos, int direction)
        {
            switch (direction)
            {
                case 0:
                    return (pos.X, pos.Y - 1);
                case 1:
                    return (pos.X + 1, pos.Y);
                case 2:
                    return (pos.X, pos.Y + 1);
                case 3:
                    return (pos.X - 1, pos.Y);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static int Rotate(int direction, int rotation) => rotation == 0 ? Mod(direction - 1, 4) : Mod(direction + 1, 4);

        private static int Mod(int a, int b)
        {
            var res = a;
            while (res < 0)
                res += b;

            return res % b;
        }
    }
}