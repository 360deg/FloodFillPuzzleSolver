using Solver.Infrastructure.Models;

namespace Solver.Infrastructure.Engine;

public partial class Game
{
    // Calculate next move
    public KeyValuePair<int, List<(int, int)>> CalculateNextMove(Dictionary<int, List<(int, int)>> connectedValues)
    {
        var nextColor = connectedValues.MaxBy(s => s.Value.Count);

        return nextColor;
    }
    
    /// <summary>
    /// Is Game over
    /// </summary>
    /// <param name="array"></param>
    /// <param name="value">empty space value = 8</param>
    /// <returns></returns>
    public bool IsGameOver(int[,] array, int value = 8)
    {
        var rows = array.GetLength(0);
        var columns = array.GetLength(1);

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                if (array[row, column] != value)
                {
                    return false;
                }
            }    
        }

        return true;
    }
    
    public void Loop(int[,] board, int currDepth, ref List<PathModel> list, PathModel? path = null, int depth = 5)
    {
        path ??= new PathModel
        {
            Colors = new List<int>()
        };

        if (IsGameOver(board))
        {
            path.Board = board;
            list.Add(path);
            return;
        }
        
        var connectedValues = CountConnectedCellsValue(board);
        foreach (var value in connectedValues)
        {
            if (currDepth == depth)
            {
                path.Board = board;
                list.Add(path);
                return;
            }
            
            var copyOfBoard = (int[,])board.Clone();
            var copyOfPath = (PathModel)path.Clone();
            
            ChangeConnectedCells(copyOfBoard, value);
            
            copyOfPath.Colors.Add(value.Key);
            copyOfPath.Count += value.Value.Distinct().Count();

            Loop(copyOfBoard, currDepth + 1, ref list, copyOfPath, depth);
        }
    }
}
