namespace server.Services;
public static class ServiceRegister
{
    public static void RegisterServices(this IServiceCollection services) {
        services.RegisterLogicServices();
        services.RegisterRepositoryServices();
        services.RegisterAuditingServices();
        services.RegisterMappingServices();
    }
}
