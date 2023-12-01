using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day05
{
    public class Solution : PuzzleBase<IEnumerable<Line>>
    {
        public Solution(string inputFile) : base(inputFile)
        {
        }

        public override object SolvePart1()
        {
            Func<Line, bool> check = coord => coord.Start.X != coord.End.X && coord.Start.Y != coord.End.Y;
            var map = AddCoordinatesToMap(Input, check);
            return map.Count(x => x.Value >= 2);
        }

        public override object SolvePart2()
        {
            var map = AddCoordinatesToMap(Input, coord => false);
            return map.Count(x => x.Value >= 2);
        }

        private Dictionary<Point, int> AddCoordinatesToMap(
            IEnumerable<Line> coords,
            Func<Line, bool> extraCheck)
        {
            var map = new Dictionary<Point, int>();

            foreach (var coord in coords)
            {
                if (extraCheck(coord))
                    continue;

                var yDiff = Math.Sign(coord.End.Y - coord.Start.Y);
                var xDiff = Math.Sign(coord.End.X - coord.Start.X);

                for (int x = coord.Start.X, y = coord.Start.Y;
                     x != coord.End.X + xDiff || y != coord.End.Y + yDiff;
                     x += xDiff, y += yDiff)
                {
                    if (map.ContainsKey(new Point(x, y)))
                        map[new Point(x, y)]++;
                    else
                        map.Add(new Point(x, y), 1);
                }
            }

            return map;
        }

        public sealed override IEnumerable<Line> ParseInput(string inputFile) =>
            DataReader.ReadLinesFromFile(inputFile)
                .Select(x => x.Split(" -> "))
                .Select(CreateLine);

        private Line CreateLine(string[] input)
        {
            var point1 = new Point(int.Parse(input[0].Split(',')[0]), int.Parse(input[0].Split(',')[1]));
            var point2 = new Point(int.Parse(input[1].Split(',')[0]), int.Parse(input[1].Split(',')[1]));
            return new Line(point1, point2);
        }
    }
}