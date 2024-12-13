using AdventOfCode.Core.Utils;
using Reqnroll;

namespace AdventOfCode.Specs;

[Binding]
public class Day06(ScenarioContext scenarioContext)
{
    private const string Input = "INPUT";
    private const string Positions = "Positions";

    private AdventOfCode.Core.Day06? _day06;

    [Given(@"a map of the guard situation")]
    public void GivenAMapOfTheGuardSituation(string multilineText)
        => scenarioContext.Add(Input, multilineText);

    [Given(@"the map of the guard situation called '(.*)'")]
    public void GivenTheMapOfTheGuardSituationCalled(string filename)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, filename);
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var multilineText = File.ReadAllText(filePath);
        scenarioContext[Input] = multilineText;
    }

    [When(@"you load the grid")]
    public void WhenYouLoadTheGrid()
        => _day06 = new AdventOfCode.Core.Day06(scenarioContext.Get<string>(Input));

    [Then(@"the location of the guard should be (.*)")]
    public void ThenTheLocationOfTheGuardShouldBe(string guardLocation)
    {
        var location = guardLocation.Split(',').Select(int.Parse).ToArray();
        Assert.Equal((location[0], location[1]), _day06!.GuardStartPosition);
    }

    [When(@"you count the number of distinct positions the guard visits")]
    public void WhenYouCountTheNumberOfDistinctPositionsTheGuardVisits()
        => scenarioContext.Add(Positions, _day06!.SolvePart1());

    [Then(@"the number of distinct locations the guard visits should be (.*)")]
    public void ThenTheNumberOfDistinctLocationsTheGuardVisitsShouldBe(int steps)
        => Assert.Equal(steps, scenarioContext.Get<int>(Positions));


    [When(@"the guard takes (.*) step")]
    public void WhenTheGuardTakesStep(int numberOfSteps)
    {
        var step = new AdventOfCode.Core.Day06.Step(_day06.GuardStartPosition, DirectionRotator.Directions.North, false);
        for (var i = 0; i < numberOfSteps; i++)
        {
            step = _day06.NextStep(step);
        }
        scenarioContext.Add("GUARD_POSITION", step.CurrentGuardPosition);
    }

    [Then(@"the next location of the guard should be (.*)")]
    public void ThenTheNextLocationOfTheGuardShouldBe(string guardLocation )
    {
        var location = guardLocation.Split(',').Select(int.Parse).ToArray();
        Assert.Equal((location[0], location[1]), scenarioContext.Get<(int, int)>("GUARD_POSITION"));
    }

    [Then(@"the path the guard walks looks like this")]
    public void ThenThePathTheGuardWalksLooksLikeThis(Table table)
    {
        ScenarioContext.StepIsPending();
    }
}