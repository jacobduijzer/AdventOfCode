using System.Text;
using AdventOfCode.Core.Common;
using IronOcr;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace AdventOfCode.Core.Puzzles.Day13;

public class Solution : PuzzleBase
{
    public const char Character = '#';
    public const char EmptyCharacter = '.';
    
    private readonly Grid _grid;

    public Solution(string inputFile)
    {
        _grid = new Grid(DataReader.ReadLinesFromFile(inputFile));
    }

    public override object SolvePart1()
    {
        return _grid.HandleFolds(new List<Fold> { _grid.Folds[0] }).Count;
    }

    public override object SolvePart2()
    {
        var newGrid = _grid.HandleFolds(_grid.Folds);
        // CreateBitmap(newGrid);
        return CreateOutputString(newGrid);
    }

    private string CreateOutputString(HashSet<(int X, int Y)> grid)
    {
        var columns = grid.Max(p => p.X);
        var row = grid.Max(p => p.Y);

        var sb = new StringBuilder();
        sb.AppendLine();

        for (var y = 0; y <= row; y++)
        {
            for (var x = 0; x <= columns; x++)
                sb.Append(grid.Contains((x, y)) ? Character : EmptyCharacter);

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private void CreateBitmap(HashSet<(int X, int Y)> grid)
    {
        BitmapExportContext bmpContext = SkiaGraphicsService.Instance.CreateBitmapExportContext(600, 400);
        SizeF bmpSize = new(bmpContext.Width, bmpContext.Height);
        ICanvas canvas = bmpContext.Canvas;

        ClearBackground(canvas, bmpSize, Colors.Navy);
        DrawLines(canvas, bmpSize, grid);
        // for (int i = 0; i < outputLines.Length; i++)
        //     DrawBigTextWithShadow(canvas, outputLines[i], i);

        SaveFig(bmpContext, Path.GetFullPath("quickstart.jpg"));
    }


    void ClearBackground(ICanvas canvas, SizeF bmpSize, Color bgColor)
    {
        canvas.FillColor = Colors.Navy;
        canvas.FillRectangle(0, 0, bmpSize.Width, bmpSize.Height);
    }

    void DrawLines(ICanvas canvas, SizeF bmpSize, HashSet<(int X, int Y)> grid)
    {
        canvas.StrokeSize = 8;
        int scale = 8;
        
        canvas.StrokeColor = Colors.White;

        var columns = grid.Max(p => p.X);
        var row = grid.Max(p => p.Y);
        for (var y = 0; y <= row; y++)
        {
            for (var x = 0; x <= columns; x++)
            {
                if (grid.Contains((x, y)))
                    canvas.DrawLine(
                        x1: x + (x * scale),
                        y1: y + (y * scale),
                        x2: x + 10 + (x * scale),
                        y2: y + 10 + (y * scale));
            }
        }
    }

    void SaveFig(BitmapExportContext bmp, string filePath)
    {
        bmp.WriteToFile(filePath);
        Console.WriteLine($"WROTE: {filePath}");
        
        // var Ocr = new IronTesseract();
        // using (var Input = new OcrInput(filePath))
        // {
        //     // Input.Deskew();  // use if image not straight
        //     // Input.DeNoise(); // use if image contains digital noise
        //     var Result = Ocr.Read(Input);
        //     Console.WriteLine(Result.Text);
        // }
        string Text = new IronTesseract().Read(filePath).Text;

    }
}