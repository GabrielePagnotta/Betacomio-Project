using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Betacomio_Project.LogModels;

public partial class AdminLogContext : DbContext
{
    public AdminLogContext()
    {
    }

    public AdminLogContext(DbContextOptions<AdminLogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<UserCredential> UserCredentials { get; set; }


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
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Password_Salt");
            entity.Property(e => e.Phone).HasMaxLength(25);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
