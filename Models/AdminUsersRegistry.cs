using System;
using System.Collections.Generic;

namespace Betacomio_Project.Models;

public partial class AdminUsersRegistry
{
    public int AddressId { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string City { get; set; } = null!;

    public string StateProvince { get; set; } = null!;

    public string CountryRegion { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public string? Phone { get; set; }
}
