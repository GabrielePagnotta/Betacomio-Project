using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class Wishlist
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public DateTime AddedDate { get; set; }

    public Guid Rowguid { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
