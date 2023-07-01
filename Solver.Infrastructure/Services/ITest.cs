namespace Solver.Infrastructure.Services;

public interface ITest
{
    Task<string> HealthCheck();
}