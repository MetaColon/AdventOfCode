using System;
using System.Collections.Generic;

using AdventOfCode.Day5;


namespace AdventOfCode.Day7
{
    public class PipedComputer : TestComputer
    {
        public Action <int> Reader { private get; set; }
        public Func <int>   Writer { protected get; set; }

        /// <inheritdoc />
        public PipedComputer (IEnumerable <int> code, Action <int> reader, Func <int> writer) : base (code)
        {
            Reader = reader;
            Writer = writer;
        }

        protected PipedComputer (IEnumerable <int> code) : base (code) {}

        /// <inheritdoc />
        protected override int PerformOperation (int position, int length, int opCode, int [] parameters)
        {
            switch (Code [position] % 100)
            {
                case 1:
                case 2:
                case 99:
                case 5:
                case 6:
                case 7:
                case 8:
                    return base.PerformOperation (position, length, opCode, parameters);
                case 3:
                    WriteResult (position, Writer.Invoke (), length);
                    return position + length + 1;
                case 4:
                    Reader.Invoke (parameters [0]);
                    return position + length + 1;
                default:
                    Console.WriteLine ("Something went wrong.");
                    return position + length - 1;
            }
        }
    }
}