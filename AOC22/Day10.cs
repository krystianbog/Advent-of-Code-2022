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


        }
    }
}
