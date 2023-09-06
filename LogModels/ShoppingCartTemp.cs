using System;
using System.Collections.Generic;

namespace Betacomio_Project.LogModels;

public partial class ShoppingCartTemp
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public DateTime? AddedDate { get; set; }

    public Guid? Rowguid { get; set; }
}
