using Reqnroll;

namespace AdventOfCode.Specs;

[Binding]
public class Day01(ScenarioContext scenarioContext)
{
    private const string List = "LIST";
    private const string TotalDistance = "TOTAL_DISTANCE";

    private AdventOfCode.Core.Day01? _day01; 
    
    [Given(@"the list the Historians have")]
    public void GivenTheListTheHistoriansHave(string multilineText) =>
        scenarioContext[List] = multilineText;
    
    [Given(@"the list the Historians have, called '(.*)'")]
    public void GivenTheListTheHistoriansHaveCalled(string filename)
    {
        var filePath = Path.Combine(AppContext.BaseDirectory, filename);
        if(!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");
        
        var multilineText = File.ReadAllText(filePath);
        scenarioContext[List] = multilineText;
    }

    [When(@"you load and fix the list")]
    public void WhenYouLoadAndFixTheList()
    {
        _day01 = new AdventOfCode.Core.Day01((string)scenarioContext[List]);
    }
    
    [Then(@"there should be two arrays with the sorted values")]
    public void ThenThereShouldBeTwoArraysWithTheValues(Table table)
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

    [When(@"you calculate the total distances")]
    public void WhenYouCalculateTheTotalDistances()
    {
        var totalDistance = _day01.SolvePart1();
        scenarioContext.Add(TotalDistance, totalDistance);
    }

    [When(@"you calculate the similarity score")]
    public void WhenYouCalculateTheSimilarityScore()
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