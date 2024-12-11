using Reqnroll;

namespace AdventOfCode.Specs;

[Binding]
public class Day06(ScenarioContext scenarioContext)
{
    private const string Input = "INPUT";

    private AdventOfCode.Core.Day06? _day06; 
    
    [Given(@"a map of the guard situation")]
    public void GivenAMapOfTheGuardSituation(string multilineText) =>
        scenarioContext[Input] = multilineText;

    [When(@"you load the grid")]
    public void WhenYouLoadTheGrid()
    {
        _day06 = new AdventOfCode.Core.Day06((string)scenarioContext[Input]);
    }

    [Then(@"the location of the guard should be (.*)")]
    public void ThenTheLocationOfTheGuardShouldBe(string guardLocation)
    {
        var location = guardLocation.Split(',').Select(int.Parse).ToArray();
        Assert.Equal((location[0], location[1]), _day06!.GuardStart);
    }

    [Then(@"the number of steps should be (.*)")]
    public void ThenTheNumberOfStepsShouldBe(int p0)
    {
        var steps = _day06!.SolvePart1();
        Assert.Equal(p0, steps);
    }
}