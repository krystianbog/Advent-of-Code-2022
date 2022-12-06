using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC22
{
    public static class Day5
    {
        const string _inputFilePath = "../../../inputd5.txt";
        const string _inputLineSeparator = "\n";
        const int _craneArrangeLinesEnd = 9;
        const int _craneArrangeValueIndex = 4;
        const int _numberOfCranes = 9;
        const int _stackMovementNumberOfElementsPosition = 0;
        const int _stackMovementSourceStackIdPosition = 1;
        const int _stackMovementTargetStackIdPosition = 2;

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);
            var inputList = input.Split(_inputLineSeparator).Where(x => !string.IsNullOrEmpty(x)).ToList();

            var craneArrange = inputList.Take(_craneArrangeLinesEnd - 1).ToList();
            inputList.RemoveRange(0, _craneArrangeLinesEnd);

            var stackList = new List<Stack>();

            foreach (var level in craneArrange)
            {
                for (int i = 0; i < _numberOfCranes; i++)
                {
                    var crateIdentificator = level[_craneArrangeValueIndex * i + 1];

                    if (char.IsWhiteSpace(crateIdentificator))
                    {
                        continue;
                    }

                    if (stackList.FirstOrDefault(x => x.Id == i + 1) is null)
                    {
                        stackList.Add(new Stack(i + 1, crateIdentificator));
                    }
                    else
                    {
                        stackList.FirstOrDefault(x => x.Id == i + 1).Content.Add(crateIdentificator);
                    }
                }
            }

            var stackListPartOne = GetStackListFromArrangeInput(craneArrange);
            var stackListPartTwo = GetStackListFromArrangeInput(craneArrange);

            var stackMovements = new List<StackMovement>();

            foreach (var transferOrder in inputList)
            {
                var transferOrderDigitsInput = Regex.Replace(transferOrder, @"\D", " ");

                var transferOrderFixedDigitsInput = Regex.Replace(transferOrderDigitsInput, @"\s+", " ");

                var transferOrderNumbersList = Array.ConvertAll(transferOrderFixedDigitsInput.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray(), int.Parse);

                stackMovements.Add(new StackMovement
                {
                    NumberOfElements = transferOrderNumbersList[_stackMovementNumberOfElementsPosition],
                    SourceStackId = transferOrderNumbersList[_stackMovementSourceStackIdPosition],
                    TargetStackId = transferOrderNumbersList[_stackMovementTargetStackIdPosition]
                });
            }

            foreach (var stackMovement in stackMovements)
            {
                for (int i = 0; i < stackMovement.NumberOfElements; i++)
                {
                    var movedElement = stackListPartOne.FirstOrDefault(x => x.Id == stackMovement.SourceStackId).Content.FirstOrDefault();

                    stackListPartOne.FirstOrDefault(x => x.Id == stackMovement.SourceStackId).Content.RemoveAt(0);

                    stackListPartOne.FirstOrDefault(x => x.Id == stackMovement.TargetStackId).Content.Insert(0, movedElement);
                }

                var movedElements = stackListPartTwo.FirstOrDefault(x => x.Id == stackMovement.SourceStackId).Content.Take(stackMovement.NumberOfElements).ToList();

                stackListPartTwo.FirstOrDefault(x => x.Id == stackMovement.SourceStackId).Content.RemoveRange(0, stackMovement.NumberOfElements);

                stackListPartTwo.FirstOrDefault(x => x.Id == stackMovement.TargetStackId).Content.InsertRange(0, movedElements);
            }

            Console.WriteLine($"D5 PT1: {string.Join("", stackListPartOne.Select(x => x.Content.FirstOrDefault()))}");
            Console.WriteLine($"D5 PT2: {string.Join("", stackListPartTwo.Select(x => x.Content.FirstOrDefault()))}");
        }

        public static List<Stack> GetStackListFromArrangeInput(List<string> craneArrange)
        {
            var stackList = new List<Stack>();

            foreach (var level in craneArrange)
            {
                for (int i = 0; i < _numberOfCranes; i++)
                {
                    var crateIdentificator = level[_craneArrangeValueIndex * i + 1];

                    if (char.IsWhiteSpace(crateIdentificator))
                    {
                        continue;
                    }

                    if (stackList.FirstOrDefault(x => x.Id == i + 1) is null)
                    {
                        stackList.Add(new Stack(i + 1, crateIdentificator));
                    }
                    else
                    {
                        stackList.FirstOrDefault(x => x.Id == i + 1).Content.Add(crateIdentificator);
                    }
                }
            }

            return stackList.OrderBy(x => x.Id).ToList();
        }
    }

    public class StackMovement
    {
        public int NumberOfElements { get; set; }
        public int SourceStackId { get; set; }
        public int TargetStackId { get; set; }
    }

    public class Stack
    {
        public Stack(int id, char initialChar)
        {
            Id = id;
            Content = new List<char> { initialChar };
        }

        public int Id { get; set; }
        public List<char> Content { get; set; }
    }
}
