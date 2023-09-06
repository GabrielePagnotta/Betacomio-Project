using System;
using System.Collections.Generic;

namespace Betacomio_Project.LogModels;

public partial class WishlistTemp
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime? AddedDate { get; set; }

    public Guid? Rowguid { get; set; }
}
