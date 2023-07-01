using System.Text.RegularExpressions;

namespace Solver.Infrastructure.Shared;

public partial class Parser
{
    [GeneratedRegex("background-color: rgb\\((\\d+), (\\d+), (\\d+)\\);")]
    private static partial Regex ColorParsingRegex();
    
    private const int NumRows = 25;
    private const int NumColumns = 25;

    public static int[,] ParseHtml(string tableData)
    {
        var regex = ColorParsingRegex();
        var matches = regex.Matches(tableData);

        var rowIndex = 0;
        var columnIndex = 0;
        var colorCodes = new int[NumRows, NumColumns];

        foreach (Match match in matches)
        {
            var red = int.Parse(match.Groups[1].Value);
            var green = int.Parse(match.Groups[2].Value);
            var blue = int.Parse(match.Groups[3].Value);

            var colorCode = (int)GetColorCode(red, green, blue);
            colorCodes[rowIndex, columnIndex] = colorCode;

            columnIndex++;
            if (columnIndex != NumColumns) continue;

            rowIndex++;
            columnIndex = 0;
        }

        return colorCodes;
    }

    private static Colors GetColorCode(int red, int green, int blue)
    {
        return red switch
        {
            235 when green == 191 && blue == 106 => Colors.Bej, // bej
            0 when green == 0 && blue == 0 => Colors.Black, // black
            55 when green == 147 && blue == 214 => Colors.Blue, // blue
            255 when green == 255 && blue == 255 => Colors.White, // white
            211 when green == 42 && blue == 32 => Colors.Red ,// red
            39 when green == 49 && blue == 66 => Colors.Empty,
            _ => Colors.Error
        };
    }
    
    public enum Colors
    {
        Error = -1,
        Bej = 0,
        Black = 1,
        Blue = 2,
        White = 3,
        Red = 4,
        Empty = 8
    }
}