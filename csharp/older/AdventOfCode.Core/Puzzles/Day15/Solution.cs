using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day15;

public class Solution : PuzzleBase<Dictionary<Point, int>>
{
    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1()
    {
        var (calculatedGrid, result) = MinCost(Input);
        var path = GetPath(calculatedGrid,
            new Point(calculatedGrid.Keys.MaxBy(p => p.X)!.X, calculatedGrid.Keys.MaxBy(p => p.Y)!.Y));
        return new PathFinderResult(Input, calculatedGrid, path, result);
    }

    public override object SolvePart2()
    {
        var (calculatedGrid, result) = MinCost(EnlargeGrid(Input));
        var path = GetPath(calculatedGrid,
            new Point(calculatedGrid.Keys.MaxBy(p => p.X)!.X, calculatedGrid.Keys.MaxBy(p => p.Y)!.Y));
        return new PathFinderResult(Input, calculatedGrid, path, result); 
    }

    private (Dictionary<Point, int> CalculatedGrid, int Result) MinCost(Dictionary<Point, int> grid)
    {
        var startPoint = new Point(0, 0);
        var endPoint = new Point(grid.Keys.MaxBy(p => p.X)!.X, grid.Keys.MaxBy(p => p.Y)!.Y);

        var queue = new PriorityQueue<Point, int>();
        var calculatedGrid = new Dictionary<Point, int>
        {
            [startPoint] = 0
        };

        queue.Enqueue(startPoint, 0);

        while (queue.Count > 0)
        {
            var currentPoint = queue.Dequeue();

            foreach (var nextPoint in GetNeighbours(currentPoint))
            {
                if (!grid.ContainsKey(nextPoint) || calculatedGrid.ContainsKey(nextPoint))
                    continue;

                var totalRisk = calculatedGrid[currentPoint] + grid[nextPoint];
                calculatedGrid[nextPoint] = totalRisk;
                if (nextPoint == endPoint)
                    break;

                queue.Enqueue(nextPoint, totalRisk);
            }
        }

        return (calculatedGrid, calculatedGrid[endPoint]);
    }

    private Dictionary<Point, int> EnlargeGrid(Dictionary<Point, int> grid)
    {
        var (columnCount, rowCount) = (grid.Keys.MaxBy(p => p.X)!.X + 1, grid.Keys.MaxBy(p => p.Y)!.Y + 1);

        Dictionary<Point, int> enlargedGrid = new();
        
        foreach(var column in Enumerable.Range(0, rowCount * 5))
        foreach (var row in Enumerable.Range(0, columnCount * 5))
        {
            var cellY = row % rowCount;
            var cellX = column % columnCount;
            var cellRiskLevel = grid[new Point(cellX, cellY)];
            var cellDistance = (row / rowCount) + (column / rowCount);
            var riskLevel = (cellRiskLevel + cellDistance - 1) % 9 + 1;
            enlargedGrid.Add(new Point(column, row), riskLevel);
        }

        return enlargedGrid;
    }

    private List<Point> GetPath(Dictionary<Point, int> grid, Point startPoint)
    {
        List<Point> path = new();
        var endPoint = new Point(0, 0);
        var queue = new PriorityQueue<Point, int>();
        var calculatedGrid = new Dictionary<Point, int>
        {
            [startPoint] = 0
        };

        queue.Enqueue(startPoint, 0);

        while (queue.Count > 0)
        {
            var currentPoint = queue.Dequeue();
            
            var neighbours = GetNeighbours(currentPoint);
            var lowestPoint = grid
                .Where(x => neighbours.Contains(x.Key))
                .OrderBy(x => x.Value)
                .FirstOrDefault();

            if (lowestPoint.Key == endPoint)
                break;

            path.Add(lowestPoint.Key);
            queue.Enqueue(lowestPoint.Key, 0);

            // foreach (var nextPoint in GetNeighbours(currentPoint))
            // {
            //     if (!grid.ContainsKey(nextPoint) || calculatedGrid.ContainsKey(nextPoint))
            //         continue;
            //
            //     var totalRisk = calculatedGrid[currentPoint] + grid[nextPoint];
            //     calculatedGrid[nextPoint] = totalRisk;
            //     if (nextPoint == endPoint)
            //         break;
            //
            //     queue.Enqueue(nextPoint, totalRisk);
            // }
        }

        path.Reverse();
        return path;
        // List<Point> path = new();
        //
        // var endPoint = new Point(0, 0);
        //
        // for (int column = startPoint.X; column > 0; column--)
        // {
        //     for (int row = startPoint.Y; row > 0; row--)
        //     {
        //         var neighbours = GetNeighbours(new Point(column, row));
        //         // var points = grid
        //         //     .Where(x => x.Key.Equals(neighbours));
        //         var lowestPoint = grid
        //             .Where(x => neighbours.Contains(x.Key))
        //             .OrderBy(x => x.Value)
        //             .FirstOrDefault();
        //         
        //
        //         // var keys = grid
        //         //     .Where(x => lowestPoint.Contains(x.Key))
        //         //     .GroupBy(y => y.Value)
        //         //     .First()
        //         //     .Select(point => point.Key)
        //         //     .First();
        //         //
        //         path.Add(lowestPoint.Key);
        //
        //
        //     }
        // }
        //
        // path.Reverse();
        // return path;
    }
    
    IEnumerable<Point> GetNeighbours(Point point) =>
        new[]
        {
            point with {Y = point.Y + 1},
            point with {Y = point.Y - 1},
            point with {X = point.X + 1},
            point with {X = point.X - 1},
        };

    public sealed override Dictionary<Point, int> ParseInput(string inputFile)
    {
        var lines = DataReader.ReadLinesFromFile(inputFile);
        var grid = new Dictionary<Point, int>();
        foreach (var y in Enumerable.Range(0, lines[0].Length))
        foreach (var x in Enumerable.Range(0, lines.Length))
            grid.Add(new Point(x, y), lines[x][y] - '0');

        return grid;
    }
}