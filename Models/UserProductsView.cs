using System;
using System.Collections.Generic;

namespace Betacomio_Project.Models;

public partial class UserProductsView
{
    public string Name { get; set; } = null!;

    public string ProductType { get; set; } = null!;

    public string ModelType { get; set; } = null!;

    public decimal ListPrice { get; set; }

    public string? Color { get; set; }

    public string? Size { get; set; }

    public decimal? Weight { get; set; }

    public string Description { get; set; } = null!;

    public byte[]? ThumbNailPhoto { get; set; }

    public string Culture { get; set; } = null!;
}
