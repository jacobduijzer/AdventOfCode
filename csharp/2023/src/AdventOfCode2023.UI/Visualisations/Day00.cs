using Spectre.Console;

namespace AdventOfCode2023.UI.Visualisations;

public static class Day00
{
    public static async Task Run()
    {
        var root = new Tree("Root");

        var foo = root.AddNode("[yellow]Foo[/]");
        var table = foo.AddNode(new Table()
            .RoundedBorder()
            .AddColumn("First")
            .AddColumn("Second")
            .AddRow("1", "2")
            .AddRow("3", "4")
            .AddRow("5", "6"));

        table.AddNode("[blue]Baz[/]");
        foo.AddNode("Qux");

        var bar = root.AddNode("[yellow]Bar[/]");
        bar.AddNode(new Calendar(2020, 12)
            .AddCalendarEvent(2020, 12, 12)
            .HideHeader());
        
        await AnsiConsole.Live(root)
            .AutoClear(false)
            .Overflow(VerticalOverflow.Ellipsis)
            .Cropping(VerticalOverflowCropping.Bottom)
            .StartAsync(async ctx => { }
            );
    }
}