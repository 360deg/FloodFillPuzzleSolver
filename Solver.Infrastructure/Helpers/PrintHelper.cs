namespace Solver.Infrastructure.Helpers;

public static class PrintHelper
{
    public static void Print2DArray(int[,] array)
    {
        var numRows = array.GetLength(0);
        var numColumns = array.GetLength(1);

        for (var row = 0; row < numRows; row++)
        {
            for (int column = 0; column < numColumns; column++)
            {
                Console.Write(array[row, column] + " ");
            }
            
            Console.WriteLine();
        }
        Console.WriteLine("\n***********************************\n");
    }

    public static void PrintStepList(List<string?> list)
    {
        Console.WriteLine("\n***********************************\n");

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
        
        Console.WriteLine("\n***********************************\n");
    }
}
