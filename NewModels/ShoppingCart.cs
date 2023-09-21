using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class ShoppingCart
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime AddedDate { get; set; }

    public Guid Rowguid { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}