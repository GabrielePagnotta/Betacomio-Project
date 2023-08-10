using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class UserAddress
{
    public int UserId { get; set; }

    public int AddressId { get; set; }

    public Guid Rowguid { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
