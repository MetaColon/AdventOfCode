using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day2
{
    public class ArithmeticComputer : Computer
    {
        public override long Execute()
        {
            var position = 0;

            while (position < Code.Count && Code[position] != 99)
                position = PerformOperation(position);

            return Code[0];
        }

        protected Dictionary<int, int> Lengths { get; set; }

        protected HashSet<int> HasParameters = new HashSet<int>
        {
            1, 2
        };

        protected int PerformOperation(int position)
        {
            var length = GetLength(position);
            var opCode = (int) (Code[position] % 100);
            var parameters = new long[0];
            if (HasParameters.Contains(opCode))
                parameters = GetParameters(position, length);

            return PerformOperation(position, length, opCode, parameters);
        }

        protected virtual int PerformOperation(int position, int length, int opCode, long[] parameters)
        {
            switch (opCode)
            {
                case 1:
                    WriteResult(position, Add(new[] {parameters[0], parameters[1]}), length);
                    return position + length + 1;
                case 2:
                    WriteResult(position, Multiply(new[] {parameters[0], parameters[1]}), length);
                    return position + length + 1;
                case 99:
                    return position + length + 1;
                default:
                    Console.WriteLine("Something went wrong.");
                    return 0;
            }
        }

        private int GetLength(int position) => Lengths[(int) (Code[position] % 100)];

        private long[] GetParameters(int position, int count)
            => Enumerable.Range(position + 1, count).Select(i => GetParameter(position, i)).ToArray();

        protected long GetParameter(int position, int i) => Code[GetIndex(GetMode(position, i - position - 1), i)];

        protected virtual int GetIndex(int mode, int i)
        {
            switch (mode)
            {
                case 0:
                    return (int) Code[i];
                case 1:
                    return i;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual int GetMode(int position, int index) => 0;

        protected virtual void WriteResult(int position, long value, int length) => Code[(int) Code[position + length]] = value;

        private static long Add(long[] parameters) => parameters.Sum();

        private static long Multiply(long[] parameters) => parameters.Aggregate((x, y) => x * y);

        private void Init() => Lengths = new Dictionary<int, int>
        {
            {1, 3},
            {2, 3},
            {99, -1}
        };

        public ArithmeticComputer(IEnumerable<int> code) : base(code) => Init();
        public ArithmeticComputer(IEnumerable<long> code) : base(code) => Init();
    }
}