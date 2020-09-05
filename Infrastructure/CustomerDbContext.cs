namespace MicroCredential.Infrastructure
{
    using MicroCredential.Infrastructure.Entity;
    using Microsoft.EntityFrameworkCore;

    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(customer =>
            {
                customer.HasKey(c => c.CustomerId);
            });
        }
    }
}
