using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day2
{
    public class ArithmeticComputer : Computer
    {
        public override int Execute ()
        {
            var position = 0;

            while (position < Code.Count && Code [position] != 99)
                position = PerformOperation (position);

            return Code [0];
        }

        protected Dictionary <int, int> Lengths { get; }

        protected HashSet <int> HasParameters = new HashSet <int>
        {
            1, 2
        };

        protected int PerformOperation (int position)
        {
            var    length     = GetLength (position);
            var    opCode     = Code [position] % 100;
            var parameters = new int[0];
            if (HasParameters.Contains (opCode))
                parameters = GetParameters (position, length);

            return PerformOperation (position, length, opCode, parameters);
        }

        protected virtual int PerformOperation (int position, int length, int opCode, int [] parameters)
        {
            switch (opCode)
            {
                case 1:
                    WriteResult (position, Add (new [] {parameters [0], parameters [1]}), length);
                    return position + length + 1;
                case 2:
                    WriteResult (position, Multiply (new [] {parameters [0], parameters [1]}), length);
                    return position + length + 1;
                case 99:
                    return position + length + 1;
                default:
                    Console.WriteLine ("Something went wrong.");
                    return 0;
            }
        }

        private int GetLength (int position) => Lengths [Code [position] % 100];

        private int [] GetParameters (int position, int count)
            => Enumerable.Range (position + 1, count).Select (i =>
            {
                switch (GetMode (position, i - position - 1))
                {
                    case 0:
                        return Code [Code [i]];
                    case 1:
                        return Code [i];
                    default:
                        throw new ArgumentOutOfRangeException ();
                }
            }).ToArray ();

        protected virtual int GetMode (int position, int index) => 0;

        protected void WriteResult (int position, int value, int length) => Code [Code [position + length]] = value;

        private static int Add (int [] parameters) => parameters.Sum ();

        private static int Multiply (int [] parameters) => parameters.Aggregate ((x, y) => x * y);

        public ArithmeticComputer (IEnumerable <int> code) : base (code) =>
            Lengths = new Dictionary <int, int>
            {
                {1, 3},
                {2, 3},
                {99, -1}
            };
    }
}