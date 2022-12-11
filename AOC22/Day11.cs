using System.Text.RegularExpressions;

namespace AOC22
{
    public static class Day11
    {
        const string _inputFilePath = "../../../inputd11.txt";
        const string _monkeySeparator = "\n\n";
        const string _lineSeparator = "\n";
        const string _itemSeparator = ", ";

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);
            var monkeyInputList = input.Split(_monkeySeparator).Where(x => !string.IsNullOrEmpty(x)).ToList();

            Console.WriteLine($"D11 PT1: {Perform(20, monkeyInputList, true)}");
            Console.WriteLine($"D11 PT2: {Perform(10000, monkeyInputList, false)}");

            long Perform(int numberOfRounds, List< string> monkeyInputList, bool useNormalRelief)
            {
                var monkeyList = new List<Monkey>();

                foreach (var monkeyInput in monkeyInputList)
                {
                    var monkeyInfo = monkeyInput.Split(_lineSeparator);

                    monkeyList.Add(new Monkey(
                        id: Convert.ToInt32(monkeyInfo[0][7].ToString()),
                        operationChar: monkeyInfo[2][23],
                        operationValue: Regex.IsMatch(monkeyInfo[2], "\\d") ? Convert.ToInt64(monkeyInfo[2][25..]) : 0,
                        divisibleValue: Convert.ToInt64(monkeyInfo[3][21..]),
                        monkeyIdToPassIfTrue: Convert.ToInt32(monkeyInfo[4][29].ToString()),
                        monkeyIdToPassIfFalse: Convert.ToInt32(monkeyInfo[5][30].ToString()),
                        holdedItems: Array.ConvertAll(monkeyInfo[1][18..]
                                        .Split(_itemSeparator), s => long.Parse(s))
                                        .Select(x => new Item(x)).ToList()
                        ));
                }

                var superModulo = monkeyList.Select(m => m.DivisibleValue).Aggregate((m, i) => m * i);

                for (int i = 0; i < numberOfRounds; i++)
                {
                    foreach (var monkey in monkeyList)
                    {
                        for (int j = 0; j < monkey.HoldedItems.Count; j++)
                        {
                            monkey.HoldedItems[j].StressLevel = monkey.ManipulateItem(monkey.HoldedItems[j].StressLevel, useNormalRelief, superModulo);

                            if (monkey.HoldedItems[j].StressLevel % monkey.DivisibleValue == 0)
                            {
                                monkeyList[monkey.MonkeyIdToPassIfTrue].HoldedItems.Add(monkey.HoldedItems[j]);
                            }
                            else
                            {
                                monkeyList[monkey.MonkeyIdToPassIfFalse].HoldedItems.Add(monkey.HoldedItems[j]);
                            }

                            monkey.NumberOfPasses++;
                        }

                        monkey.HoldedItems.Clear();
                    }
                }

                return monkeyList.OrderByDescending(x => x.NumberOfPasses).Take(2).Select(m => m.NumberOfPasses).Aggregate((m, i) => m * i);
            }
        }
    }

    public class Monkey
    {
        public Monkey(int id, char operationChar, long operationValue, long divisibleValue, int monkeyIdToPassIfTrue, int monkeyIdToPassIfFalse, List<Item> holdedItems)
        {
            Id = id;
            OperationChar = operationChar;
            OperationValue = operationValue;
            DivisibleValue = divisibleValue;
            MonkeyIdToPassIfTrue = monkeyIdToPassIfTrue;
            MonkeyIdToPassIfFalse = monkeyIdToPassIfFalse;
            HoldedItems = new List<Item>(holdedItems);
        }

        public int Id { get; set; }
        public char OperationChar { get; set; }
        public long OperationValue { get; set; } //if 0 then old value
        public long DivisibleValue { get; set; }
        public int MonkeyIdToPassIfTrue { get; set; }
        public int MonkeyIdToPassIfFalse { get; set; }
        public long NumberOfPasses { get; set; }
        public List<Item> HoldedItems { get; set; }

        public long ManipulateItem(long oldValue, bool useNormalRelief, long superModulo)
        {
            long temporaryValue = OperationValue == 0 ? oldValue : OperationValue;

            if (useNormalRelief)
            {
                return OperationChar switch
                {
                    '+' => Convert.ToInt64(Math.Floor((decimal)(oldValue + temporaryValue) / 3)),
                    '*' => Convert.ToInt64(Math.Floor((decimal)(oldValue * temporaryValue) / 3)),
                    _ => 0,
                };
            }

            return OperationChar switch
            {
                '+' => Convert.ToInt64(Math.Floor((decimal)(oldValue + temporaryValue) % superModulo)),
                '*' => Convert.ToInt64(Math.Floor((decimal)(oldValue * temporaryValue) % superModulo)),
                _ => 0,
            };
        }
    }

    public class Item
    {
        public Item(long stressLevel)
        {
            StressLevel = stressLevel;
        }

        public long StressLevel { get; set; }
    }
}