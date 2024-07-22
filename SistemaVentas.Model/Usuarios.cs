using System;
using System.Collections.Generic;

namespace SistemaVentas.Model;

public partial class Usuarios
{
    public int IdUsuario { get; set; }

    public string? NombreCompleto { get; set; }

    public string? Correo { get; set; }

    public string? Clave { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdRol { get; set; }

    public virtual Roles? IdRolNavigation { get; set; }
}
