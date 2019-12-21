namespace AdventOfCode.Day04
{
    public class Part2Matcher : Part1Matcher
    {
        /// <inheritdoc />
        public override bool HasDouble (byte [] number)
        {
            for (var i = 0; i < number.Length - 1; i++)
                if (number [i + 1] == number [i] &&
                    (i == number.Length - 2 || number [i + 2] != number [i]) &&
                    (i == 0 || number [i - 1] != number [i]))
                    return true;

            return false;
        }
    }
}