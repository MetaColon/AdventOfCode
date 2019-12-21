namespace AdventOfCode.Day04
{
    public abstract class SecureContainerMatcher : Matcher
    {
        /// <inheritdoc />
        public bool MeetsCriteria (byte [] number) => DoesntDecrease (number) && HasDouble (number);

        public abstract bool DoesntDecrease (byte [] number);
        public abstract bool HasDouble (byte [] number);
    }
}