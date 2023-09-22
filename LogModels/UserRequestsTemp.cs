using System;
using System.Collections.Generic;

namespace Betacomio_Project.LogModels;

public partial class UserRequestsTemp
{
    public int RequestId { get; set; }

    public int? UserId { get; set; }

    public string Object { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[]? Image { get; set; }

    public string Email { get; set; } = null!;
}
