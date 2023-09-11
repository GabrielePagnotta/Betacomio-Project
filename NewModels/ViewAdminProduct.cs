using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class ViewAdminProduct
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string ProductNumber { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string ModelType { get; set; } = null!;

    public decimal StandardCost { get; set; }

    public decimal ListPrice { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public decimal? Weight { get; set; }

    public string Description { get; set; } = null!;

    public byte[]? ThumbnailPhoto { get; set; }

    public string Culture { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
