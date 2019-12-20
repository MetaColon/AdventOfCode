using System;
using System.Collections.Generic;


namespace AdventOfCode.Day7
{
    public class PausableComputer : PipedComputer
    {
        /// <inheritdoc />
        public PausableComputer (IEnumerable <int> code, Action <long> reader, Func <long> writer) : base (code, reader, writer) {}

        /// <inheritdoc />
        public PausableComputer (IEnumerable <int> code) : base (code) {}

        private int Position { get; set; }
        public bool Paused { get; set; }

        /// <inheritdoc />
        public override long Execute ()
        {
            Position = 0;
            return Run ();
        }

        private long Run ()
        {
            while (Position < Code.Count && Code [Position] != 99)
            {
                if (Paused)
                    return -2;
                Position = PerformOperation (Position);
            }

            return Code [0];
        }

        public void Pause () => Paused = true;

        public long Resume ()
        {
            Paused = false;
            return Run ();
        }

        /// <inheritdoc />
        protected override int PerformOperation (int position, int length, int opCode, long [] parameters)
        {
            switch (Code [position] % 100)
            {
                case 1:
                case 2:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 99:
                    return base.PerformOperation (position, length, opCode, parameters);
                case 3:
                    var value = Writer.Invoke ();
                    if (Paused)
                        return position;

                    WriteResult (position, value, length);
                    return position + length + 1;
                default:
                    Console.WriteLine ("Something went wrong.");
                    return position + length - 1;
            }
        }
    }
}