using System;
using System.Collections.Generic;

namespace Betacomio_Project.Models;

public partial class AdminProductsView
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string ProductNumber { get; set; } = null!;

    public string? Color { get; set; }

    public string? Size { get; set; }

    public decimal? Weight { get; set; }

    public decimal StandardCost { get; set; }

    public decimal ListPrice { get; set; }

    public DateTime ModifiedDate { get; set; }

    public string? ThumbnailPhotoFileName { get; set; }

    public string ProductType { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string ModelType { get; set; } = null!;

    public string? CatalogDescription { get; set; }

    public string Culture { get; set; } = null!;
}
