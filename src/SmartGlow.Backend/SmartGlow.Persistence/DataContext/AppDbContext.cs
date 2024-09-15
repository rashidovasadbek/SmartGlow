using Microsoft.EntityFrameworkCore;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Persistence.DataContext;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions): DbContext(dbContextOptions)
{
    #region Identity Infrastructure
    
    public DbSet<User> Users => Set<User>();
    
    #endregion

    #region Client

    public DbSet<Street> Streets => Set<Street>();
    
    public DbSet<OnOffTiming> OnOffTimings => Set<OnOffTiming>();

    #endregion
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}