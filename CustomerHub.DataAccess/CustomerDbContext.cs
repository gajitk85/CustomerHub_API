
using CustomerHub.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerHub.DataAccess
{
    public class CustomerDbContext : DbContext
    {
        
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adjust the size based on your requirements
            modelBuilder.Entity<Customer>()
                .Property(e => e.SvgImage)
                .HasMaxLength(10000); 

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=CustomerDB.sqlite");
        }
    }

}
