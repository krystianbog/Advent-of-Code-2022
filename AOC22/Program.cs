#region D1
//var input = File.ReadAllText("../../../inputd1.txt");
//var inputList = input.Split("\n\n").ToList();
//int max = 0;
//var sumList = new List<int>();

//foreach (var line in inputList)
//{
//    var values = line.Split("\n").ToList();
//    values.RemoveAll(x => x == string.Empty);
//    var valuesInt = new List<int>();
//    values.ForEach(x => valuesInt.Add(Convert.ToInt32(x)));
//    var sum = valuesInt.Sum();
//    sumList.Add(sum);
//    if (sum > max) max = sum;
//}

//sumList = sumList.OrderByDescending(x => x).ToList();
//var t3 = sumList.Take(3);
//var t3sum = t3.Sum();

//Console.WriteLine($"PT1: {max}");
//Console.WriteLine($"PT2: {t3sum}");
#endregion

#region D2
/* PT1
    Win =         6 points
    Draw =        3 point

    A = Rock      1 point
    B = Paper     2 points
    C = Scissors  3 points

    X = Rock      
    Y = Paper
    Z = Scissors
*/
/* PT2
    Win =         6 points
    Draw =        3 point

    A = Rock      1 point
    B = Paper     2 points
    C = Scissors  3 points

    X = Lose      
    Y = Draw
    Z = Win
*/
//var input = File.ReadAllText("../../../inputd2.txt");

//var inputList = input.Split("\n").ToList();
//inputList.RemoveAll(x => x == string.Empty);

//var scorePT1 = 0;
//var scorePT2 = 0;

//foreach (var item in inputList)
//{
//    var oponent = char.Parse(item.Substring(0, 1));
//    var me = char.Parse(item.Substring(item.Length - 1));
//    var resultPT1 = PlayPT1(oponent, me);
//    scorePT1 += resultPT1;
//    var resultPT2 = PlayPT2(oponent, me);
//    scorePT2 += resultPT2;
//}

//Console.WriteLine($"PT1: {scorePT1}");
//Console.WriteLine($"PT2: {scorePT2}");

//int PlayPT1(char oponent, char me)
//{
//    switch (oponent)
//    {
//        case 'A': //Rock
//            switch (me)
//            {
//                case 'X'://Rock
//                    return 4;
//                case 'Y'://Paper
//                    return 8;
//                case 'Z'://Scissors
//                    return 3;
//            }
//            break;
//        case 'B': //Paper
//            switch (me)
//            {
//                case 'X'://Rock
//                    return 1;
//                case 'Y'://Paper
//                    return 5;
//                case 'Z'://Scissors
//                    return 9;
//            }
//            break;
//        case 'C'://Scissors
//            switch (me)
//            {
//                case 'X'://Rock
//                    return 7;
//                case 'Y'://Paper
//                    return 2;
//                case 'Z'://Scissors
//                    return 6;
//            }
//            break;
//        default:
//            return 0;
//    }

//    return 0;
//}
//int PlayPT2(char oponent, char me)
//{
//    switch (oponent)
//    {
//        case 'A': //Rock
//            switch (me)
//            {
//                case 'X'://Lose
//                    return 3;
//                case 'Y'://Draw
//                    return 4;
//                case 'Z'://Win
//                    return 8;
//            }
//            break;
//        case 'B': //Paper
//            switch (me)
//            {
//                case 'X'://Lose
//                    return 1;
//                case 'Y'://Draw
//                    return 5;
//                case 'Z'://Win
//                    return 9;
//            }
//            break;
//        case 'C'://Scissors
//            switch (me)
//            {
//                case 'X'://Lose
//                    return 2;
//                case 'Y'://Draw
//                    return 6;
//                case 'Z'://Win
//                    return 7;
//            }
//            break;
//        default:
//            return 0;
//    }

//    return 0;
//}
#endregion

Console.WriteLine("FIN");