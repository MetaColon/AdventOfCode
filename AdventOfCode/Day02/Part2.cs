using System.Collections.Generic;

namespace AdventOfCode.Day02
{
    public static class Part2
    {
        public static int Solve ()
        {
            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var code = Initialize (Data.Program, noun, verb);
                    new ArithmeticComputer (code).Execute ();
                    var res = code [0];

                    if (res == 19690720)
                        return CalculateSolution (noun, verb);
                }
            }

            return -1;
        }

        private static int CalculateSolution (int noun, int verb) => 100 * noun + verb;

        private static List <int> Initialize (List <int> code, int noun, int verb) => new List <int> (code) {[1] = noun, [2] = verb};
    }
}