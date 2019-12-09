using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day3
{
    public static class Part2
    {
        public static int Solve ()
        {
            var instructions1 = Part1.Parse (Data.Instructions1);
            var instructions2 = Part1.Parse (Data.Instructions2);

            var (startX, startY, field) = Part1.Initialize (instructions1, instructions2);

            var intersections = Part1.GetIntersections (field, startX, startY, instructions1, instructions2);


            return (from intersection in intersections
                    let steps1 = GetSteps (intersection.X, intersection.Y, startX, startY, instructions1)
                    let steps2 = GetSteps (intersection.X, intersection.Y, startX, startY, instructions2)
                    select steps1 + steps2).Min ();
        }

        private static int GetSteps (int destinationX, int destinationY, int startX, int startY, List <(Direction Direction, int Count)> instructions)
        {
            var positions = new List <(int X, int Y)> ();
            var x = startX;
            var y = startY;

            foreach (var instruction in instructions)
                for (var i = 0; i < instruction.Count; i++)
                {
                    switch (instruction.Direction)
                    {
                        case Direction.North:
                            y++;
                            break;
                        case Direction.East:
                            x++;
                            break;
                        case Direction.South:
                            y--;
                            break;
                        case Direction.West:
                            x--;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException ();
                    }

                    positions.Add ((x, y));

                    if (x == destinationX && y == destinationY)
                        return positions.Count;
                }

            return positions.Count + 1;
        }
    }
}