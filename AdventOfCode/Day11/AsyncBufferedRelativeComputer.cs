using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Day09;

namespace AdventOfCode.Day11
{
    public class AsyncBufferedRelativeComputer : RelativeComputer
    {
        private Queue<long> ReadBuffer { get; } = new Queue<long>();
        private Queue<long> WriteBuffer { get; } = new Queue<long>();
        public bool Executing { get; private set; }

        public AsyncBufferedRelativeComputer(IEnumerable<long> code) : base(code) => Init();
        public AsyncBufferedRelativeComputer(IEnumerable<int> code) : base(code) => Init();

        public Task<long> ExecuteAsync()
        {
            if (Executing)
                return null;

            Executing = true;
            return new Task<long>(() =>
            {
                Thread.CurrentThread.Name = "Computer";

                var res = base.Execute();
                Executing = false;
                return res;
            });
        }

        private void Init()
        {
            Writer = () =>
            {
                while (Executing)
                    if (WriteBuffer.Count > 0)
                        lock (WriteBuffer)
                        {
                            if (WriteBuffer.Count > 0)
                            {
                                var e = WriteBuffer.Dequeue();
                                //Debug.WriteLine($"Computer reads {e}");
                                return e;
                            }

                            Thread.Sleep(1);
                        }

                return -1;
            };
            Reader = l =>
            {
                lock (ReadBuffer)
                {
                    //Debug.WriteLine($"Computer writes {l}");
                    ReadBuffer.Enqueue(l);
                }
            };
        }

        public long Read()
        {
            while (Executing)
                if (ReadBuffer.Count > 0)
                    lock (ReadBuffer)
                    {
                        if (ReadBuffer.Count > 0)
                        {
                            var e = ReadBuffer.Dequeue();
                            //Debug.WriteLine($"User reads {e}");
                            return e;
                        }

                        Thread.Sleep(1);
                    }

            return -1;
        }

        public void Write(long l)
        {
            lock (WriteBuffer)
            {
                //Debug.WriteLine($"User writes {l}");
                WriteBuffer.Enqueue(l);
            }
        }
    }
}