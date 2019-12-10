namespace AdventOfCode.Day4
{
    public static class Part2
    {
        public static int Solve ()
            => Part1.CountPossibilities (Data.Minimum, Data.Maximum, new Part2Matcher ());
    }
}