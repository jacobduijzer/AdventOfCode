namespace AdventOfCode.Core.Common;

public static class GridHelper
{
    public static bool IsValidPoint(int column, int row, int maxColumns, int maxRows) =>
        0 <= column && column < maxColumns && 0 <= row && row < maxRows;
}