using System;
using System.Collections.Generic;

namespace Cvjecara_backend.DataModels;

public partial class Narudzba
{
    public int Id { get; set; }

    public int? UseId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public double Cjena { get; set; }

    public DateOnly? Created { get; set; }

    public virtual ICollection<Sadrzajnarudzbe> Sadrzajnarudzbes { get; set; } = new List<Sadrzajnarudzbe>();

    public virtual User? Use { get; set; }
}
