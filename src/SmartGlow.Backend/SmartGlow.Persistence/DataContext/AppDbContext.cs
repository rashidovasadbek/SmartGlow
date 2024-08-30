using Microsoft.EntityFrameworkCore;
using SmartGlow.Domain.Entities;

namespace SmartGlow.Persistence.DataContext;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions): DbContext(dbContextOptions)
{
    #region Identity Infrastructure
    
    public DbSet<User> Users => Set<User>();
    
    #endregion
}