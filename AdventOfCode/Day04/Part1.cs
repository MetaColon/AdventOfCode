using System;

namespace AdventOfCode.Day04
{
    public static class Part1
    {
        public static int Solve ()
            => CountPossibilities (Data.Minimum, Data.Maximum, new Part1Matcher ());

        public static int CountPossibilities (int min, int max, Matcher matcher)
            => CountPossibilities (Split (Data.Minimum), Split (Data.Maximum), matcher);

        private static int CountPossibilities (byte [] min, byte [] max, Matcher matcher)
        {
            var number = new byte[min.Length];
            min.CopyTo (number, 0);

            var count = 0;

            while (!HasExceeded (number, max))
            {
                if (matcher.MeetsCriteria (number))
                    count++;

                Increase (number);
            }

            return count;
        }

        private static void Increase (byte [] number)
        {
            var carry = true;
            for (var r = number.Length - 1; r >= 0 && carry; r--)
            {
                var increased = number [r] + 1;
                carry      = increased == 10;
                number [r] = (byte) (increased % 10);
            }
        }

        private static bool HasExceeded (byte [] number, byte [] max)
        {
            if (max.Length < number.Length)
                return true;

            if (max.Length > number.Length)
                return false;

            for (var i = 0; i < number.Length; i++)
            {
                if (max [i] < number [i])
                    return true;
                if (max [i] > number [i])
                    return false;
            }

            return false;
        }

        private static byte [] Split (int number)
        {
            var size = (int) Math.Ceiling (Math.Log10 (number));
            var res  = new byte[size];

            for (var i = 0; i < size; i++)
            {
                var power = Math.Pow (10, i);
                var high  = number / power;
                var digit = high % 10;
                res [res.Length - i - 1] = (byte) digit;
            }

            return res;
        }
    }
}