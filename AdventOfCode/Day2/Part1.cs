using System;
using System.Collections.Generic;


namespace AdventOfCode.Day2
{
    public static class Part1
    {
        public static int Solve ()
        {
            var code = new List <int> (Data.Program);
            Initialize (code);

            Execute (code);

            return code [0];
        }

        public static void Execute (List <int> code)
        {
            var position = 0;

            while (position < code.Count && code [position] != 99)
            {
                PerformOperation (code, position);
                position += 4;
            }
        }

        private static void PerformOperation (List <int> code, int position)
        {
            var        opCode = code [position];
            (int, int) parameters;
            int        res;
            switch (opCode)
            {
                case 1:
                    parameters = GetParameters (code, position);
                    res        = Add (parameters);
                    WriteResult (code, position, res);
                    return;
                case 2:
                    parameters = GetParameters (code, position);
                    res        = Multiply (parameters);
                    WriteResult (code, position, res);
                    return;
                case 99:
                    return;
            }

            Console.WriteLine("Something went wrong; ");
        }

        private static (int, int) GetParameters (List <int> code, int position) => (code [code [position + 1]], code [code [position + 2]]);

        private static void WriteResult (List <int> code, int position, int value) => code [code [position + 3]] = value;

        private static int Add ((int, int) parameters) => parameters.Item1 + parameters.Item2;

        private static int Multiply ((int, int) parameters) => parameters.Item1 * parameters.Item2;

        private static void Initialize (List <int> code)
        {
            code [1] = 12;
            code [2] = 2;
        }
    }
}