using System;
using System.Collections.Generic;

namespace Betacomio_Project.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int? Age { get; set; }

    public string Email { get; set; } = null!;

    public int? Phone { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string? Nationality { get; set; }

    public Guid Rowguid { get; set; }


}
