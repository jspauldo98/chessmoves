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

        // ... Register other Mappers

        services.AddScoped(provider => {
            // Matrix
            var matrixMapper = provider.GetRequiredService<MatrixMapper>();
            mapperFactory.RegisterMapper(() => Task.FromResult<MapperYonHash<MatrixEntity, MatrixModel, MatrixDto, MatrixEntityAudit>>(matrixMapper));

            // ... Register other mappers
            
            return mapperFactory;
        });
    }
}