using System;
using System.Collections.Generic;

namespace Cvjecara_backend.DataModels;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? Title { get; set; }

    public string password { get; set; } = null!;


    public DateOnly? DateOfBirth { get; set; }

    public bool VerifiedAdmin { get; set; }

    public DateOnly? Created { get; set; }

    public DateOnly? Updated { get; set; }

    public virtual ICollection<Narudzba> Narudzbas { get; set; } = new List<Narudzba>();
}
