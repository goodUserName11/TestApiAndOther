using Microsoft.EntityFrameworkCore;
using TestApi.Entity;

namespace TestApi.Data
{
    public class SearchAndRangeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Okpd2> Okpd2s { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCompany> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=wpl36.hosting.reg.ru;Initial Catalog=u1662870_Zakupki;Persist Security Info=True;User ID=u1662870_konstantin;Password=@x9Aw64e");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Okpd2
            modelBuilder.Entity<Okpd2>()
                .HasKey(b => b.Code)
                .HasName("Code");

            modelBuilder.Entity<Okpd2>()
                .Property(b => b.Name)
                .HasColumnName("Name");

            modelBuilder.Entity<Okpd2>()
                .Property(b => b.Parent)
                .HasColumnName("Parent");

            //Roles
            modelBuilder.Entity<Role>()
                .HasKey(b => b.Id)
                .HasName("Id");

            modelBuilder.Entity<Role>()
                .Property(b => b.Name)
                .HasColumnName("Name");

            //UserCompany
            modelBuilder.Entity<UserCompany>()
                .HasKey(b => b.Inn)
                .HasName("Inn");

            modelBuilder.Entity<UserCompany>()
                .Property(b => b.CompanyName)
                .HasColumnName("Company_Name");

            modelBuilder.Entity<UserCompany>()
                .Property(b => b.Address)
                .HasColumnName("Address");

            //User
            modelBuilder.Entity<User>().HasKey(b => b.Id).HasName("Id");
            modelBuilder.Entity<User>().Property(b => b.Name).HasColumnName("Name");
            modelBuilder.Entity<User>().Property(b => b.Surname).HasColumnName("Surname");
            modelBuilder.Entity<User>().Property(b => b.Patronimic).HasColumnName("Patronimic");
            modelBuilder.Entity<User>().Property(b => b.Password).HasColumnName("Password");
            modelBuilder.Entity<User>().Property(b => b.Email).HasColumnName("Email");
            modelBuilder.Entity<User>().Property(b => b.Phone).HasColumnName("Phone");
            modelBuilder.Entity<User>().Property(b => b.Role).HasColumnName("Role");
            modelBuilder.Entity<User>().Property(b => b.CompanyInn).HasColumnName("Company");
        }
    }
}
