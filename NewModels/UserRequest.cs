using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class UserRequest
{
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public string RequestObject { get; set; } = null!;

    public string RequestBody { get; set; } = null!;

    public byte[]? Image { get; set; }

    public virtual User User { get; set; } = null!;
}
