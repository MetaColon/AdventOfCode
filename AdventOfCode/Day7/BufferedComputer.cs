using System;
using System.Collections.Generic;
using System.Threading;


namespace AdventOfCode.Day7
{
    public class BufferedComputer : PipedComputer
    {
        private Queue <int> ReadBuffer          { get; } = new Queue <int> ();
        private Queue <int> WriteBuffer         { get; } = new Queue <int> ();
        public  Action      WaitForWriteContent { private get; set; }

        /// <inheritdoc />
        public BufferedComputer (IEnumerable <int> code, Action waitForWriteContent = null) : base (code)
        {
            WaitForWriteContent = waitForWriteContent;

            Reader = ReadBuffer.Enqueue;
            Writer = () =>
            {
                while (true)
                {
                    WaitForWriteContent.Invoke ();
                    if (WriteBuffer.Count != 0)
                        break;
                    Thread.Sleep (1);
                }

                return WriteBuffer.Dequeue ();
            };
        }

        public bool HasRead () => ReadBuffer.Count > 0;
        public int  Read ()       => ReadBuffer.Dequeue ();
        public void Write (int v) => WriteBuffer.Enqueue (v);
    }
}