using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sellhandproduct.Models;
using sellhandproduct.Models.Domain;
//using sellhandproduct.Models.intermediate;

namespace sellhandproduct.Data
{

    public class MVCDataContext : DbContext

    {
        public MVCDataContext(DbContextOptions options) : base(options)
        {
        } 

        public DbSet<Products> Products { get; set; }

       


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Products>()
        //        .HasOne(p => p.Seller)
        //        .WithMany(s => s.Products) 
        //        .HasForeignKey(p => p.SellerId);
        //}

    }
}
