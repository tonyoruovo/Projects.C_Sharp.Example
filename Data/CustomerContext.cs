
using Example.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.Data
{
    public class CustomerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Customer");
        }
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<CustomerModel>()
        //         .HasOne<CountryModel>(c => c.Country);

        //     modelBuilder.Entity<CountryModel>()
        //         .HasMany(d => d.CountryDetails)
        //         .HasForeignKey(cd => cd.CountryId)
        //         .OnDelete(DeleteBehavior.SetNull);
        // }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<CountryDetailsModel> CountryDetails { get; set; }
    }
}