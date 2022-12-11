namespace AOC22
{
    public static class Day9
    {
        const string _inputFilePath = "../../../inputd9.txt";
        const string _inputLineSeparator = "\n";

        public static void Run()
        {
            var input = File.ReadAllText(_inputFilePath);
            var inputList = input.Split(_inputLineSeparator).Where(x => !string.IsNullOrEmpty(x)).ToList();

            var motionList = new List<Motion>();

            foreach (var motionInput in inputList)
            {
                motionList.Add(new Motion
                (
                    motionInput[..1][0],
                    Convert.ToInt32(motionInput[2..])
                ));
            }

            //Perform(motionList, 2);
            Perform(motionList, 10);//todo
        }

        private static void Perform(List<Motion> motionList, int numberOfKnots)
        {
            var ropeKnots = new List<RopeKnot>();

            for (int i = 0; i < numberOfKnots; i++)
            {
                ropeKnots.Add(new RopeKnot());
            }

            var positionsVisitedByLastKnot = new List<Position>();

            foreach (var motion in motionList)
            {
                for (int i = 0; i < motion.Value; i++)
                {
                    for (int j = 0; j < ropeKnots.Count; j++)
                    {
                        if (j == 0) //head zawsze się porusza
                        {
                            if (motion.Direction == 'L')
                            {
                                ropeKnots[j].CoordinateX--;
                            }
                            if (motion.Direction == 'U')
                            {
                                ropeKnots[j].CoordinateY++;
                            }
                            if (motion.Direction == 'R')
                            {
                                ropeKnots[j].CoordinateX++;
                            }
                            if (motion.Direction == 'D')
                            {
                                ropeKnots[j].CoordinateY--;
                            }
                        }
                        else
                        {
                            int changeInX = Math.Abs(ropeKnots[j - 1].CoordinateX - ropeKnots[j].CoordinateX);
                            int changeInY = Math.Abs(ropeKnots[j - 1].CoordinateY - ropeKnots[j].CoordinateY);

                            if (changeInX == 0 && changeInY > 1)//ruch tylko w pionie
                            {
                                if (ropeKnots[j - 1].CoordinateY > ropeKnots[j].CoordinateY)//poprzedni jest wyżej
                                {
                                    ropeKnots[j].CoordinateY++;
                                }
                                if (ropeKnots[j - 1].CoordinateY < ropeKnots[j].CoordinateY)//poprzedni jest niżej
                                {
                                    ropeKnots[j].CoordinateY--;
                                }
                            }
                            if (changeInX > 1 && changeInY == 0)//ruch tylko w poziomie
                            {
                                if (ropeKnots[j - 1].CoordinateX > ropeKnots[j].CoordinateX)//poprzedni jest w prawo
                                {
                                    ropeKnots[j].CoordinateX++;
                                }
                                if (ropeKnots[j - 1].CoordinateX < ropeKnots[j].CoordinateX)//poprzedni jest w lewo
                                {
                                    ropeKnots[j].CoordinateX--;
                                }
                            }
                            if ((changeInX == 1 && changeInY > 1) || (changeInX > 1 && changeInY == 1))//ruch w skos
                            {
                                if (ropeKnots[j - 1].CoordinateY > ropeKnots[j].CoordinateY &&
                                    ropeKnots[j - 1].CoordinateX < ropeKnots[j].CoordinateX)//poprzedni jest wyżej w lewo
                                {
                                    ropeKnots[j].CoordinateY++;
                                    ropeKnots[j].CoordinateX--;
                                }
                                if (ropeKnots[j - 1].CoordinateY > ropeKnots[j].CoordinateY &&
                                    ropeKnots[j - 1].CoordinateX > ropeKnots[j].CoordinateX)//poprzedni jest wyżej w prawo
                                {
                                    ropeKnots[j].CoordinateY++;
                                    ropeKnots[j].CoordinateX++;
                                }
                                if (ropeKnots[j - 1].CoordinateY < ropeKnots[j].CoordinateY &&
                                    ropeKnots[j - 1].CoordinateX < ropeKnots[j].CoordinateX)//poprzedni jest niżej w lewo
                                {
                                    ropeKnots[j].CoordinateY--;
                                    ropeKnots[j].CoordinateX--;
                                }
                                if (ropeKnots[j - 1].CoordinateY < ropeKnots[j].CoordinateY &&
                                    ropeKnots[j - 1].CoordinateX > ropeKnots[j].CoordinateX)//poprzedni jest niżej w prawo
                                {
                                    ropeKnots[j].CoordinateY--;
                                    ropeKnots[j].CoordinateX++;
                                }
                            }

                            if (j == ropeKnots.Count - 1)
                            {
                                if (!positionsVisitedByLastKnot.Any(x => x.CoordinateX == ropeKnots[j].CoordinateX && x.CoordinateY == ropeKnots[j].CoordinateY))
                                {
                                    positionsVisitedByLastKnot.Add(new Position(ropeKnots[j].CoordinateX, ropeKnots[j].CoordinateY));
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"D9 result for {numberOfKnots} knots: {positionsVisitedByLastKnot.Count}");//5619 2376
        }
    }

    public class RopeKnot
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
    }

    public class Position
    {
        public Position(int coordinateX, int coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
        }

        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
    }

    public class Motion
    {
        public Motion(char direction, int value)
        {
            Direction = direction;
            Value = value;
        }

        public char Direction { get; set; }
        public int Value { get; set; }
    }
}
