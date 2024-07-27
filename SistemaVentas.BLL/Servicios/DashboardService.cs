using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.Model;

namespace SistemaVentas.BLL.Servicios
{
    public class DashboardService : IDashboardService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<Productos> _productosRepositorio;
        private readonly IMapper _mapper;

        public DashboardService(IVentaRepository ventaRepository, IGenericRepository<Productos> productosRepositorio, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _productosRepositorio = productosRepositorio;
            _mapper = mapper;
        }

        private static IQueryable<Ventas> RetornarVentas(IQueryable<Ventas> tablaVentas, int restarCantDias)
        {
            DateTime? ultimaFecha = tablaVentas.OrderByDescending(v => v.FechaRegistro).Select(v => v.FechaRegistro).First();
            ultimaFecha = ultimaFecha.Value.AddDays(restarCantDias);
            return tablaVentas.Where(v => v.FechaRegistro.Value.Date >= ultimaFecha.Value.Date);
        }

        private async Task<int> TotalVentasUltimaSemana()
        {
            int total = 0;
            IQueryable<Ventas> _ventasQuery = await _ventaRepository.Consultar();
            if (_ventasQuery.Count() > 0)
            {
                var tablaVentas = RetornarVentas(_ventasQuery, -7);
                total = tablaVentas.Count();
            }
            return total;
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            decimal resultado = 0;
            IQueryable<Ventas> _ventasQuery = await _ventaRepository.Consultar();
            if (_ventasQuery.Count() > 0)
            {
                var tablaVentas = RetornarVentas(_ventasQuery, -7);
                resultado = tablaVentas.Select(v => v.Total).Sum(v => v.Value);
            }
            return Convert.ToString(resultado, new CultureInfo("es-CO"));
        }

        private async Task<int> TotalProductos()
        {
            IQueryable<Productos> _productosQuery = await _productosRepositorio.Consultar();
            int total = _productosQuery.Count();
            return total;
        }

        private async Task<Dictionary<string, int>> VentasUltimaSemana()
        {
            Dictionary<string, int> resultado = new Dictionary<string, int>();
            IQueryable<Ventas> _ventasQuery = await _ventaRepository.Consultar();
            if (_ventasQuery.Count() > 0)
            {
                var tablaVenta = RetornarVentas(_ventasQuery, -7);
                resultado = tablaVenta
                    .GroupBy(v => v.FechaRegistro.Value.Date)
                    .OrderBy(v => v.Key)
                    .Select(dv => new { Fecha = dv.Key.ToString("dd/MM/yyyy"), Total = dv.Count() })
                    .ToDictionary(keySelector: r => r.Fecha, elementSelector: r => r.Total);
            }
            return resultado;
        }

        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDashboard = new DashboardDTO();
            try
            {
                vmDashboard.TotalVentas = await TotalVentasUltimaSemana();
                vmDashboard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDashboard.TotalProductos = await TotalProductos();

                List<VentasSemanaDTO> listaVentasSemana = new List<VentasSemanaDTO>();
                foreach (KeyValuePair<string, int> item in await VentasUltimaSemana())
                {
                    listaVentasSemana.Add(new VentasSemanaDTO()
                    {
                        Fecha = item.Key,
                        Total = item.Value
                    });
                }

                vmDashboard.VentasUltimaSemana = listaVentasSemana;
            }
            catch
            {
                throw;
            }
            return vmDashboard;
        }
    }
}
