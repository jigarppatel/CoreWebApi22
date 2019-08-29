using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace CoreWebApi22.Models
{
    public partial class CoreWebApiTestDBContext : DbContext
    {
       
        public CoreWebApiTestDBContext(DbContextOptions<CoreWebApiTestDBContext> options, IConfiguration configuration)
            : base(options)
        {
            
        }

        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.ProductCategoryName).IsUnicode(false);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductDescription).IsUnicode(false);

                entity.Property(e => e.ProductImagePath).IsUnicode(false);

                entity.Property(e => e.ProductTitle).IsUnicode(false);

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductCategories");
            });
        }
    }
}
