using System;
using System.Collections.Generic;

namespace Betacomio_Project.LogModels;

public partial class UserRequestsTemp
{
    public int RequestId { get; set; }

    public int UserId { get; set; }

    public string RequestObject { get; set; } = null!;

    public string RequestBody { get; set; } = null!;

    public byte[]? Image { get; set; }
}
