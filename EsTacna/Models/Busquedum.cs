using System;
using System.Collections.Generic;

namespace EsTacna.Models;

public partial class Busquedum
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string TerminoBusqueda { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
