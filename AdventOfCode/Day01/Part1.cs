using System;
using System.Linq;

namespace AdventOfCode.Day01
{
    public static class Part1
    {
        public static int Solve () => Data.Input.Select (CalculateFuel).Sum ();

        public static int CalculateFuel (int mass) => (int) (Math.Floor ((double) mass / 3) - 2);
    }
}