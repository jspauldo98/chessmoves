using server.Logic;
using server.Models;
using spauldo_techture;

namespace server.Services;

public static class LogicService
{
    public static void RegisterLogicServices(this IServiceCollection services)
    {
        // Matrix
        services.AddScoped<IMatrixLogic, MatrixLogic>();
        services.AddScoped(typeof(ILogicCrudYon<MatrixDto, MatrixModel, MatrixEntity, MatrixEntityAudit>), typeof(MatrixLogic));
        // Job
        services.AddScoped<IJobLogic, JobLogic>();
        services.AddScoped(typeof(ILogicCrudYon<JobDto, JobModel, JobEntity, JobEntityAudit>), typeof(JobLogic));
        // Puzzle Knight Moves
        services.AddScoped<IPuzzleKnightMovesLogic, PuzzleKnightMovesLogic>();
        services.AddScoped(typeof(ILogicCrudYon<PuzzleKnightMovesDto, PuzzleKnightMovesModel, PuzzleKnightMovesEntity, PuzzleKnightMovesEntityAudit>), typeof(PuzzleKnightMovesLogic));

        // ... Register other Logic 
    }
}