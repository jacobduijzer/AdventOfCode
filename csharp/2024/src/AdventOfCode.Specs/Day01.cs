using Reqnroll;

namespace AdventOfCode.Specs;

[Binding]
public class Day01(ScenarioContext scenarioContext)
{
    private const string List = "LIST";
    private const string TotalDistance = "TOTAL_DISTANCE";

    private AdventOfCode.Core.Day01 _day01 = new AdventOfCode.Core.Day01();
    
    [Given(@"the list the Historians have")]
    public void GivenTheListTheHistoriansHave(string multilineText) =>
        scenarioContext[List] = multilineText;
    
    [Given(@"the list the Historians have, called '(.*)'")]
    public void GivenTheListTheHistoriansHaveCalled(string filename)
    {
        var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;
        var multilineText = File.ReadAllText(Path.Combine(path, filename));
        scenarioContext[List] = multilineText;
    }

    [When(@"you fix the list")]
    public void WhenYouFixTheList()
    {
        _day01.AddData((string)scenarioContext[List]);
    }
    
    [Then(@"there should be to arrays with the sorted values")]
    public void ThenThereShouldBeToArraysWithTheValues(Table table)
    {
        var data = table.CreateInstance<(string leftNumbers, string rightNumbers)>();
        foreach (var (number, index) in data.leftNumbers.Split(',').Select((value, i) => (int.Parse(value), i)))
        {
            Assert.Equal(number, _day01.LeftLocations[index]);
        }
        
        foreach (var (number, index) in data.rightNumbers.Split(',').Select((value, i) => (int.Parse(value), i)))
        {
            Assert.Equal(number, _day01.RightLocations[index]);
        }
    }

    [When(@"calculate the total distances")]
    public void WhenCalculateTheTotalDistances()
    {
        var totalDistance = _day01.SolvePart1();
        scenarioContext.Add(TotalDistance, totalDistance);
    }

    [When(@"calculate the similarity score")]
    public void WhenCalculateTheSimilarityScore()
    {
        var totalDistance = _day01.SolvePart2();
        scenarioContext.Add(TotalDistance, totalDistance);
    }

    [Then(@"the total distance should be (.*)")]
    public void ThenTheTotalDistanceShouldBe(int expectedTotalDistance)
    {
        var totalDistance = (int)scenarioContext[TotalDistance];
        Assert.Equal(expectedTotalDistance, totalDistance);
    }

}