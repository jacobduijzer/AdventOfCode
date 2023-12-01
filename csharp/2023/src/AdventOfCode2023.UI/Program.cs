using AdventOfCode2023.Core;
using Spectre.Console;

// SETTINGS
const int NumberOfRows = 10;
const int CharacterDelay = 50;
const int LineDelay = 50;

var day01 = new Day01();

var directory = AppContext.BaseDirectory;
var fileName = "day01.txt";

var input = File.ReadAllLines(Path.Combine(directory, "input", fileName));

var table = new Table().Expand().BorderColor(Color.Grey);
table.AddColumn("[yellow]Calibration Line[/]");
table.AddColumn(new TableColumn("[yellow]Calibration Value[/]").Centered());
table.AddColumn(new TableColumn("[yellow]Total Calibration Sum[/]").Centered());

AnsiConsole.MarkupLine("Press [yellow]CTRL+C[/] to exit");

await AnsiConsole.Live(table)
    .AutoClear(false)
    .Overflow(VerticalOverflow.Ellipsis)
    .Cropping(VerticalOverflowCropping.Bottom)
    .StartAsync(async ctx =>
    {
        var currentLine = 0;
        foreach(var line in input)
        {
            if (table.Rows.Count > NumberOfRows)
            {
                table.Rows.RemoveAt(0);
                currentLine--;
            }

            day01.SetLine(line);
            table.AddRow(line, "0", "0");

            for (var i = 0; i < line.Length; i++)
            {
                day01.RecoverNext();
                table.Rows.Update(currentLine, 0, new Markup(day01.GetNewLine()));
                ctx.Refresh();
                await Task.Delay(CharacterDelay);
            }
            table.Rows.Update(currentLine, 1, new Markup(day01.LineResult().ToString()));
            table.Rows.Update(currentLine, 2, new Markup(day01.TotalResult().ToString()));
            
            currentLine++;
            
            ctx.Refresh();
            await Task.Delay(LineDelay);
        }
    });
    