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

        // ... Register additional auditors

        services.AddScoped(provider => {
            // Matrix
            var matrixAuditor = provider.GetRequiredService<MatrixAuditor>();
            auditFactory.RegisterAuditor(() => Task.FromResult<Auditor<MatrixEntity, MatrixModel, MatrixDto, MatrixEntityAudit>>(matrixAuditor));

            // .. Register additional auditors

            return auditFactory;
        });
    }
}