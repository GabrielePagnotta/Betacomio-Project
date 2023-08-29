using System;
using System.Collections.Generic;

namespace Betacomio_Project.NewModels;

public partial class LanguageEnum
{
    public int Id { get; set; }

    public string Language { get; set; } = null!;

    public string LanguageCode { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
