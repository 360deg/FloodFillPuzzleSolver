namespace Solver.Infrastructure.Services;

public class Test : ITest
{
    public async Task<string> HealthCheck()
    {
        return await Task.Run(() => "Hello World");
    }
}
