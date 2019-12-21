using System.Collections.Generic;

namespace AdventOfCode.Day02
{
    public static class Part1
    {
        public static int Solve ()
        {
            var code = new List <int> (Data.Program);
            Initialize (code);

            var computer = new ArithmeticComputer (code);
            var result = computer.Execute ();

            return (int) result;
        }

        private static void Initialize (List <int> code)
        {
            code [1] = 12;
            code [2] = 2;
        }
    }
}