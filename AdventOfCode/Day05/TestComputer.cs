using System;
using System.Collections.Generic;
using AdventOfCode.Day02;

namespace AdventOfCode.Day05
{
    public class TestComputer : ArithmeticComputer
    {
        /// <inheritdoc />
        public TestComputer(IEnumerable<int> code) : base(code) => Init();

        public TestComputer(IEnumerable<long> code) : base(code) => Init();

        private void Init()
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
        protected override int PerformOperation (int position, int length, int opCode, long [] parameters)
        {
            switch (opCode)
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
                    return (int) (True (parameters [0]) ? parameters [1] : position + length + 1);
                case 6:
                    return (int) (False (parameters [0]) ? parameters [1] : position + length + 1);
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

        private static bool True (long value) => value != 0;

        private static bool False (long value) => value == 0;

        private static bool LessThan (long a, long b) => a < b;

        private static bool Equals (long a, long b) => a == b;

        protected virtual void WriteResult (int position, bool value, int length) => Code [(int) Code [position + length]] = value ? 1 : 0;

        /// <inheritdoc />
        protected override int GetMode (int position, int index)
            => (int) (Code [position] / (long) Math.Pow (10, index + 2) % 10);
    }
}