using System.Collections.Generic;
using System.Linq;


namespace AdventOfCode.Day2
{
    public abstract class Computer
    {
        protected List<long> Code { get; }

        protected Computer(IEnumerable<int> code) => Code = code.Select(i => (long) i).ToList();
        protected Computer(IEnumerable<long> code) => Code = new List<long>(code);

        public abstract long Execute();
    }
}