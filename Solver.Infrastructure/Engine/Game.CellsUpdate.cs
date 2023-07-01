namespace Solver.Infrastructure.Engine;

public partial class Game
{
    public void ChangeConnectedCells(int[,] array, int row, int column)
    {
        int targetValue = array[row, column];
        
        // Check if target cell is already 8 or out of range
        if (targetValue == 8 || row < 0 || row >= array.GetLength(0) || column < 0 || column >= array.GetLength(1))
            return;
        
        // Search
        DFS(array, row, column, targetValue);
    }
    
    public void ChangeConnectedCells(int[,] array, KeyValuePair<int, List<(int, int)>> cells)
    {
        int targetValue = cells.Key;
        
        // Check if target cell is already 8
        if (targetValue == 8)
            return;

        foreach (var cell in cells.Value)
        {
            // Search
            DFS(array, cell.Item1, cell.Item2, targetValue);
        }
    }

    private void DFS(int[,] array, int row, int column, int targetValue)
    {
        // Check if the current cell is out of bounds or has a different value
        if (row < 0 || row >= array.GetLength(0) || column < 0 || column >= array.GetLength(1) || array[row, column] != targetValue)
            return;
        
        // Change the value of current cell to 8
        array[row, column] = 8;
        
        // Recursively process the adjacent cells
        DFS(array, row - 1, column, targetValue); // Up
        DFS(array, row + 1, column, targetValue); // Down
        DFS(array, row, column - 1, targetValue); // Left
        DFS(array, row, column + 1, targetValue); // Right
    }
}
