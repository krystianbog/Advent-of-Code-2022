namespace AOC22
{
    public static class Day8
    {
        const string _inputFilePath = "../../../inputd8.txt";
        const string _inputLineSeparator = "\n";

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);
            var inputList = input.Split(_inputLineSeparator).Where(x => !string.IsNullOrEmpty(x)).ToList();

            var treeList = new List<Tree>();
            int coordinateRowCounter = 0;

            foreach (var treeLine in inputList)
            {
                int coordinateColumnCounter = 0;

                foreach (var tree in treeLine)
                {
                    treeList.Add(new Tree(coordinateRowCounter, coordinateColumnCounter, Convert.ToInt32(tree.ToString())));

                    coordinateColumnCounter++;
                }

                coordinateRowCounter++;
            }

            //PT1
            foreach (var tree in treeList)
            {
                bool obstructedViewFromLeft = treeList
                    .Where(x => x.CoordinateColumn < tree.CoordinateColumn && x.CoordinateRow == tree.CoordinateRow)
                    .Any(x => x.Height >= tree.Height);

                bool obstructedViewFromUp = treeList
                    .Where(x => x.CoordinateColumn == tree.CoordinateColumn && x.CoordinateRow < tree.CoordinateRow)
                    .Any(x => x.Height >= tree.Height);

                bool obstructedViewFromRight = treeList
                    .Where(x => x.CoordinateColumn > tree.CoordinateColumn && x.CoordinateRow == tree.CoordinateRow)
                    .Any(x => x.Height >= tree.Height);

                bool obstructedViewFromDown = treeList
                    .Where(x => x.CoordinateColumn == tree.CoordinateColumn && x.CoordinateRow > tree.CoordinateRow)
                    .Any(x => x.Height >= tree.Height);

                if (obstructedViewFromLeft && obstructedViewFromUp && obstructedViewFromRight && obstructedViewFromDown)
                {
                    tree.IsVisibleFromOutside = false;
                }
            }

            Console.WriteLine($"D8 PT1: {treeList.Count(x => x.IsVisibleFromOutside)}");

            //PT2
            foreach (var tree in treeList)
            {
                bool leftReached = false;
                int counterLeft = 1;
                while (!leftReached)
                {
                    if (tree.CoordinateColumn == 0)
                    {
                        tree.ViewDistanceLeft = 0;
                        break;
                    }

                    var searchTree = treeList.FirstOrDefault(x => x.CoordinateRow == tree.CoordinateRow && x.CoordinateColumn == tree.CoordinateColumn - counterLeft);

                    if (searchTree == null)
                    {
                        leftReached = true;
                    }
                    else
                    {
                        if (searchTree.Height < tree.Height)
                        {
                            counterLeft++;
                            tree.ViewDistanceLeft++;
                        }
                        else
                        {
                            tree.ViewDistanceLeft++;
                            leftReached = true;
                        }
                    }
                }

                bool upReached = false;
                int counterUp = 1;
                while (!upReached)
                {
                    if (tree.CoordinateRow == 0)
                    {
                        tree.ViewDistanceUp = 0;
                        break;
                    }

                    var searchTree = treeList.FirstOrDefault(x => x.CoordinateRow == tree.CoordinateRow - counterUp && x.CoordinateColumn == tree.CoordinateColumn);

                    if (searchTree == null)
                    {
                        upReached = true;
                    }
                    else
                    {
                        if (searchTree.Height < tree.Height)
                        {
                            counterUp++;
                            tree.ViewDistanceUp++;
                        }
                        else
                        {
                            tree.ViewDistanceUp++;
                            upReached = true;
                        }
                    }
                }

                bool rightReached = false;
                int counterRight = 1;
                while (!rightReached)
                {
                    if (tree.CoordinateColumn == 98)
                    {
                        tree.ViewDistanceRight = 0;
                        break;
                    }

                    var searchTree = treeList.FirstOrDefault(x => x.CoordinateRow == tree.CoordinateRow && x.CoordinateColumn == tree.CoordinateColumn + counterRight);

                    if (searchTree == null)
                    {
                        rightReached = true;
                    }
                    else
                    {
                        if (searchTree.Height < tree.Height)
                        {
                            counterRight++;
                            tree.ViewDistanceRight++;
                        }
                        else
                        {
                            tree.ViewDistanceRight++;
                            rightReached = true;
                        }
                    }
                }

                bool downReached = false;
                int counterDown = 1;
                while (!downReached)
                {
                    if (tree.CoordinateRow == 98)
                    {
                        tree.ViewDistanceDown = 0;
                        break;
                    }

                    var searchTree = treeList.FirstOrDefault(x => x.CoordinateRow == tree.CoordinateRow + counterDown && x.CoordinateColumn == tree.CoordinateColumn);

                    if (searchTree == null)
                    {
                        downReached = true;
                    }
                    else
                    {
                        if (searchTree.Height < tree.Height)
                        {
                            counterDown++;
                            tree.ViewDistanceDown++;
                        }
                        else
                        {
                            tree.ViewDistanceDown++;
                            downReached = true;
                        }
                    }
                }

                tree.ScenicScore = tree.ViewDistanceLeft * tree.ViewDistanceUp * tree.ViewDistanceRight * tree.ViewDistanceDown;
            }

            Console.WriteLine($"D8 PT2: {treeList.OrderByDescending(x => x.ScenicScore).First().ScenicScore}");
        }
    }

    public class Tree
    {
        public Tree(int coordinateRow, int coordinateColumn, int height)
        {
            CoordinateRow = coordinateRow;
            CoordinateColumn = coordinateColumn;
            Height = height;
            IsVisibleFromOutside = true;
        }

        public int CoordinateRow { get; set; }
        public int CoordinateColumn { get; set; }
        public int Height { get; set; }
        public bool IsVisibleFromOutside { get; set; }
        public int ViewDistanceLeft { get; set; }
        public int ViewDistanceUp { get; set; }
        public int ViewDistanceRight { get; set; }
        public int ViewDistanceDown { get; set; }
        public int ScenicScore { get; set; }
    }
}
