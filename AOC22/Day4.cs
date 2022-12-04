namespace AOC22
{
    public static class Day4
    {
        const string _inputFilePath = "../../../inputd4.txt";
        const string _inputLineSeparator = "\n";
        const string _pairsSplitValue = ",";
        const string _individualSplitValue = "-";

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);
            var inputList = input.Split(_inputLineSeparator).Where(x => !string.IsNullOrEmpty(x)).ToList();

            int counterFullyContains = 0;
            int counterAnyOverlaps = 0;

            foreach (var elvesPair in inputList)
            {
                var elvesPairArray = elvesPair.Split(_pairsSplitValue);

                var firstElfRange = GetRange(elvesPairArray[0]);
                var secondElfRange = GetRange(elvesPairArray[1]);

                if (firstElfRange.All(secondElfRange.Contains) || secondElfRange.All(firstElfRange.Contains))
                {
                    counterFullyContains++;
                }

                if (firstElfRange.Intersect(secondElfRange).Any() || secondElfRange.Intersect(firstElfRange).Any())
                {
                    counterAnyOverlaps++;
                }
            }

            Console.WriteLine($"D4 PT1: {counterFullyContains}");
            Console.WriteLine($"D4 PT2: {counterAnyOverlaps}");
        }

        private static List<int> GetRange(string elfSectionIDs)
        {
            var rangeIndicators = Array.ConvertAll(elfSectionIDs.Split(_individualSplitValue), int.Parse).Distinct().ToArray();

            if (rangeIndicators.Length == 1)
            {
                return new List<int> { rangeIndicators[0] };
            }
            else
            {
                return Enumerable.Range(rangeIndicators[0], rangeIndicators[1] + 1 - rangeIndicators[0]).ToList();
            }
        }
    }
}
