using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIForBD.Models
{
    public partial class praktikaContext : DbContext
    {
        public praktikaContext()
        {
        }

        public praktikaContext(DbContextOptions<praktikaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Meal> Meals { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_by");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");

                entity.Property(e => e.DeletedBy).HasColumnName("Deleted_by");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.ToTable("Meal");

                entity.Property(e => e.MealId).HasColumnName("MealID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_by");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");

                entity.Property(e => e.DeletedBy).HasColumnName("Deleted_by");

                entity.Property(e => e.Kkal).HasColumnName("KKAL");

                entity.Property(e => e.MealDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.MealName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Meals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Meal__UserID__3B75D760");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_by");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");

                entity.Property(e => e.DeletedBy).HasColumnName("Deleted_by");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Season)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Created_at");

                entity.Property(e => e.CreatedBy).HasColumnName("Created_by");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("Deleted_at");

                entity.Property(e => e.DeletedBy).HasColumnName("Deleted_by");

                entity.Property(e => e.Email)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasMany(d => d.Categories)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserCategory",
                        l => l.HasOne<Category>().WithMany().HasForeignKey("CategoryId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__User_Cate__Categ__44FF419A"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__User_Cate__UserI__440B1D61"),
                        j =>
                        {
                            j.HasKey("UserId", "CategoryId").HasName("PK__User_Cat__B6185F0EA5F8BFFA");

                            j.ToTable("User_Category");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("CategoryId").HasColumnName("CategoryID");
                        });

                entity.HasMany(d => d.Products)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserProduct",
                        l => l.HasOne<Product>().WithMany().HasForeignKey("ProductId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__User_Prod__Produ__412EB0B6"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__User_Prod__UserI__403A8C7D"),
                        j =>
                        {
                            j.HasKey("UserId", "ProductId").HasName("PK__User_Pro__DCC800C288CCA218");

                            j.ToTable("User_Product");

                            j.IndexerProperty<int>("UserId").HasColumnName("UserID");

                            j.IndexerProperty<int>("ProductId").HasColumnName("ProductID");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
