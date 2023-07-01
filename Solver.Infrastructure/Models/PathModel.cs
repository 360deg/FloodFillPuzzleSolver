namespace Solver.Infrastructure.Models;

public class PathModel : ICloneable
{
    public List<int> Colors { get; set; }
    public int[,] Board { get; set; }
    public int Count { get; set; }
    public object Clone()
    {
        return new PathModel
        {
            Colors = new List<int>(Colors),
            Count = Count,
            Board = Board
        };
    }
}
