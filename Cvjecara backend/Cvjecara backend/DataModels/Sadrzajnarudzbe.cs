using System;
using System.Collections.Generic;

namespace Cvjecara_backend.DataModels;

public partial class Sadrzajnarudzbe
{
    public int Id { get; set; }

    public int? CvjId { get; set; }

    public int? BukId { get; set; }

    public int? NarId { get; set; }

    public int Kolicina { get; set; }

    public double Cijena { get; set; }

    public long Column4 { get; set; }

    public virtual Buket? Buk { get; set; }

    public virtual Cvjet? Cvj { get; set; }

    public virtual Narudzba? Nar { get; set; }
}
