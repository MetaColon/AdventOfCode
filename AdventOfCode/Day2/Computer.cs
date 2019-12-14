using System.Collections.Generic;


namespace AdventOfCode.Day2
{
    public abstract class Computer
    {
        protected List <int> Code { get; }

        protected Computer (List <int> code) => Code = code;

        public abstract int Execute ();
    }
}