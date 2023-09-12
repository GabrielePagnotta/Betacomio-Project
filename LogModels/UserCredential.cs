using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Betacomio_Project.LogModels;

public partial class UserCredential
{
    public int UserId { get; set; }
    [MinLength(5)]
    public string? Username { get; set; }
    [MinLength(4)]
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
