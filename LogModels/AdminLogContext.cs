using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Betacomio_Project.LogModels;

public partial class AdminLogContext : DbContext
{

    public AdminLogContext(DbContextOptions<AdminLogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<OrderProxy> OrderProxies { get; set; }

    public virtual DbSet<ShoppingCartTemp> ShoppingCartTemps { get; set; }

    public virtual DbSet<UserCredential> UserCredentials { get; set; }

    public virtual DbSet<UserRequestsTemp> UserRequestsTemps { get; set; }

    public virtual DbSet<WishlistTemp> WishlistTemps { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("PK_ErrorLogAdmin");

            entity.ToTable("ErrorLog");

            entity.Property(e => e.ErrorId).HasColumnName("ErrorID");
            entity.Property(e => e.ErrorDescription).HasColumnType("ntext");
            entity.Property(e => e.ErrorMachine).HasMaxLength(50);
            entity.Property(e => e.ErrorOriginClass)
                .HasMaxLength(50)
                .HasColumnName("ErrorOrigin_Class");
            entity.Property(e => e.ErrorOriginMethod)
                .HasMaxLength(50)
                .HasColumnName("ErrorOrigin_Method");
            entity.Property(e => e.ErrorTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<OrderProxy>(entity =>
        {
            entity.HasKey(e => e.GenericId);

            entity.ToTable("OrderProxy");

            entity.Property(e => e.GenericId).HasColumnName("GenericID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.AddressDetail).HasMaxLength(30);
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(15)
                .HasColumnName("Postal_Code");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Region).HasMaxLength(50);
            entity.Property(e => e.SubTotal).HasColumnType("money");
            entity.Property(e => e.TotalPrice).HasColumnType("money");
            entity.Property(e => e.UnitPrice).HasColumnType("money");
        });


        modelBuilder.Entity<ShoppingCartTemp>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId });

            entity.ToTable("ShoppingCartTemp");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.TotalPrice).HasColumnType("money");
            entity.Property(e => e.UnitPrice).HasColumnType("money");
        });

        modelBuilder.Entity<UserCredential>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.BirthYear)
                .HasMaxLength(20)
                .HasColumnName("Birth_Year");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Nationality).HasDefaultValueSql("((3))");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Password_Hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("Password_Salt");
            entity.Property(e => e.Phone).HasMaxLength(25);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(30);
        });

        modelBuilder.Entity<UserRequestsTemp>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.ToTable("UserRequestsTemp");

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Object).HasMaxLength(150);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<WishlistTemp>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId });

            entity.ToTable("WishlistTemp");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
