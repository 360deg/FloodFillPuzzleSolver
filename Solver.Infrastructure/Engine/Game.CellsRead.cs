namespace Solver.Infrastructure.Engine;

public partial class Game
{
    private List<(int, int)> FindAllCellsInCluster(int[,] array, int row, int column, int targetValue)
    {
        var cluster = new List<(int, int)>();
        var visited = new bool[array.GetLength(0), array.GetLength(1)];

        DFSHelper(array, row, column, targetValue, cluster, visited);

        return cluster;
    }

    private void DFSHelper(int[,] array, int row, int column, int targetValue, List<(int, int)> cluster, bool[,] visited)
    {
        // Check if the current cell is out of bounds, has a different value, or has been visited
        if (row < 0 || row >= array.GetLength(0) || column < 0 || column >= array.GetLength(1) || array[row, column] != targetValue || visited[row, column])
            return;
        
        // Mark the current cell as visited
        visited[row, column] = true;
        
        // Add the current cell to the cluster
        cluster.Add((row, column));
        
        // Recursively process the adjacent cells
        DFSHelper(array, row - 1, column, targetValue, cluster, visited); // Up
        DFSHelper(array, row + 1, column, targetValue, cluster, visited); // Down
        DFSHelper(array, row, column - 1, targetValue, cluster, visited); // Left
        DFSHelper(array, row, column + 1, targetValue, cluster, visited); // Right
    }

    public Dictionary<int, List<(int, int)>> CountConnectedCellsValue(int[,] data)
    {
        var connectedValues = new Dictionary<int, List<(int, int)>>();
        
        var rows = data.GetLength(0);
        var columns = data.GetLength(1);

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                var value = data[row, column];
                
                if (value != 8) continue;
                // check cells
                if (row - 1 >= 0)
                    AddValue(data[row - 1, column], connectedValues, row - 1, column);
                if (row + 1 < rows)
                    AddValue(data[row + 1, column], connectedValues, row + 1, column);
                if (column - 1 >= 0)
                    AddValue(data[row, column - 1], connectedValues, row, column - 1);
                if (column + 1 < columns)
                    AddValue(data[row, column + 1], connectedValues, row, column + 1);
            }
        }
        
        // Add all connected cells 
        foreach (var (value, valueTuples) in connectedValues)
        {
            foreach (var clusterCoord in valueTuples.ToList()
                         .Select(coord => FindAllCellsInCluster(data,
                             coord.Item1,
                             coord.Item2,
                             data[coord.Item1,
                                 coord.Item2]))
                         .SelectMany(cluster => from clusterCoord in cluster
                             let tst = connectedValues.First(c => c.Key == value)
                             where !tst.Value.Contains(clusterCoord)
                             select clusterCoord))
            {
                if (!connectedValues.ContainsKey(value))
                    connectedValues[value] = new List<(int, int)>();

                connectedValues[value].Add(clusterCoord);
            }
        }

        return connectedValues;

    }

    private static void AddValue(int value, Dictionary<int, List<(int, int)>> connectedValues, int row, int column)
    {
        if (value == 8) return;
        if (connectedValues.TryGetValue(value, out var connectedValue))
            connectedValue.Add((row, column));
        else
            connectedValues[value] = new List<(int, int)> { (row, column) };
    }
}