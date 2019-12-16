using System;
using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day7
{
    public static class Part1
    {
        public static int Solve () => GetMaxPermutationValue (new HashSet <byte> {0, 1, 2, 3, 4});

        private static int GetMaxPermutationValue (HashSet <byte> numbers) => Permute (numbers).Select (EvaluateOutputSignal).Max ();

        public static HashSet <List <byte>> Permute (HashSet <byte> input)
            => input.Count > 1
                ? input.SelectMany (b =>
                {
                    var subs = Permute (input.Except (new [] {b}).ToHashSet ());
                    return subs.Select (l =>
                    {
                        l.Insert (0, b);
                        return l;
                    });
                }).ToHashSet ()
                : new HashSet <List <byte>> {input.ToList ()};

        private static int EvaluateOutputSignal (List <byte> permutation)
        {
            var writeStack = new Stack <byte> (permutation);
            var readQueue  = new Queue <int> (1);
            readQueue.Enqueue (0);

            while (writeStack.Count > 0)
            {
                var step = 0;

                var reader = new Action <int> (v => readQueue.Enqueue (v));
                var writer = new Func <int> (() => step++ == 0 ? writeStack.Pop () : readQueue.Dequeue ());

                var computer = new PipedComputer (Data.Code, reader, writer);
                computer.Execute ();
            }

            return readQueue.Dequeue ();
        }
    }
}