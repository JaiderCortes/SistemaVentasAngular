using System;
using System.Collections.Generic;

namespace SistemaVentas.Model;

public partial class NumerosDocumento
{
    public int IdNumeroDocumento { get; set; }

    public int UltimoNumero { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
