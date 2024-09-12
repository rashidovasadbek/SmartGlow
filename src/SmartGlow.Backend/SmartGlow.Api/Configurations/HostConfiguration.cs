namespace SmartGlow.Api.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddPersistence()
            .AddIdentityInfrastructure()
            .AddMappers()
            .AddClientInfrastructure()
            .AddCaching()
            .AddValidators()
            .AddMediator()
            .AddRequestContextTools()
            .AddDevTools()
            .AddExposers();

        return new(builder);
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app.MigrateDataBaseSchemasAsync();
        await app.SeedDataAsync();
        
        app
            .UseExposers()
            .UseDevTools();
            
        return app;
    }
}