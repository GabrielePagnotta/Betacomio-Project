using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class ViewAdminUserRegistry
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? BirthYear { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int Nationality { get; set; }

    public int AddressId { get; set; }

    public string Address { get; set; } = null!;

    public string? AddressDetail { get; set; }

    public string City { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }
}
