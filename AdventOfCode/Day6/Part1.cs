using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day6
{
    public static class Part1
    {
        public static int Solve () => CountOrbits (GetObjects (Data.Map));

        private static HashSet <Object> GetObjects (HashSet <string> map)
        {
            var res = map.SelectMany (s => s.Split (')')).Select (s => new Object (s)).ToHashSet ();

            foreach (var parts in map.Select (orbit => orbit.Split (')')))
                res.First (o => o.Name.Equals (parts [1])).Orbits = res.First (o => o.Name.Equals (parts [0]));

            return res;
        }

        private static int CountOrbits (Object o) => o.Orbits == null ? 0 : 1 + CountOrbits (o.Orbits);

        private static int CountOrbits (HashSet <Object> objects) => objects.Select (CountOrbits).Sum ();
    }
}