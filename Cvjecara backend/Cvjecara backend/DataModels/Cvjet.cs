using System;
using System.Collections.Generic;

namespace Cvjecara_backend.DataModels;

public partial class Cvjet
{
    public int Id { get; set; }

    public string Naziv { get; set; } = null!;

    public int Kolicina { get; set; }

    public double Cijena { get; set; }

    public string? Opis { get; set; }

    public string? Slika { get; set; } = null!;

    public DateOnly? Created { get; set; }

    public DateOnly? Updated { get; set; }

    public virtual ICollection<Sadrzajbuketum> Sadrzajbuketa { get; set; } = new List<Sadrzajbuketum>();

    public virtual ICollection<Sadrzajnarudzbe> Sadrzajnarudzbes { get; set; } = new List<Sadrzajnarudzbe>();
}
