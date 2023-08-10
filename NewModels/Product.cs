using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

/// <summary>
/// Productss sold or used in the manfacturing of sold Productss.
/// </summary>
public partial class Product
{
    public int ProductId { get; set; }

    /// <summary>
    /// Name of the Products.
    /// </summary>
    public string Name { get; set; } = null!;

    public string ProductNumber { get; set; } = null!;

    /// <summary>
    /// Products color.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Standard cost of the Products.
    /// </summary>
    public decimal StandardCost { get; set; }

    /// <summary>
    /// Selling price.
    /// </summary>
    public decimal ListPrice { get; set; }

    /// <summary>
    /// Products size.
    /// </summary>
    public string? Size { get; set; }

    /// <summary>
    /// Products weight.
    /// </summary>
    public decimal? Weight { get; set; }

    public int? ProductCategoryId { get; set; }

    public int? ProductModelId { get; set; }

    /// <summary>
    /// Date the Products was available for sale.
    /// </summary>
    public DateTime SellStartDate { get; set; }

    /// <summary>
    /// Date the Products was no longer available for sale.
    /// </summary>
    public DateTime? SellEndDate { get; set; }

    /// <summary>
    /// Date the Products was discontinued.
    /// </summary>
    public DateTime? DiscontinuedDate { get; set; }

    /// <summary>
    /// Small image of the Products.
    /// </summary>
    public byte[]? ThumbNailPhoto { get; set; }

    /// <summary>
    /// Small image file name.
    /// </summary>
    public string? ThumbnailPhotoFileName { get; set; }

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ProductModel? ProductModel { get; set; }

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
