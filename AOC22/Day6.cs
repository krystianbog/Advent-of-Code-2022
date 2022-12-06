namespace AOC22
{
    public static class Day6
    {
        const string _inputFilePath = "../../../inputd6.txt";
        const int _startOfPacketMarker = 4;
        const int _messageMarker = 14;

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);

            Console.WriteLine($"D6 PT1: {SearchForFirstOccurance(input, _startOfPacketMarker)}");
            Console.WriteLine($"D6 PT2: {SearchForFirstOccurance(input, _messageMarker)}");
        }

        static int SearchForFirstOccurance(string input, int uniqueMarker)
        {
            for (int i = uniqueMarker; i < input.Length; i++)
            {
                var substringInput = input.Substring(i - uniqueMarker, uniqueMarker);

                if (substringInput.All(x => substringInput.Count(y => x == y) == 1))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
