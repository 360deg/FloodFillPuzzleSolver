using Microsoft.AspNetCore.Mvc;
using Solver.Infrastructure.Payloads;
using Solver.Infrastructure.Services;

namespace Solver.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase
{
    private readonly ITest _test;
    private readonly ICalculate _calculate;

    public MainController(ITest test, ICalculate calculate)
    {
        _test = test;
        _calculate = calculate;
    }

    [HttpGet("[action]")]
    public async Task<string> CheckHealth()
    {
        return await _test.HealthCheck();
    }

    [HttpPost]
    public List<int> Calculate([FromBody] CalculatePayload request)
    {
        return _calculate.AdvancedGreedyAlgorithmCalculation(request.Data);
        // await _calculate.GreedyAlgorithmCalculation(request.Data);
    }
}
