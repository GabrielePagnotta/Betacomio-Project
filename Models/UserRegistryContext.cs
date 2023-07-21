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

    public virtual DbSet<User> Users { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Nationality).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasMaxLength(120);
            entity.Property(e => e.PasswordSalt).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
