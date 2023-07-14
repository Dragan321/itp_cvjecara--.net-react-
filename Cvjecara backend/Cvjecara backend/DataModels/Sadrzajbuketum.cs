using System;
using System.Collections.Generic;

namespace Cvjecara_backend.DataModels;

public partial class Sadrzajbuketum
{
    public int Id { get; set; }

    public int? BukId { get; set; }

    public int? CvjId { get; set; }

    public int Kolicina { get; set; }

    public virtual Buket? Buk { get; set; }

    public virtual Cvjet? Cvj { get; set; }
}
