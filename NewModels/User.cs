using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class User
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

    public Guid Rowguid { get; set; }

    public virtual LanguageEnum NationalityNavigation { get; set; } = null!;

    public virtual ICollection<OrderHeader> OrderHeaders { get; set; } = new List<OrderHeader>();

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserRequest> UserRequests { get; set; } = new List<UserRequest>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
