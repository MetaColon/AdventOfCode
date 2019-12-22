using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day12
{
    public static class Part1
    {
        public static int Solve() => Simulate(Data.ParsePositions().Select(pos => new Moon(pos)).ToHashSet(), 1000).Sum(moon => moon.Energy());

        private static HashSet<Moon> Simulate(HashSet<Moon> moons, int steps)
        {
            var pairs = Pairs(moons).ToHashSet();

            for (var i = 0; i < steps; i++)
                Step(moons, pairs);

            return moons;
        }

        private static void Step(HashSet<Moon> moons, IEnumerable<(Moon, Moon)> pairs = null)
        {
            StepAccelerate(pairs ?? Pairs(moons.ToHashSet()));
            StepMove(moons);
        }

        private static void StepAccelerate(IEnumerable<(Moon, Moon)> pairs)
        {
            foreach (var pair in pairs)
                pair.Item1.AccelerateTo(pair.Item2);
        }

        private static void StepMove(IEnumerable<Moon> moons)
        {
            foreach (var moon in moons)
                moon.Move();
        }

        private static IEnumerable<(Moon, Moon)> Pairs(HashSet<Moon> moons) => moons.SelectMany(moon => moons.Except(new[] {moon}).Select(moon2 => (moon, moon2)));
    }
}