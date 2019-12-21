using System.Linq;

namespace AdventOfCode.Day01
{
    public static class Part2
    {
        public static int Solve () => Data.Input.Select (i => FuelOfFuel (Part1.CalculateFuel (i))).Sum ();

        private static int FuelOfFuel (int fuel) => fuel <= 0 ? 0 : fuel + FuelOfFuel (Part1.CalculateFuel (fuel));
    }
}