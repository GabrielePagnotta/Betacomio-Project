using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class OrderHeader
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int AddressId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal SubTotal { get; set; }

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual User Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
