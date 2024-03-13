using System;
using System.Collections.Generic;

namespace EsTacna.Models;

public partial class Ep
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<EpsEstablecimientoSalud> EpsEstablecimientoSaluds { get; set; } = new List<EpsEstablecimientoSalud>();
}
