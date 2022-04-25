using Microsoft.EntityFrameworkCore;
using TestApi.Entity;

namespace TestApi.Data
{
    public class SearchAndRangeContext : DbContext
    {
        //public DbSet<User> Users { get; set; }
        public DbSet<Okpd2> Okpd2s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=DESKTOP-5KT6NVS\SQLEXPRESS;Initial Catalog=AisSearchAndRangeSupplier;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
                //.HasKey(b => b.Id)
                //.HasName("Id");

            modelBuilder.Entity<Okpd2>().
                HasKey(b => b.Code)
                .HasName("Code");

            modelBuilder.Entity<Okpd2>().
                Property(b => b.Name)
                .HasColumnName("Name");
        }
    }
}
