using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Core.Common;

namespace AdventOfCode.Core.Puzzles.Day19;

public class Solution : PuzzleBase<List<Scanner>>
{
    public Solution(string inputFile) : base(inputFile)
    {
    }

    public override object SolvePart1() => HandleScans(Input);
    public override object SolvePart2() => throw new NotImplementedException("Already answerd by part 1");

    private Result HandleScans(List<Scanner> scanners)
    {
        var visibleBeacons = scanners[0].VisibleBeacons.ToArray();
        scanners.RemoveAt(0);
        
        do
        {
            var linked = from match in scanners
                from orient in Enumerable.Range(0, 24)
                let rotated = match.VisibleBeacons.Select(s => Rotate(s, orient)).ToArray()
                from vector in visibleBeacons
                from offset in rotated.Select(s => vector - s)
                let translated = rotated.Select(s => s + offset).ToArray()
                where visibleBeacons.Intersect(translated).Count() >= 12
                select (match, translated, offset);
        
            var link = linked.First();
            visibleBeacons = visibleBeacons.Union(link.translated).ToArray();
            scanners.Remove(link.match);
        } while (scanners.Count > 0);
        
        // var distances = from s in scanners
        //     from s2 in scanners
        //     select Math.Abs(s.X - s2.X) + Math.Abs(s.Y - s2.Y) + Math.Abs(s.Z - s2.Z);
        
        var distances = 
            from s in visibleBeacons
            from s2 in visibleBeacons
            select Math.Abs(s.X - s2.X) + Math.Abs(s.Y - s2.Y) + Math.Abs(s.Z - s2.Z);

        return new Result(visibleBeacons.Count(), (long)distances.Max());

        // var queue = new Queue<Scanner>();
        // var result = new List<Vector3>();
        //
        // result.AddRange(scanners[0].VisibleBeacons);
        // queue.Enqueue(scanners[0]);
        // scanners.Remove(scanners[0]);
        //
        // while (queue.Count > 0)
        // {
        //     var scanner0 = queue.Dequeue();
        //     var linked = from match in scanners
        //         from orient in Enumerable.Range(0, 24)
        //         let rotated = match.VisibleBeacons.Select(s => Rotate(s, orient)).ToArray()
        //         from vector in scanner0.VisibleBeacons
        //         from offset in rotated.Select(s => vector - s)
        //         let translated = rotated.Select(s => s + offset).ToArray()
        //         where scanner0.VisibleBeacons.Intersect(translated).Count() >= 12
        //         select (match, translated, offset);
        //    
        //     if(!linked.Any())
        //         continue;
        //     
        //     var link = linked.First();
        //     result = result.Union(link.translated).ToList();
        //     scanners.Remove(link.match);
        //     queue.Enqueue(link.match);
        // }
        //
        // return new Result(result.Count(), 0);
        // return new Result(0, 0);

        // var scanners = new List<Vector3> {new Vector3(0, 0, 0)};
        // var visibleBeacons = scans[0].VisibleBeacons.ToArray();
        // scans.RemoveAt(0);

        // do
        // {
        //     var linked = from match in scans
        //         from orient in Enumerable.Range(0, 24)
        //         let rotated = match.VisibleBeacons.Select(s => Rotate(s, orient)).ToArray()
        //         from vector in visibleBeacons
        //         from offset in rotated.Select(s => vector - s)
        //         let translated = rotated.Select(s => s + offset).ToArray()
        //         where visibleBeacons.Intersect(translated).Count() >= 12
        //         select (match, translated, offset);
        //
        //     var link = linked.First();
        //     visibleBeacons = visibleBeacons.Union(link.translated).ToArray();
        //     scans.Remove(link.match);
        //     scanners.Add(link.offset);
        // } while (scans.Count > 0);
        //
        // var distances = from s in scanners
        //     from s2 in scanners
        //     select Math.Abs(s.X - s2.X) + Math.Abs(s.Y - s2.Y) + Math.Abs(s.Z - s2.Z);

        // return new Result(visibleBeacons.Count(), (long) distances.Max());
    }

    public override List<Scanner> ParseInput(string inputFile)
    {
        List<Scanner> scanners = new();
        ISet<Vector3> coords = new HashSet<Vector3>();
        foreach (var line in File.ReadAllLines(inputFile).Where(line => !line.StartsWith("---")))
        {
            if (string.IsNullOrEmpty(line))
            {
                scanners.Add(new Scanner(coords));
                coords = new HashSet<Vector3>();
                continue;
            }

            var split = line.Split(',');
            coords.Add(new Vector3(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
        }

        scanners.Add(new Scanner(coords));
        return scanners;
    }

    private Vector3 Rotate(Vector3 v, int direction)
    {
        var rotated = direction switch
        {
            0 => (v.X, v.Y, v.Z), 1 => (v.Y, v.Z, v.X), 2 => (-v.Y, v.X, v.Z), 3 => (-v.X, -v.Y, v.Z),
            4 => (v.Y, -v.X, v.Z), 5 => (v.Z, v.Y, -v.X), 6 => (v.Z, v.X, v.Y), 7 => (v.Z, -v.Y, v.X),
            8 => (v.Z, -v.X, -v.Y), 9 => (-v.X, v.Y, -v.Z), 10 => (v.Y, v.X, -v.Z), 11 => (v.X, -v.Y, -v.Z),
            12 => (-v.Y, -v.X, -v.Z), 13 => (-v.Z, v.Y, v.X), 14 => (-v.Z, v.X, -v.Y), 15 => (-v.Z, -v.Y, -v.X),
            16 => (-v.Z, -v.X, v.Y), 17 => (v.X, -v.Z, v.Y), 18 => (-v.Y, -v.Z, v.X), 19 => (-v.X, -v.Z, -v.Y),
            20 => (v.Y, -v.Z, -v.X), 21 => (v.X, v.Z, -v.Y), 22 => (-v.Y, v.Z, -v.X), 23 => (-v.X, v.Z, v.Y),
        };
        return new Vector3(rotated.Item1, rotated.Item2, rotated.Item3);
    }
}