using System.Collections.Generic;


namespace AdventOfCode.Day2
{
    public abstract class Computer
    {
        protected List <int> Code { get; }

        protected Computer (IEnumerable <int> code) => Code = new List <int> (code);

        public abstract int Execute ();
    }
}