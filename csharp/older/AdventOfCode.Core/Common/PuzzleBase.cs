namespace AdventOfCode.Core.Common;

public abstract class PuzzleBase
{
    public abstract object SolvePart1();
    public abstract object SolvePart2();
}

public abstract class PuzzleBase<TInputType> : PuzzleBase
{
    public TInputType Input { get; private set; }

    public abstract TInputType ParseInput(string inputFile);
    
    protected PuzzleBase(string inputFile)
    {
        Input = ParseInput(inputFile);
    }
}