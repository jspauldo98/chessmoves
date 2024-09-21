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

            // ... Register additional repos
            return repoFactory;
        });
    }
}