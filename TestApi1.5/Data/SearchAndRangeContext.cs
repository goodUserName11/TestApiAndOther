using Microsoft.EntityFrameworkCore;
using TestApi.Entity;
using TestApi1._5.Entity;

namespace TestApi.Data
{
    public class SearchAndRangeContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Okpd2> Okpd2s { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserCompany> Companies { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<ProductPricesRow> productPricesRows { get; set; }

        public DbSet<SuppliersList> SuppliersLists { get; set; }
        public DbSet<SupplierInList> SupplierInLists { get; set; }

        public DbSet<CriterionСoefficientType> Types { get; set; }
        public DbSet<Coefficient> Coefficients { get; set; }
        public DbSet<CoefficientValue> CoefficientValues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                //@"Data Source=wpl36.hosting.reg.ru;Initial Catalog=u1662870_Zakupki;Persist Security Info=True;User ID=u1662870_konstantin;Password=@x9Aw64e");
                "Data Source=DESKTOP-5KT6NVS\\SQLEXPRESS;Initial Catalog=ZakupkiLocal;Integrated Security=True");
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

            //Contact
            modelBuilder.Entity<Contact>()
                .HasKey(b => b.Id)
                .HasName("Id");

            //Product
            //modelBuilder.Entity<Product>().ToTable("Suppler_s_Products");

            //ProductPricesView
            modelBuilder.Entity<ProductPricesRow>().HasNoKey().ToView("ProductPricesView");
        }
    }
}
