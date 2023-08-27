using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sellhandproduct.Models;
using sellhandproduct.Models.Domain;
//using sellhandproduct.Models.intermediate;

namespace sellhandproduct.Data
{

    public class MVCDemoDbContext : IdentityDbContext 

    {
        public MVCDemoDbContext(DbContextOptions<MVCDemoDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<ProductCategory> ProductCategory { get; set; }

        public DbSet<Register> register { get; set; } 

        public DbSet<ApplicationUser> applicationUsers { get; set; }

        public DbSet<Baskets> Basket { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Basket tablosundaki UserID alanını ApplicationUser tablosundaki Id alanına bağlama
            builder.Entity<Baskets>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .IsRequired();
        }

    }
}
