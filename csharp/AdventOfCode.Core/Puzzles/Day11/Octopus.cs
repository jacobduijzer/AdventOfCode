namespace AdventOfCode.Core.Puzzles.Day11;

public class Octopus
{
    public int Level = 0;

    public Octopus(int startValue) => Level = startValue;

    public int IncreaseLevel()
    {
        Level++;
        return Level;
    }

    public void Reset() => Level = 0;
}