using Solver.Infrastructure.Engine;
using Solver.Infrastructure.Helpers;
using Solver.Infrastructure.Models;
using Solver.Infrastructure.Shared;

namespace Solver.Infrastructure.Services;

public class Calculate : ICalculate
{
    private readonly Game _game;
    
    public Calculate()
    {
        _game = new Game();
    }
    
    public void GreedyAlgorithmCalculation(string data)
    {
        var board = Parser.ParseHtml(data);
        
        const int row = 0;
        const int column = 0;
        
        _game.ChangeConnectedCells(board, row, column);

        var step = 0;
        var listOfSteps = new List<string?>();

        while (!_game.IsGameOver(board))
        {
            var connectedValues = _game.CountConnectedCellsValue(board);
            var nextColorToChoose = _game.CalculateNextMove(connectedValues);
            _game.ChangeConnectedCells(board, nextColorToChoose);
            
            step++;
            listOfSteps.Add(step + ". " + Enum.GetName(typeof(Parser.Colors), nextColorToChoose.Key));
        }
        
        PrintHelper.PrintStepList(listOfSteps);
        Console.WriteLine($"Step: {step}");
    }

    public List<int> AdvancedGreedyAlgorithmCalculation(string data)
    {
        var board = Parser.ParseHtml(data);
        
        const int row = 0;
        const int column = 0;
        
        _game.ChangeConnectedCells(board, row, column);
        
        var listOfSteps = new List<PathModel>();
        var sortedList = new List<PathModel>();
        PathModel? path = null;
        do
        {
            _game.Loop(board, 0, ref listOfSteps, path: path, depth: 5);
            sortedList = listOfSteps.OrderBy(s => s.Colors.Count).ThenByDescending(s => s.Count).Take(100).ToList();
            board = sortedList.First().Board;
            path = sortedList.First();
            listOfSteps = new List<PathModel>();
        } while (!_game.IsGameOver(sortedList.First().Board));
        
        return sortedList.First().Colors;
    }
}
