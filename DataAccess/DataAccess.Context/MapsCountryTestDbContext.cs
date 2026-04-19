
using Microsoft.EntityFrameworkCore;
using System.Reflection;


public class MapsCountryTestDbContext : DbContext
{
    public DbSet<Office> Offices { get; set; }
    
    public MapsCountryTestDbContext(DbContextOptions<MapsCountryTestDbContext> options) : base(options)
    { 
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
            base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
