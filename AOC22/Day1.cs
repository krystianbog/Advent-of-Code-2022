namespace AOC22
{
    public static class Day1
    {
        public static void Run()
        {
            var input = File.ReadAllText("../../../inputd1.txt");
            var inputList = input.Split("\n\n").ToList();
            int max = 0;
            var sumList = new List<int>();

            foreach (var line in inputList)
            {
                var values = line.Split("\n").ToList();
                values.RemoveAll(x => x == string.Empty);
                var valuesInt = new List<int>();
                values.ForEach(x => valuesInt.Add(Convert.ToInt32(x)));
                var sum = valuesInt.Sum();
                sumList.Add(sum);
                if (sum > max) max = sum;
            }

            var t3sum = sumList.OrderByDescending(x => x).Take(3).Sum();

            Console.WriteLine($"D1 PT1: {max}");
            Console.WriteLine($"D1 PT2: {t3sum}");
        }
    }
}
