using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public int? ParentProductCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
