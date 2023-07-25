using System;
using System.Collections.Generic;

namespace Betacomio_Project.Models;

public partial class UserAddress
{
    public int AddressId { get; set; }

    public string Address { get; set; } = null!;

    public string AddressDetail { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? StateProvince { get; set; }

    public string Country { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public Guid Rowguid { get; set; }
}
