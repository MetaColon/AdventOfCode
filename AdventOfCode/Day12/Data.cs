using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day12
{
    public static class Data
    {
        private const string Positions = "<x=-5, y=6, z=-11>\n<x=-8, y=-4, z=-2>\n<x=1, y=16, z=4>\n<x=11, y=11, z=-4>\n";

        public static IEnumerable<(int X, int Y, int Z)> ParsePositions() =>
            from line in Positions.Split('\n')
            select string.Join("", line.Replace(new[] {"<", ">", "=", " ", "x", "y", "z"}, ""))
            into raw
            select raw.Split(',')
            into parts
            where parts.Length == 3
            select (int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));

        private static string Replace(this string s, string[] toReplace, string replacement)
        {
            var f = s;
            foreach (var tr in toReplace)
            {
                f = f.Replace(tr, replacement);
            }

            return f;
        }
    }
}