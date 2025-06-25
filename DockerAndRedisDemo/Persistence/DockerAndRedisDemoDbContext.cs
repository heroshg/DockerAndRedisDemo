using Microsoft.EntityFrameworkCore;

namespace DockerAndRedisDemo.Persistence;

public class DockerAndRedisDemoDbContext : DbContext
{
    public DockerAndRedisDemoDbContext(DbContextOptions<DockerAndRedisDemoDbContext> options) : base(options)
    { }

    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(80);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("DECIMAL(19,2)");

        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(200);

        base.OnModelCreating(modelBuilder);
    }

}
