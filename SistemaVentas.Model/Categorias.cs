using System;
using System.Collections.Generic;

namespace SistemaVentas.Model;

public partial class Categorias
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();
}
