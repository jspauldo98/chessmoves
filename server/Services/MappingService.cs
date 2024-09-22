using server.Mapping;
using server.Models;
using spauldo_techture;

namespace server.Services;

public static class MappingService
{
    public static void RegisterMappingServices(this IServiceCollection services)
    {
        var mapperFactory = new MapperFactory();
        
        // Matrix
        services.AddScoped<MatrixMapper>();
        // Job
        services.AddScoped<JobMapper>();
        // Puzzle Knight Moves
        services.AddScoped<PuzzleKnightMovesMapper>();

        // ... Register other Mappers

        services.AddScoped(provider => {
            // Matrix
            var matrixMapper = provider.GetRequiredService<MatrixMapper>();
            mapperFactory.RegisterMapper(() => Task.FromResult<MapperYonHash<MatrixEntity, MatrixModel, MatrixDto, MatrixEntityAudit>>(matrixMapper));
            // Job
            var jobMapper = provider.GetRequiredService<JobMapper>();
            mapperFactory.RegisterMapper(() => Task.FromResult<MapperYonHash<JobEntity, JobModel, JobDto, JobEntityAudit>>(jobMapper));
            // Job
            var puzzleKnightMovesMapper = provider.GetRequiredService<PuzzleKnightMovesMapper>();
            mapperFactory.RegisterMapper(() => Task.FromResult<MapperYonHash<PuzzleKnightMovesEntity, PuzzleKnightMovesModel, PuzzleKnightMovesDto, PuzzleKnightMovesEntityAudit>>(puzzleKnightMovesMapper));

            // ... Register other mappers
            
            return mapperFactory;
        });
    }
}