using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.Model;

namespace SistemaVentas.BLL.Servicios
{
    public class VentasService : IVentasService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IGenericRepository<DetalleVenta> _detalleVentaRepositorio;
        private readonly IMapper _mapper;

        public VentasService(IVentaRepository ventaRepository, IGenericRepository<DetalleVenta> detalleVentaRepositorio, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _detalleVentaRepositorio = detalleVentaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentasDTO> Registrar(VentasDTO model)
        {
            try
            {
                var ventaGenerada = await _ventaRepository.Registrar(_mapper.Map<Ventas>(model));
                if (ventaGenerada.IdVenta == 0)
                {
                    throw new TaskCanceledException("No se pudo crear la venta.");
                }
                return _mapper.Map<VentasDTO>(ventaGenerada);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<VentasDTO>> Historial(string buscarPor, string numeroVenta, string fechaInicio, string fechaFin)
        {
            IQueryable<Ventas> query = await _ventaRepository.Consultar();
            var listaResultado = new List<Ventas>();
            try
            {
                if (buscarPor == "Fecha") //Fecha
                {
                    DateTime fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                    DateTime fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                    listaResultado = await query.Where(v =>
                        v.FechaRegistro.Value.Date >= fechInicio.Date &&
                        v.FechaRegistro.Value.Date <= fechFin.Date
                    ).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                }
                else //Numero de venta
                {
                    listaResultado = await query.Where(v => v.NumeroDocumento == numeroVenta).Include(dv => dv.DetalleVenta)
                    .ThenInclude(p => p.IdProductoNavigation).ToListAsync();
                }
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<VentasDTO>>(listaResultado);
        }

        public async Task<List<ReporteDTO>> Reporte(string fechaInicio, string fechaFin)
        {
            IQueryable<DetalleVenta> query = await _detalleVentaRepositorio.Consultar();
            var listaResultado = new List<DetalleVenta>();
            try
            {
                DateTime fechInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", new CultureInfo("es-CO"));
                DateTime fechFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", new CultureInfo("es-CO"));

                listaResultado = await query
                    .Include(p => p.IdProductoNavigation)
                    .Include(v => v.IdVentaNavigation)
                    .Where(dv =>
                        dv.IdVentaNavigation.FechaRegistro.Value.Date >= fechInicio.Date &&
                        dv.IdVentaNavigation.FechaRegistro.Value.Date <= fechInicio.Date
                    ).ToListAsync();
            }
            catch
            {
                throw;
            }
            return _mapper.Map<List<ReporteDTO>>(listaResultado);
        }
    }
}
