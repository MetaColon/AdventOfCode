using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day3
{
    public static class Part1
    {
        public static int Solve ()
        {
            var instructions1 = Parse (Data.Instructions1);
            var instructions2 = Parse (Data.Instructions2);

            var dimensions = BuildExtreme (CalculateMaxDimensions (instructions1), CalculateMaxDimensions (instructions2));
            var (startX, startY, sizeX, sizeY) = BreakDimensions (dimensions);

            var field = new int[sizeX, sizeY];
            InitializeField (field);

            Simulate (field, (startX, startY), instructions1, 1);
            Simulate (field, (startX, startY), instructions2, 2);

            var crossPoints = GetCrossPositions (field);
            var manhattans  = ToManhattanDistances (crossPoints, (startX, startY));

            return manhattans.Min ();
        }

        private static HashSet <int> ToManhattanDistances (HashSet <(int X, int Y)> points, (int X, int Y) startPosition)
            => points.Select (tuple => Math.Abs (tuple.X - startPosition.X) + Math.Abs (tuple.Y - startPosition.Y)).ToHashSet ();

        private static HashSet <(int X, int Y)> GetCrossPositions (int [,] field)
        {
            var fin = new HashSet <(int X, int Y)> ();

            for (var x = 0; x < field.GetLength (0); x++)
                for (var y = 0; y < field.GetLength (1); y++)
                    if (field [x, y] == 3)
                        fin.Add ((x, y));

            return fin;
        }

        private static void Simulate (int [,] field, (int X, int Y) position, List <(Direction Direction, int Count)> instructions, int bit)
        {
            foreach (var instruction in instructions)
            {
                var direction = instruction.Direction;
                var count     = instruction.Count;

                for (var i = 0; i < count; i++)
                {
                    switch (direction)
                    {
                        case Direction.North:
                            position.Y++;
                            break;
                        case Direction.East:
                            position.X++;
                            break;
                        case Direction.South:
                            position.Y--;
                            break;
                        case Direction.West:
                            position.X--;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException ();
                    }

                    Mark (field, position, bit);
                }
            }
        }

        private static void Mark (int [,] field, (int X, int Y) position, int bit) => field [position.X, position.Y] |= bit;

        private static void InitializeField (int [,] field)
        {
            for (var x = 0; x < field.GetLength (0); x++)
                for (var y = 0; y < field.GetLength (1); y++)
                    field [x, y] = 0;
        }

        private static (int StartX, int StartY, int SizeX, int SizeY) BreakDimensions ((int North, int East, int South, int West) dimension)
            => (-dimension.West, -dimension.South, dimension.East - dimension.West + 1, dimension.North - dimension.South + 1);

        private static (int North, int East, int South, int West) BuildExtreme ((int North, int East, int South, int West) a, (int North, int East, int South, int West) b)
            => (Math.Max (a.North, b.North), Math.Max (a.East, b.East), Math.Min (a.South, b.South), Math.Min (a.West, b.West));

        private static (int North, int East, int South, int West) CalculateMaxDimensions (List <(Direction, int)> instructions)
        {
            var maxEast  = 0;
            var maxWest  = 0;
            var maxNorth = 0;
            var maxSouth = 0;

            var x = 0;
            var y = 0;

            foreach (var direction in instructions)
            {
                switch (direction.Item1)
                {
                    case Direction.North:
                        y += direction.Item2;
                        break;
                    case Direction.East:
                        x += direction.Item2;
                        break;
                    case Direction.South:
                        y -= direction.Item2;
                        break;
                    case Direction.West:
                        x -= direction.Item2;
                        break;
                }

                if (x > maxEast)
                    maxEast = x;
                if (x < maxWest)
                    maxWest = x;
                if (y > maxNorth)
                    maxNorth = y;
                if (y < maxSouth)
                    maxSouth = y;
            }

            return (maxNorth, maxEast, maxSouth, maxWest);
        }

        private static List <(Direction, int)> Parse (List <string> instructions)
            => instructions.Select (s =>
            {
                Direction direction;
                switch (s [0])
                {
                    case 'U':
                        direction = Direction.North;
                        break;
                    case 'R':
                        direction = Direction.East;
                        break;
                    case 'D':
                        direction = Direction.South;
                        break;
                    case 'L':
                        direction = Direction.West;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException ();
                }

                var count = int.Parse (s.Substring (1));

                return (direction, count);
            }).ToList ();
    }
}