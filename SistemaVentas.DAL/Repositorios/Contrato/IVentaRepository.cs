﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.Model;

namespace SistemaVentas.DAL.Repositorios.Contrato
{
    public interface IVentaRepository : IGenericRepository<Ventas>
    {
        Task<Ventas> Registrar(Ventas modelo);
    }
}
