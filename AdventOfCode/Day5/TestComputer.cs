using System;
using System.Collections.Generic;

using AdventOfCode.Day2;


namespace AdventOfCode.Day5
{
    public class TestComputer : ArithmeticComputer
    {
        /// <inheritdoc />
        public TestComputer (List <int> code) : base (code)
        {
            Lengths.Add (3, 1);
            Lengths.Add (4, 1);
            Lengths.Add (5, 2);
            Lengths.Add (6, 2);
            Lengths.Add (7, 3);
            Lengths.Add (8, 3);

            HasParameters.Add (4);
            HasParameters.Add (5);
            HasParameters.Add (6);
            HasParameters.Add (7);
            HasParameters.Add (8);
        }

        /// <inheritdoc />
        protected override int PerformOperation (int position, int length, int opCode, int [] parameters)
        {
            switch (Code [position] % 100)
            {
                case 1:
                case 2:
                case 99:
                    return base.PerformOperation (position, length, opCode, parameters);
                case 3:
                    WriteResult (position, int.Parse (Console.ReadLine () ?? "-1"), length);
                    return position + length + 1;
                case 4:
                    Console.WriteLine (parameters [0]);
                    return position + length + 1;
                case 5:
                    return True (parameters [0]) ? parameters [1] : position + length + 1;
                case 6:
                    return False (parameters [0]) ? parameters [1] : position + length + 1;
                case 7:
                    WriteResult (position, LessThan (parameters [0], parameters [1]), length);
                    return position + length + 1;
                case 8:
                    WriteResult (position, Equals (parameters [0], parameters [1]), length);
                    return position + length + 1;
                default:
                    Console.WriteLine ("Something went wrong.");
                    return position + length + 1;
            }
        }

        private static bool True (int value) => value != 0;

        private static bool False (int value) => value == 0;

        private static bool LessThan (int a, int b) => a < b;

        private static bool Equals (int a, int b) => a == b;

        private void WriteResult (int position, bool value, int length) => Code [Code [position + length]] = value ? 1 : 0;

        /// <inheritdoc />
        protected override int GetMode (int position, int index)
            => Code [position] / (int) Math.Pow (10, index + 2) % 10;
    }
}