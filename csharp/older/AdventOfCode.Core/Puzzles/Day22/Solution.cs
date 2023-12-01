using System.Text.RegularExpressions;
using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day22;

public class Solution : PuzzleBase<List<Cube>>
{
    private readonly string _inputRegex = @"(\w+) x=(-?\d+)..(-?\d+),y=(-?\d+)..(-?\d+),z=(-?\d+)..(-?\d+)";

    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1() =>
        CreateCubeList(FilterCubeList(Input, -50, 50, -50, 50, -50, 50)).Count;

    public override object SolvePart2()
    {
        Dictionary<(int minX, int maxX, int minY, int maxY, int minZ, int maxZ), long> cubes = new();

        foreach (var cube in Input)
        {
            var (minX, maxX, minY, maxY, minZ, maxZ, turnOn) = cube;
            long newSign = turnOn ? 1 : -1;
            var newCuboid = (minX, maxX, minY, maxY, minZ, maxZ);

            Dictionary<(int minX, int maxX, int minY, int maxY, int minZ, int maxZ), long> newCuboids = new();

            foreach (var kvp in cubes)
            {
                var (minX2, maxX2, minY2, maxY2, minZ2, maxZ2) = kvp.Key;
                var curSign = kvp.Value;

                var tmpMinX = Math.Max(minX, minX2);
                var tmpMaxX = Math.Min(maxX, maxX2);
                var tmpMinY = Math.Max(minY, minY2);
                var tmpMaxY = Math.Min(maxY, maxY2);
                var tmpMinZ = Math.Max(minZ, minZ2);
                var tmpMaxZ = Math.Min(maxZ, maxZ2);

                var tmpCuboid = (tmpMinX, tmpMaxX, tmpMinY, tmpMaxY, tmpMinZ, tmpMaxZ);
                
                if (tmpMinX <= tmpMaxX && tmpMinY <= tmpMaxY && tmpMinZ <= tmpMaxZ)
                    newCuboids[tmpCuboid] = newCuboids.GetValueOrDefault(tmpCuboid, 0) - curSign;
            }

            if (newSign == 1) 
                newCuboids[newCuboid] = newCuboids.GetValueOrDefault(newCuboid, 0) + newSign;

            foreach (var (key, value) in newCuboids)
                cubes[key] = cubes.GetValueOrDefault(key, 0) + value;
        }
        
        return cubes.Sum(a => (a.Key.maxX - a.Key.minX + 1L) * (a.Key.maxY - a.Key.minY + 1) * (a.Key.maxZ - a.Key.minZ + 1) * a.Value);
    }


    public HashSet<(int, int, int)> CreateCubeList(IEnumerable<Cube> cubes)
    {
        HashSet<(int, int, int)> cubeList = new();
        foreach (var cubeAction in cubes)
        {
            for (var x = cubeAction.MinX; x <= cubeAction.MaxX; x++)
            for (var y = cubeAction.MinY; y <= cubeAction.MaxY; y++)
            for (var z = cubeAction.MinZ; z <= cubeAction.MaxZ; z++)
            {
                if (cubeAction.State)
                    cubeList.Add((x, y, z));
                else if (cubeList.Contains((x, y, z)))
                    cubeList.Remove((x, y, z));
            }
        }

        return cubeList;
    }

    private Cube? Intersect(Cube a, Cube b, bool on)
    {
        if (a.MinX > b.MaxX || a.MaxX < b.MinX || a.MinY > b.MaxY || a.MaxY < b.MinY || a.MinZ > b.MaxZ ||
            a.MaxZ < b.MinZ)
        {
            return null;
        }

        return new Cube(
            Math.Max(a.MinX, b.MinX),
            Math.Min(a.MaxX, b.MaxX),
            Math.Max(a.MinY, b.MinY),
            Math.Min(a.MaxY, b.MaxY),
            Math.Max(a.MinZ, b.MinZ),
            Math.Min(a.MaxZ, b.MaxZ),
            true);
    }

    public IEnumerable<Cube> FilterCubeList(List<Cube> cubes,
        int minX, int maxX,
        int minY, int maxY,
        int minZ, int maxZ) =>
        cubes.Where(x =>
            x.MinX >= minX && x.MaxX <= maxX &&
            x.MinY >= minY && x.MaxY <= maxY &&
            x.MinZ >= minZ && x.MaxZ <= maxZ);

    public override List<Cube> ParseInput(string inputFile) =>
        File.ReadLines(inputFile)
            .Select(x => Regex.Split(x, _inputRegex))
            .Select(x =>
            {
                var state = x[1] == "on" ? true : false;
                var minX = int.Parse(x[2]);
                var maxX = int.Parse(x[3]);
                var minY = int.Parse(x[4]);
                var maxY = int.Parse(x[5]);
                var minZ = int.Parse(x[6]);
                var maxZ = int.Parse(x[7]);

                return new Cube(minX, maxX, minY, maxY, minZ, maxZ, state);
            })
            .ToList();
}