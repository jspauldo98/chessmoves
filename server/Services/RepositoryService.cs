using server.Models;
using server.Repository;
using spauldo_techture;

namespace server.Services;

public static class RepositoryService
{
    public static void RegisterRepositoryServices(this IServiceCollection services)
    {
        var repoFactory = new RepoFactory();

        // Matrix
        services.AddScoped<IMatrixRepo, MatrixRepo>();
        services.AddScoped<IMatrixAuditRepo, MatrixAuditRepo>();
        // Job
        services.AddScoped<IJobRepo, JobRepo>();
        services.AddScoped<IJobAuditRepo, JobAuditRepo>();
        // Puzzle Knight Moves
        services.AddScoped<IPuzzleKnightMovesRepo, PuzzleKnightMovesRepo>();
        services.AddScoped<IPuzzleKnightMovesAuditRepo, PuzzleKnightMovesAuditRepo>();

        // ... Register additional Repos

        services.AddScoped(provider => {
            // Matrix
            var matrixRepo = provider.GetRequiredService<IMatrixRepo>();
            var matrixAuditRepo = provider.GetRequiredService<IMatrixAuditRepo>();
            repoFactory.RegisterRepo(() => Task.FromResult<IRepoEntityFramework<MatrixEntity>>(matrixRepo));
            repoFactory.RegisterRepo(() => Task.FromResult<IRepoEntityFramework<MatrixEntityAudit>>(matrixAuditRepo));

            // Job
            var jobRepo = provider.GetRequiredService<IJobRepo>();
            var jobAuditRepo = provider.GetRequiredService<IJobAuditRepo>();
            repoFactory.RegisterRepo(() => Task.FromResult<IRepoEntityFramework<JobEntity>>(jobRepo));
            repoFactory.RegisterRepo(() => Task.FromResult<IRepoEntityFramework<JobEntityAudit>>(jobAuditRepo));

            // Puzzle Knight Moves
            var puzzleKnightMovesRepo = provider.GetRequiredService<IPuzzleKnightMovesRepo>();
            var puzzleKnightMovesAuditRepo = provider.GetRequiredService<IPuzzleKnightMovesAuditRepo>();
            repoFactory.RegisterRepo(() => Task.FromResult<IRepoEntityFramework<PuzzleKnightMovesEntity>>(puzzleKnightMovesRepo));
            repoFactory.RegisterRepo(() => Task.FromResult<IRepoEntityFramework<PuzzleKnightMovesEntityAudit>>(puzzleKnightMovesAuditRepo));

            // ... Register additional repos
            return repoFactory;
        });
    }
}