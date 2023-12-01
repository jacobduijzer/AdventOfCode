using AdventOfCode.Core.Puzzles.Day16;
using Xunit;

namespace AdventOfCode.UnitTests.Puzzles.Day16;

public class DecoderShould
{
    [Fact]
    public void DecodeMessage()
    {
        // ARRANGE
        Decoder decoder = new("D2FE28");
        
        // ACT
        var result =decoder.DecodeTransMission();
        
        // ASSERT 
        Assert.NotNull(result);
        Assert.IsType<LiteralValuePackage>(result);
        Assert.Equal(6, result.Version);
        Assert.Equal(OperationType.Literal, result.OperationType);
        Assert.Equal(2021, ((LiteralValuePackage) result).Value);
    }

    [Fact]
    public void DecodeAnotherMessage()
    {
        // ARRANGE
        Decoder decoder = new("38006F45291200");
        
        // ACT
        var result = decoder.DecodeTransMission();
        
        // ASSERT 
        Assert.NotNull(result);
        Assert.IsType<OperatorPackage>(result);
        // Assert.Equal(6, result.Version);
        // Assert.Equal(4, result.TypeId);
        // Assert.Equal(2021, ((LiteralValuePackage) result).Value);
        
    }
    
}