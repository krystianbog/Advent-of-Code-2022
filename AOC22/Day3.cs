namespace AOC22
{
    public static class Day3
    {
        const int _upperCharCalculatorAdditionalValue = 38;
        const int _lowerCharCalculatorAdditionalValue = 96;
        const string _inputFilePath = "../../../inputd3.txt";
        const string _separator = "\n";
        const int _backpackGroupSize = 3;

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);
            var backpackContentList = input.Split(_separator).Where(x => !string.IsNullOrEmpty(x)).ToList();

            int prioritySum = 0;

            foreach (var backpackContent in backpackContentList)
            {
                var firstCompartment = backpackContent.Substring(0, backpackContent.Length / 2);

                var secondCompartment = backpackContent.Substring(backpackContent.Length / 2);

                var commonValues = firstCompartment.Intersect(secondCompartment);

                var priority = commonValues.Sum(x => ValuePriorityCalculator(x));

                prioritySum += priority;
            }

            Console.WriteLine($"D3 PT1: {prioritySum}");

            int priorityGroupedSum = 0;

            int backpackContentGroupsCount = backpackContentList.Count / _backpackGroupSize;

            for (int i = 0; i < backpackContentGroupsCount; i++)
            {
                var backpackContentListGroup = backpackContentList.Take(_backpackGroupSize).ToList();

                var commonValues = new List<char>();

                for (int j = 0; j < backpackContentListGroup.Count - 1; j++)
                {
                    if (commonValues.Any())
                    {
                        commonValues = commonValues.Intersect(backpackContentListGroup[j + 1]).ToList();
                    }
                    else
                    {
                        commonValues = backpackContentListGroup[j].Intersect(backpackContentListGroup[j + 1]).ToList();
                    }
                }

                int priority = ValuePriorityCalculator(commonValues.First());

                priorityGroupedSum += priority;

                backpackContentList.RemoveRange(0, _backpackGroupSize);
            }

            Console.WriteLine($"D3 PT2: {priorityGroupedSum}");

            int ValuePriorityCalculator(char input)
            {
                if (Char.IsUpper(input))
                {
                    return input - _upperCharCalculatorAdditionalValue;
                }
                else
                {
                    return input - _lowerCharCalculatorAdditionalValue;
                }
            }
        }
    }
}
