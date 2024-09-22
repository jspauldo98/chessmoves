using server.Models;
using spauldo_techture;

namespace server.Services;

public static class AuditingService
{
    public static void RegisterAuditingServices(this IServiceCollection services)
    {
        var auditFactory = new AuditorFactory();

        // Matrix
        services.AddScoped<MatrixAuditor>();
        // Job
        services.AddScoped<JobAuditor>();
        // Puzzle Knight Moves
        services.AddScoped<PuzzleKnightMovesAuditor>();

        // ... Register additional auditors

        services.AddScoped(provider => {
            // Matrix
            var matrixAuditor = provider.GetRequiredService<MatrixAuditor>();
            auditFactory.RegisterAuditor(() => Task.FromResult<Auditor<MatrixEntity, MatrixModel, MatrixDto, MatrixEntityAudit>>(matrixAuditor));
            // Job
            var jobAuditor = provider.GetRequiredService<JobAuditor>();
            auditFactory.RegisterAuditor(() => Task.FromResult<Auditor<JobEntity, JobModel, JobDto, JobEntityAudit>>(jobAuditor));
            // Puzzle Knight Moves
            var puzzleKnightMovesAuditor = provider.GetRequiredService<PuzzleKnightMovesAuditor>();
            auditFactory.RegisterAuditor(() => Task.FromResult<Auditor<PuzzleKnightMovesEntity, PuzzleKnightMovesModel, PuzzleKnightMovesDto, PuzzleKnightMovesEntityAudit>>(puzzleKnightMovesAuditor));

            // .. Register additional auditors

            return auditFactory;
        });
    }
}