using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace MyPopuStore.DAL.DBd
{
    public partial class MyPopupStoreDBContext : DbContext
    {
        public MyPopupStoreDBContext()
        {
        }

        public MyPopupStoreDBContext(DbContextOptions<MyPopupStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryPrice> CategoryPrices { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SaleDetail> SaleDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.json", true, true).Build();
                optionsBuilder.UseSqlServer(config[$"ConnectionStrings:MyPopuStoreDB"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<CategoryPrice>(entity =>
            {
                entity.ToTable("CategoryPrice");

                entity.Property(e => e.CategoryPriceId)
                    .ValueGeneratedNever()
                    .HasColumnName("CategoryPriceID");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_Product_1");

                entity.ToTable("Product");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryPriceId).HasColumnName("CategoryPriceID");

                entity.Property(e => e.Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Picture).HasColumnType("text");

                entity.HasOne(d => d.CategoryPrice)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryPriceId)
                    .HasConstraintName("FK_Product_CategoryPrice");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.Property(e => e.SaleId)
                    .ValueGeneratedNever()
                    .HasColumnName("SaleID");

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<SaleDetail>(entity =>
            {
                entity.HasKey(e => e.SaleDetailsId);

                entity.Property(e => e.SaleDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("SaleDetailsID");

                entity.Property(e => e.NbProduct).HasColumnName("nbProduct");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SaleId).HasColumnName("SaleID");

                entity.HasOne(d => d.ProductCodeNavigation)
                    .WithMany(p => p.SaleDetails)
                    .HasForeignKey(d => d.ProductCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleDetails_Product");

                entity.HasOne(d => d.Sale)
                    .WithMany(p => p.SaleDetails)
                    .HasForeignKey(d => d.SaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SaleDetails_Sale");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
