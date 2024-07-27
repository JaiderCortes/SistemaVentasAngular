using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.DTO;

namespace SistemaVentas.BLL.Servicios.Contrato
{
    public interface IVentasService
    {
        Task<VentasDTO> Registrar(VentasDTO model);

        Task<List<VentasDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin);

        Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin);

    }
}
