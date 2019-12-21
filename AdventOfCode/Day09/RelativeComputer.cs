using System;
using System.Collections.Generic;
using AdventOfCode.Day05;

namespace AdventOfCode.Day09
{
    public class RelativeComputer : TestComputer
    {
        public RelativeComputer(IEnumerable<int> code) : base(code) => Init();

        public RelativeComputer(IEnumerable<long> code) : base(code) => Init();

        private void Init()
        {
            Lengths.Add(9, 1);
            HasParameters.Add(9);
        }

        private long RelativeBase { get; set; } = 0;

        protected override int PerformOperation(int position, int length, int opCode, long[] parameters)
        {
            switch (opCode)
            {
                case 9:
                    RelativeBase += parameters[0];
                    return position + length + 1;
                default:
                    return base.PerformOperation(position, length, opCode, parameters);
            }
        }

        protected override int GetIndex(int mode, int i)
        {
            EnsureMemory(i);

            switch (mode)
            {
                case 0:
                    EnsureMemory(Code[i]);
                    goto case 1;
                case 1:
                    return base.GetIndex(mode, i);
                case 2:
                    EnsureMemory(Code[i] + RelativeBase);
                    return (int) (Code[i] + RelativeBase);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EnsureMemory(long index)
        {
            while (index >= Code.Count)
                Code.Add(0);
        }

        protected override void WriteResult(int position, long value, int length)
        {
            var index = GetIndex(GetMode(position, length - 1), position + length);
            EnsureMemory(index);

            Code[index] = value;
        }

        protected override void WriteResult(int position, bool value, int length)
        {
            var index = GetIndex(GetMode(position, length - 1), position + length);
            EnsureMemory(index);

            Code [index] = value ? 1 : 0;
        }
    }
}