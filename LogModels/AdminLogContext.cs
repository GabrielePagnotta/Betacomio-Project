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

            entity.ToTable("Order_Proxy");

            entity.Property(e => e.GenericId).HasColumnName("GenericID");

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
