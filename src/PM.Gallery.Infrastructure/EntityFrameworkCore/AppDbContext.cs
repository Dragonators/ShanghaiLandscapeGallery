using Microsoft.EntityFrameworkCore;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Configurations;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ImageConfiguration());
    }
}