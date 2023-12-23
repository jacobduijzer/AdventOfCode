namespace AdventOfCode.UnitTests;

public class Playground
{
    [Fact]
    public void Playing()
    {
        var add = (int a, int b) => a + b;

        var addOne = (int a) => add(a, 1);

        Assert.Equal(5, addOne(4));
        
        var x = 10
            .AddOne()
            .AddOne();
        Assert.Equal(12, x);
    }

    [Fact]
    public void PlayingMore()
    {
        var concatenateFourStrings = (string a, string b, string c, string d) => 
            $"{a}{b}{c}{d}";

        var concatenateThreeStrings = (string a, string b, string c) =>
            concatenateFourStrings("first", a, b, c);
        
        
    }

    [Fact]
    public void PlayingMoreStill()
    {
        Func<int, int> addOne = x => x + 1;
        Func<int, int> multiplyByTwo = x => x * 2;

        Func<int, int> addOneAndMultiplyByTwo = x => multiplyByTwo(addOne(x));

        Assert.Equal(4, addOneAndMultiplyByTwo(1));
        
        Func<double, Func<double, double>> power = x => y => Math.Pow(x, y);
        Func<double, double> square = power(2);
        
        Assert.Equal(8, square(3));
    }
}

static class Test
{
    public static int AddOne(this int a)
    {
        return a + 1;
    }
}