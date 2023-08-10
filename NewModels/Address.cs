using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class Address
{
    public int AddressId { get; set; }

    public string Address1 { get; set; } = null!;

    public string? AddressDetail { get; set; }

    public string City { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
}
