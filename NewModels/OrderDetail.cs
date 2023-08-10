using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int OrderDetailId { get; set; }

    public short OrderQty { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual OrderHeader Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
