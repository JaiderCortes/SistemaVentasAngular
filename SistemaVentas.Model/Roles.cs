using System;
using System.Collections.Generic;

namespace SistemaVentas.Model;

public partial class Roles
{
    public int IdRol { get; set; }

    public string? Nombre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();

    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
