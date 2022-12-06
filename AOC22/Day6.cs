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

            SearchForFirstOccurance(1, input, _startOfPacketMarker);
            SearchForFirstOccurance(2, input, _messageMarker);
        }

        static void SearchForFirstOccurance(int partNumber, string input, int uniqueMarker)
        {
            for (int i = uniqueMarker; i < input.Length; i++)
            {
                var substringInput = input.Substring(i - uniqueMarker, uniqueMarker);

                if (substringInput.All(x => substringInput.Count(y => x == y) == 1))
                {
                    Console.WriteLine($"D6 PT{partNumber}: {i}");

                    break;
                }
            }
        }
    }
}
