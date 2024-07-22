using System;
using System.Collections.Generic;

namespace SistemaVentas.Model;

public partial class Productos
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public int? Stock { get; set; }

    public decimal? Precio { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public int? IdCategoria { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categorias? IdCategoriaNavigation { get; set; }
}
