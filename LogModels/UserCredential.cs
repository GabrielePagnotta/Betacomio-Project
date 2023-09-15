using System;
using System.Collections.Generic;

namespace Betacomio_Project.LogModels;

public partial class UserCredential
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? BirthYear { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public bool Role { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public int Nationality { get; set; }
}
