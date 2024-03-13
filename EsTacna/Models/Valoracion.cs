using System;
using System.Collections.Generic;

namespace EsTacna.Models;

public partial class Valoracion
{
    public int Id { get; set; }

    public int EstablecimientoId { get; set; }

    public int UsuarioId { get; set; }

    public string? Comentario { get; set; }

    public int Calificacion { get; set; }

    public virtual EstablecimientoSalud Establecimiento { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
