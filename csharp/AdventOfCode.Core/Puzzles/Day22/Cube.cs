using System.Numerics;

namespace AdventOfCode.Core.Puzzles.Day22;

public record Cube(
    int MinX, int MaxX, 
    int MinY, int MaxY,  
    int MinZ, int MaxZ, 
    bool State);