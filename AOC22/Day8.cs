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
            int coordinateXCounter = 0;

            foreach (var treeLine in inputList)
            {
                int coordinateYCounter = 0;

                foreach (var tree in treeLine)
                {
                    treeList.Add(new Tree(coordinateXCounter, coordinateYCounter, Convert.ToInt32(tree.ToString())));

                    coordinateYCounter++;
                }

                coordinateXCounter++;
            }

            //PT1
            foreach (var tree in treeList)
            {
                bool obstructedViewFromLeft = treeList
                    .Where(x => x.CoordinateX < tree.CoordinateX && x.CoordinateY == tree.CoordinateY)
                    .Any(x => x.Height >= tree.Height);

                bool obstructedViewFromUp = treeList
                    .Where(x => x.CoordinateX == tree.CoordinateX && x.CoordinateY < tree.CoordinateY)
                    .Any(x => x.Height >= tree.Height);

                bool obstructedViewFromRight = treeList
                    .Where(x => x.CoordinateX > tree.CoordinateX && x.CoordinateY == tree.CoordinateY)
                    .Any(x => x.Height >= tree.Height);

                bool obstructedViewFromDown = treeList
                    .Where(x => x.CoordinateX == tree.CoordinateX && x.CoordinateY > tree.CoordinateY)
                    .Any(x => x.Height >= tree.Height);

                if (obstructedViewFromLeft && obstructedViewFromUp && obstructedViewFromRight && obstructedViewFromDown)
                {
                    tree.IsVisibleFromOutside = false;
                }
            }

            //PT2
            foreach (var tree in treeList)
            {
                bool leftReached = false;
                int counterLeft = 1;
                while (!leftReached)
                {//todo if na wysokość
                    var searchTree = treeList.FirstOrDefault(x => x.CoordinateX == tree.CoordinateX - counterLeft && x.CoordinateY == tree.CoordinateY);
                    if (searchTree != null)
                    {
                        counterLeft++;
                        tree.ViewDistanceLeft++;
                    }
                    else
                    {
                        leftReached = true;
                    }
                    if (tree.CoordinateX == 0) tree.ViewDistanceLeft = 0;
                }

                bool upReached = false;
                int counterUp = 1;
                while (!upReached)
                {
                    var searchTree = treeList.FirstOrDefault(x => x.CoordinateX == tree.CoordinateX && x.CoordinateY == tree.CoordinateY - counterUp);
                    if (searchTree != null)
                    {
                        counterUp++;
                        tree.ViewDistanceUp++;
                    }
                    else
                    {
                        upReached = true;
                    }
                    if (tree.CoordinateY == 0) tree.ViewDistanceUp = 0;
                }

                bool rightReached = false;
                int counterRight = 1;
                while (!rightReached)
                {
                    var searchTree = treeList.FirstOrDefault(x => x.CoordinateX == tree.CoordinateX + counterRight && x.CoordinateY == tree.CoordinateY);
                    if (searchTree != null)
                    {
                        counterRight++;
                        tree.ViewDistanceRight++;
                    }
                    else
                    {
                        rightReached = true;
                    }
                    if (tree.CoordinateX == 98) tree.ViewDistanceRight = 0;
                }

                bool downReached = false;
                int counterDown = 1;
                while (!downReached)
                {
                    var searchTree = treeList.FirstOrDefault(x => x.CoordinateX == tree.CoordinateX && x.CoordinateY == tree.CoordinateY + counterDown);
                    if (searchTree != null)
                    {
                        counterDown++;
                        tree.ViewDistanceDown++;
                    }
                    else
                    {
                        downReached = true;
                    }
                    if (tree.CoordinateY == 98) tree.ViewDistanceDown = 0;
                }

                tree.ScenicScore = tree.ViewDistanceLeft * tree.ViewDistanceUp * tree.ViewDistanceRight * tree.ViewDistanceDown;
            }

            Console.WriteLine($"D8 PT1: {treeList.Count(x => x.IsVisibleFromOutside)}");
            Console.WriteLine($"D8 PT2: {treeList.OrderByDescending(x => x.ScenicScore).First().ScenicScore}");
        }
    }

    public class Tree
    {
        public Tree() { }
        public Tree(int coordinateX, int coordinateY, int height)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            Height = height;
            IsVisibleFromOutside = true;
        }

        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public int Height { get; set; }
        public bool IsVisibleFromOutside { get; set; }
        public int ViewDistanceLeft { get; set; }
        public int ViewDistanceUp { get; set; }
        public int ViewDistanceRight { get; set; }
        public int ViewDistanceDown { get; set; }
        public int ScenicScore { get; set; }
    }
}
