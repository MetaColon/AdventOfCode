using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Day10
{
    public static class Part2
    {
        public static int Solve() => SimulateLaser(Part1.Parse(Data.Map), (27,19)).Skip(199).FirstOrDefault().Comprise();

        private static int Comprise(this (int X, int Y) t) => t.X * 100 + t.Y;

        private static IEnumerable<(int X, int Y)> SimulateLaser(bool[,] map, (int X, int Y) pos)
        {
            var directions = OrderDirections(GetDirections(pos, (map.GetLength(0), map.GetLength(1))));
            var mapClone = Part1.Copy(map);

            for (var i = 0; mapClone.Enumerate().Any(b => b); i = (i + 1) % directions.Count)
            {
                var hit = GetHit(pos, directions[i], mapClone);
                if (hit == null) continue;

                Debug.WriteLine($"{i}: {hit}");
                mapClone[hit.Value.X, hit.Value.Y] = false;
                yield return hit.Value;
            }
        }

        private static (int X, int Y)? GetHit((int X, int Y) pos, Direction direction, bool[,] map)
        {
            if (direction.X == 0 && direction.Y == 0)
                return null;

            for (var i = 1;; i++)
            {
                var target = pos.Add(direction.Multiply(i).ToPair());

                if (target.X < 0 || target.Y < 0 || target.X >= map.GetLength(0) || target.Y >= map.GetLength(1))
                    return null;

                if (map[target.X, target.Y])
                    return target;
            }
        }

        public static (int X, int Y) Add(this (int X, int Y) a, (int X, int Y) b) => (a.X + b.X, a.Y + b.Y);

        private static IEnumerable<T> Enumerate<T>(this T[,] c)
            => Enumerable.Range(0, c.GetLength(0)).SelectMany(x
                => Enumerable.Range(0, c.GetLength(1)).Select(y
                    => c[x, y]));

        private static List<Direction> OrderDirections(IEnumerable<Direction> directions) => directions.Distinct().OrderBy(pair => pair).ToList();

        private static IEnumerable<Direction> GetDirections((int X, int Y) pos, (int X, int Y) size)
        {
            for (var x = 0; x < size.X; x++)
            for (var y = 0; y < size.Y; y++)
                if (x != pos.X || y != pos.Y)
                    yield return new Direction(x - pos.X, y - pos.Y);
        }
    }
}