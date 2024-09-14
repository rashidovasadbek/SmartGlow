using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartGlow.Api.Data;
using SmartGlow.Api.Middlewares;
using SmartGlow.Application.Common.Identity.Services;
using SmartGlow.Application.Common.Settings;
using SmartGlow.Application.Streets.Services;
using SmartGlow.Application.Users.Services;
using SmartGlow.Domain.Brokers;
using SmartGlow.Domain.Constants;
using SmartGlow.Domain.Entities;
using SmartGlow.Infrastructure.Common.Caching;
using SmartGlow.Infrastructure.Common.Identity.Services;
using SmartGlow.Infrastructure.Common.Identity.Validators;
using SmartGlow.Infrastructure.Common.RequestContext.Brokers;
using SmartGlow.Infrastructure.Common.Settings;
using SmartGlow.Infrastructure.Streets.Services;
using SmartGlow.Infrastructure.Users.Services;
using SmartGlow.Persistence.Caching.Brokers;
using SmartGlow.Persistence.DataContext;
using SmartGlow.Persistence.Repositories;
using SmartGlow.Persistence.Repositories.Interfaces;

namespace SmartGlow.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies = Assembly
        .GetExecutingAssembly()
        .GetReferencedAssemblies()
        .Select(Assembly.Load)
        .Append(Assembly.GetExecutingAssembly())
        .ToList();

    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        // Configure CacheSettings from the app settings.
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        // Register the Memory Cache service.
        builder.Services.AddLazyCache();
        
        // Register the Memory Cache as a singleton.
        builder.Services.AddSingleton<ICacheBroker, LazyMemoryCacheBroker>();
        
        // Register middlewares
        builder.Services.AddSingleton<AccessTokenValidationMiddleware>();

        return builder;
    }
    
    private static WebApplicationBuilder AddClientInfrastructure(this WebApplicationBuilder builder)
    {
        // Register repositories
        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IStreetRepository, StreetRepository>();
        
        // Register services
        builder.Services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IStreetService, StreetService>();

        return builder;
    }
    
    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);
        return builder;
    }
    
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
    /// Configures devTools including controllers
    /// Configures IdentityInfrastructure including controllers
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
     private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        //configuration settings
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));
        builder.Services.Configure<PasswordValidationSettings>(builder.Configuration.GetSection(nameof(PasswordValidationSettings)));

        //register repository
        builder.Services
            .AddScoped<IAccessTokenRepository, AccessTokenRepository>()
            .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            
        //register services
        builder.Services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IIdentitySecurityTokenGeneratorService, IdentitySecurityTokenGeneratorService>()
            .AddScoped<IIdentitySecurityTokenService, IdentitySecurityTokenService>()
            .AddScoped<IPasswordGeneratorService, PasswordGeneratorService>()
            .AddScoped<IPasswordHasherService, PasswordHasherService>();
        
        
        
        // register authentication handlers
        var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>() ??
                          throw new InvalidOperationException("JwtSettings is not configured.");
        
        // add authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                options =>
                {
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = jwtSettings.ValidateIssuer,
                        ValidIssuer = jwtSettings.ValidIssuer,
                        ValidAudience = jwtSettings.ValidAudience,
                        ValidateAudience = jwtSettings.ValidateAudience,
                        ValidateLifetime = jwtSettings.ValidateLifetime,
                        ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
                }
            );
        
        return builder;
    }
    
    /// <summary>
    /// Configures the Dependency Injection container to include validators from referenced assemblies.
    /// </summary>
    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));
        builder.Services.AddValidatorsFromAssemblies(Assemblies.ToArray()).AddFluentValidationAutoValidation();

        builder.Services.AddTransient<IValidator<RefreshToken>, RefreshTokenValidator>();
        
        return builder;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    
    private static WebApplicationBuilder AddMediator(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(conf => {conf.RegisterServicesFromAssemblies(Assemblies.ToArray());});
        
        return builder;
    }
    
    /// <summary>
    /// Configures Request Context tool for the web application.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddRequestContextTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IRequestUserContextProvider, RequestUserContextProvider>();

        return builder;
    }
    
    /// <summary>
    /// Asynchronously migrates database schemas associated with the application.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    /// <returns>A ValueTask representing the asynchronous operation, with the WebApplication instance.</returns>
    private static async ValueTask<WebApplication> MigrateDataBaseSchemasAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);
        
        await serviceScopeFactory.MigrateAsync<AppDbContext>();
        
        return app;
    }
    
    /// <summary>
    /// Seeds data into the application's database by creating a service scope and initializing the seed operation.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
    }
    
    /// <summary>
    /// Registers developer tools
    /// </summary>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
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