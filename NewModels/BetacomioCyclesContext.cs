using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Betacomio_Project.NewModels;

public partial class BetacomioCyclesContext : DbContext
{

    public BetacomioCyclesContext(DbContextOptions<BetacomioCyclesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<LanguageEnum> LanguageEnums { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderHeader> OrderHeaders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }

    public virtual DbSet<ProductModel> ProductModels { get; set; }

    public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserRequest> UserRequests { get; set; }

    public virtual DbSet<ViewAdminProduct> ViewAdminProducts { get; set; }

    public virtual DbSet<ViewAdminUserRegistry> ViewAdminUserRegistries { get; set; }

    public virtual DbSet<ViewUserProduct> ViewUserProducts { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK_UserAddress");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Address1)
                .HasMaxLength(60)
                .HasColumnName("Address");
            entity.Property(e => e.AddressDetail).HasMaxLength(30);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(15)
                .HasColumnName("Postal_Code");
            entity.Property(e => e.Region).HasMaxLength(50);
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        modelBuilder.Entity<LanguageEnum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Language__3214EC075AA674E9");

            entity.ToTable("LanguageEnum");

            entity.HasIndex(e => e.Language, "UQ__Language__C3D59250FC140883").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Language)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(6)
                .IsFixedLength();
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.OrderDetailId });

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDetailId)
                .ValueGeneratedOnAdd()
                .HasColumnName("OrderDetailID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.TotalPrice).HasColumnType("money");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_OrderHeader");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetail_Products");
        });

        modelBuilder.Entity<OrderHeader>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("OrderHeader");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.SubTotal).HasColumnType("money");

            entity.HasOne(d => d.Address).WithMany(p => p.OrderHeaders)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderHeader_Addresses");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderHeaders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderHeader_Users");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Product_ProductID");

            entity.ToTable(tb => tb.HasComment("Productss sold or used in the manfacturing of sold Productss."));

            entity.HasIndex(e => e.Name, "AK_Product_Name").IsUnique();

            entity.HasIndex(e => e.ProductNumber, "AK_Product_ProductNumber").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_Product_rowguid").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Color)
                .HasMaxLength(15)
                .HasComment("Products color.");
            entity.Property(e => e.DiscontinuedDate)
                .HasComment("Date the Products was discontinued.")
                .HasColumnType("datetime");
            entity.Property(e => e.ListPrice)
                .HasComment("Selling price.")
                .HasColumnType("money");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("Date and time the record was last updated.")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Name of the Products.");
            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.ProductNumber).HasMaxLength(25);
            entity.Property(e => e.Rowguid)
                .HasDefaultValueSql("(newid())")
                .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.")
                .HasColumnName("rowguid");
            entity.Property(e => e.SellEndDate)
                .HasComment("Date the Products was no longer available for sale.")
                .HasColumnType("datetime");
            entity.Property(e => e.SellStartDate)
                .HasComment("Date the Products was available for sale.")
                .HasColumnType("datetime");
            entity.Property(e => e.Size)
                .HasMaxLength(5)
                .HasComment("Products size.");
            entity.Property(e => e.StandardCost)
                .HasComment("Standard cost of the Products.")
                .HasColumnType("money");
            entity.Property(e => e.ThumbNailPhoto).HasComment("Small image of the Products.");
            entity.Property(e => e.ThumbnailPhotoFileName)
                .HasMaxLength(50)
                .HasComment("Small image file name.");
            entity.Property(e => e.Weight)
                .HasComment("Products weight.")
                .HasColumnType("decimal(8, 2)");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_Products_ProductCategory");

            entity.HasOne(d => d.ProductModel).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductModelId)
                .HasConstraintName("FK_Products_ProductModel");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK_ProductCategory_ProductCategoryID");

            entity.ToTable("ProductCategory");

            entity.HasIndex(e => e.Name, "AK_ProductCategory_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_ProductCategory_rowguid").IsUnique();

            entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ParentProductCategoryId).HasColumnName("ParentProductCategoryID");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity.HasKey(e => e.ProductDescriptionId).HasName("PK_ProductDescription_ProductDescriptionID");

            entity.ToTable("ProductDescription");

            entity.HasIndex(e => e.Rowguid, "AK_ProductDescription_rowguid").IsUnique();

            entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.HasKey(e => e.ProductModelId).HasName("PK_ProductModel_ProductModelID");

            entity.ToTable("ProductModel");

            entity.HasIndex(e => e.Name, "AK_ProductModel_Name").IsUnique();

            entity.HasIndex(e => e.Rowguid, "AK_ProductModel_rowguid").IsUnique();

            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.CatalogDescription).HasColumnType("xml");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
        });

        modelBuilder.Entity<ProductModelProductDescription>(entity =>
        {
            entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture }).HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

            entity.ToTable("ProductModelProductDescription");

            entity.HasIndex(e => e.Rowguid, "AK_ProductModelProductDescription_rowguid").IsUnique();

            entity.Property(e => e.ProductModelId).HasColumnName("ProductModelID");
            entity.Property(e => e.ProductDescriptionId).HasColumnName("ProductDescriptionID");
            entity.Property(e => e.Culture)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");

            entity.HasOne(d => d.ProductDescription).WithMany(p => p.ProductModelProductDescriptions)
                .HasForeignKey(d => d.ProductDescriptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductModelProductDescription_ProductDescription");

            entity.HasOne(d => d.ProductModel).WithMany(p => p.ProductModelProductDescriptions)
                .HasForeignKey(d => d.ProductModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductModelProductDescription_ProductModel");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId });

            entity.ToTable("ShoppingCart");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.TotalPrice).HasColumnType("money");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ShoppingC__Produ__2B0A656D");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShoppingC__UserI__2A164134");
        });

        modelBuilder.Entity<User>(entity =>
        {
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
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(30);

            entity.HasOne(d => d.NationalityNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Nationality)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_LanguageEnum");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.AddressId }).HasName("PK_UserAddress_1");

            entity.ToTable("UserAddress");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");

            entity.HasOne(d => d.Address).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAddress_Addresses");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAddress_Users");
        });

        modelBuilder.Entity<UserRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId);

            entity.Property(e => e.RequestId).HasColumnName("RequestID");
            entity.Property(e => e.RequestBody)
                .HasColumnType("text")
                .HasColumnName("Request_Body");
            entity.Property(e => e.RequestObject)
                .HasMaxLength(150)
                .HasColumnName("Request_Object");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRequests_Users");
        });

        modelBuilder.Entity<ViewAdminProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_AdminProducts");

            entity.Property(e => e.Color).HasMaxLength(15);
            entity.Property(e => e.Culture)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.ListPrice).HasColumnType("money");
            entity.Property(e => e.ModelType).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductNumber).HasMaxLength(25);
            entity.Property(e => e.ProductType).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(5);
            entity.Property(e => e.StandardCost).HasColumnType("money");
            entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<ViewAdminUserRegistry>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_Admin_UserRegistry");

            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.AddressDetail).HasMaxLength(30);
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.BirthYear)
                .HasMaxLength(20)
                .HasColumnName("Birth_Year");
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(25);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(15)
                .HasColumnName("Postal_Code");
            entity.Property(e => e.Region).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<ViewUserProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_UserProducts");

            entity.Property(e => e.Color).HasMaxLength(15);
            entity.Property(e => e.Culture)
                .HasMaxLength(6)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.ListPrice).HasColumnType("money");
            entity.Property(e => e.ModelType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductType).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(5);
            entity.Property(e => e.Weight).HasColumnType("decimal(8, 2)");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductId });

            entity.ToTable("Wishlist");

            entity.Property(e => e.AddedDate).HasColumnType("datetime");
            entity.Property(e => e.Rowguid).HasColumnName("rowguid");

            entity.HasOne(d => d.Product).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Wishlist__Produc__2739D489");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wishlist__UserId__2645B050");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
