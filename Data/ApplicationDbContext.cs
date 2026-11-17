using Microsoft.EntityFrameworkCore;
using ProductValidationMVC.Models;

namespace ProductValidationMVC.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(12, 2);
        modelBuilder.Entity<Product>().Property(p => p.Category).HasMaxLength(60);
        modelBuilder.Entity<Product>().HasIndex(p => p.Name);
    }
}
