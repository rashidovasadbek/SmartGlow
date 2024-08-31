using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SmartGlow.Domain.Constants;
using SmartGlow.Infrastructure.Settings;
using SmartGlow.Persistence.DataContext;

namespace SmartGlow.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies = Assembly
        .GetExecutingAssembly()
        .GetReferencedAssemblies()
        .Select(Assembly.Load)
        .Append(Assembly.GetExecutingAssembly())
        .ToList();

    
    /// <summary>
    /// Registers persistence infrastructure
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // Determine connection string
        var dbConnectionString = builder.Environment.IsProduction()
            ? Environment.GetEnvironmentVariable(DataAccessConstant.DbConnectionString)
            : builder.Configuration.GetConnectionString(DataAccessConstant.DbConnectionString);
        
        // Register data context
        builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(dbConnectionString);});
        
        return builder;
    }

    /// <summary>
    /// Configures the Dependency Injection container to include validators from referenced assemblies.
    /// </summary>
    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));
        builder.Services.AddValidatorsFromAssemblies(Assemblies);

        return builder;
    }
    
    /// <summary>
    /// Registers developer tools
    /// </summary>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        
        return builder;
    }

    /// <summary>
    /// Registers API exposers
    /// </summary>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => { options.LowercaseUrls = true; });
        builder.Services.AddControllers().AddNewtonsoftJson();

        return builder;
    }
    
    /// <summary>
    /// Registers developer tools middlewares
    /// </summary>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }

    /// <summary>
    /// Registers exposer middlewares
    /// </summary>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();
        
        return app;
    }
    
}