using System;
using System.Collections.Generic;

namespace EsTacna.Models;

public partial class EstablecimientoSalud
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public decimal Longitud { get; set; }

    public decimal Latitud { get; set; }

    public string? Descripcion { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<EpsEstablecimientoSalud> EpsEstablecimientoSaluds { get; set; } = new List<EpsEstablecimientoSalud>();

    public virtual ICollection<Valoracion> Valoracions { get; set; } = new List<Valoracion>();
}
