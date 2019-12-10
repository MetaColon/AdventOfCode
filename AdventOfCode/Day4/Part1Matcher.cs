namespace AdventOfCode.Day4
{
    public class Part1Matcher : SecureContainerMatcher
    {
        /// <inheritdoc />
        public override bool DoesntDecrease (byte [] number)
        {
            for (var i = 0; i < number.Length - 1; i++)
                if (number [i + 1] < number [i])
                    return false;

            return true;
        }

        /// <inheritdoc />
        public override bool HasDouble (byte [] number)
        {
            for (var i = 0; i < number.Length - 1; i++)
                if (number [i + 1] == number [i])
                    return true;

            return false;
        }
    }
}