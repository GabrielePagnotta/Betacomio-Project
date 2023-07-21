using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Betacomio_Project.Models;

public partial class UserRegistryContext : DbContext
{
    public UserRegistryContext()
    {
    }

    public UserRegistryContext(DbContextOptions<UserRegistryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PurchaseUser> PurchaseUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=UserRegistry;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PurchaseUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PurchaseUser");

            entity.Property(e => e.PurchaseId).HasColumnName("PurchaseID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.BirthYear)
                .HasColumnType("date")
                .HasColumnName("Birth_Year");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Nationality).HasMaxLength(20);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(120)
                .HasColumnName("Password_Hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(20)
                .HasColumnName("Password_Salt");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.Surname).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(30);
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId);

            entity.ToTable("UserAddress");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.AddressDetail).HasMaxLength(30);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(30);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(30)
                .HasColumnName("Postal_Code");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.StateProvince).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
