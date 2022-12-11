namespace AOC22
{
    public static class Day10
    {
        const string _inputFilePath = "../../../inputd10.txt";
        const string _inputLineSeparator = "\n";

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);
            var inputList = input.Split(_inputLineSeparator).Where(x => !string.IsNullOrEmpty(x)).ToList();

            var clock = new Clock();
            var cyclesToCheck = new List<int> { 20, 60, 100, 140, 180, 220 };
            int sumInCycles = 0;
            var renderPixelList = new List<char>();

            foreach (var operation in inputList)
            {
                if (operation.Contains("noop"))
                {
                    PerformCycle(clock, 1, 0);
                }

                if (operation.Contains("addx"))
                {
                    int value = Convert.ToInt32(operation[5..]);
                    PerformCycle(clock, 2, value);
                }
            }

            Console.WriteLine($"D10 P1: {sumInCycles}");
            Console.WriteLine($"D10 P2: ");

            for (int i = 1; i < renderPixelList.Count + 1; i++)
            {
                Console.Write(renderPixelList[i - 1]);

                if (i % 40 == 0)
                {
                    Console.WriteLine();
                }
            }

            void PerformCycle(Clock clock, int cycleNumber, int value)
            {
                for (int i = 0; i < cycleNumber; i++)
                {
                    if (cyclesToCheck.Contains(clock.Cycle))
                    {
                        sumInCycles += (clock.Cycle * clock.Value);
                    }

                    if (Math.Abs(clock.Value - clock.Cycle % 40) <= 1)
                    {
                        renderPixelList.Add('#');
                    }
                    else
                    {
                        renderPixelList.Add('.');
                    }

                    clock.Cycle++;
                }

                clock.Value += value;
            }
        }
    }

    public class Clock
    {
        public int Cycle { get; set; }
        public int Value { get; set; } = 1;
    }
}
