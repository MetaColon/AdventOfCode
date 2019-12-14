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
        }

        /// <inheritdoc />
        protected override int PerformOperation (int position)
        {
            switch (Code [position] % 100)
            {
                case 1:
                case 2:
                case 99:
                    return base.PerformOperation (position);
                case 3:
                    WriteResult (position, int.Parse (Console.ReadLine () ?? "-1"), Lengths [3]);
                    return position + GetLength (position) + 1;
                case 4:
                    Console.WriteLine (string.Join ("", GetParameters (position, Lengths [3])));
                    return position + GetLength (position) + 1;
                default:
                    Console.WriteLine ("Something went wrong.");
                    return position + GetLength (position) + 1;
            }
        }

        /// <inheritdoc />
        protected override int GetMode (int position, int index)
            => Code [position] / (int) Math.Pow (10, index + 2) % 10;
    }
}