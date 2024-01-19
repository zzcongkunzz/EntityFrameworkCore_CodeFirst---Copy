using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore_CodeFirst.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options) : base(options) { }
    
    #region DbSet
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOder> ProductOders { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
    }
}