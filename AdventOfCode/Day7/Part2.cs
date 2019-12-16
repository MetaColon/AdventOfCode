using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdventOfCode.Day7
{
    public static class Part2
    {
        public static int Solve () => GetMaxPermutationValue (new HashSet <byte> {9, 8, 7, 6, 5});

        private static int GetMaxPermutationValue (HashSet <byte> numbers) => Part1.Permute (numbers).Select (EvaluateOutputSignalSerial).Max ();

        private static int EvaluateOutputSignalSerial (List <byte> permutation)
        {
            var inputs = new Dictionary <int, Queue <int>> (permutation.Count);
            var computers = permutation.Select ((b, i) =>
            {
                inputs [i] = new Queue <int> (new [] {(int) b});
                return new PausableComputer (Data.Code);
            }).ToList ();

            inputs [0].Enqueue (0);

            for (var i = 0;; i++)
            {
                var index    = i % computers.Count;
                var computer = computers [index];

                computer.Reader = r => inputs [(index + 1) % computers.Count].Enqueue (r);
                computer.Writer = () =>
                {
                    if (inputs [index].Count > 0)
                        return inputs [index].Dequeue ();

                    computer.Pause ();
                    return -1;
                };

                var response = computer.Resume ();
                if (response >= 0 && index == computers.Count - 1)
                    return inputs [0].Dequeue ();
            }
        }

        private static int EvaluateOutputSignal (List <byte> permutation)
        {
            var computers = permutation.Select (b =>
            {
                var computer = new BufferedComputer (Data.Code);
                computer.Write (b);
                return computer;
            }).ToList ();

            // Initial value
            computers [0].Write (0);

            for (var i = 1; i <= computers.Count; i++)
            {
                var currentComputer  = computers [i % computers.Count];
                var previousComputer = computers [i - 1];
                currentComputer.WaitForWriteContent = () =>
                {
                    if (previousComputer.HasRead ())
                        currentComputer.Write (previousComputer.Read ());
                };
            }

            Parallel.Invoke (computers.Select (computer => new Action (() => computer.Execute ())).ToArray ());

            return computers.Last ().Read ();
        }
    }
}