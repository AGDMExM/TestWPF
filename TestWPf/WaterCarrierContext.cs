using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestWPF.Model;

namespace TestWPF
{
    public partial class WaterCarrierContext : DbContext
    {
        public WaterCarrierContext()
        {
        }

        public WaterCarrierContext(DbContextOptions<WaterCarrierContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Division> Divisions { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-FHBQ5CC;Database=water_carrier;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Division>(entity =>
            {
                entity.ToTable("divisions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDirector).HasColumnName("idDirector");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.IdDirectorNavigation)
                    .WithMany(p => p.Divisions)
                    .HasForeignKey(d => d.IdDirector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__divisions__idDir__0C85DE4D");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Gender)
                    .HasMaxLength(7)
                    .HasColumnName("gender");

                entity.Property(e => e.IdDivision).HasColumnName("idDivision");

                entity.Property(e => e.MiddleName).HasColumnName("middle_name");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Surname).HasColumnName("surname");

                entity.HasOne(d => d.IdDivisionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdDivision)
                    .HasConstraintName("FK__employees__idDiv__160F4887");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__idEmploy__0F624AF8");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tags");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasMany(d => d.IdOrders)
                    .WithMany(p => p.IdTags)
                    .UsingEntity<Dictionary<string, object>>(
                        "TagsForOrder",
                        l => l.HasOne<Order>().WithMany().HasForeignKey("IdOrder").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tags_for___idOrd__151B244E"),
                        r => r.HasOne<Tag>().WithMany().HasForeignKey("IdTag").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__tags_for___idTag__14270015"),
                        j =>
                        {
                            j.HasKey("IdTag", "IdOrder").HasName("PK__tags_for__EE8542D7B4E9873C");

                            j.ToTable("tags_for_orders");

                            j.IndexerProperty<int>("IdTag").HasColumnName("idTag");

                            j.IndexerProperty<int>("IdOrder").HasColumnName("idOrder");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
