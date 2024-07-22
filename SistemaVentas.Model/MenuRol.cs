using System;
using System.Collections.Generic;

namespace SistemaVentas.Model;

public partial class MenuRol
{
    public int IdMenuRol { get; set; }

    public int? IdMenu { get; set; }

    public int? IdRol { get; set; }

    public virtual Menus? IdMenuNavigation { get; set; }

    public virtual Roles? IdRolNavigation { get; set; }
}
