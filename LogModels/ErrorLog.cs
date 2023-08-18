using System;
using System.Collections.Generic;

namespace Betacomio_Project.LogModels;

public partial class ErrorLog
{
    public int ErrorId { get; set; }

    public int ErrorCode { get; set; }

    public string ErrorDescription { get; set; } = null!;

    public string ErrorOriginClass { get; set; } = null!;

    public string ErrorOriginMethod { get; set; } = null!;

    public DateTime ErrorTime { get; set; }

    public string ErrorMachine { get; set; } = null!;
}
