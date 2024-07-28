using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentasService _ventasService;

        public VentasController(IVentasService ventasService)
        {
            _ventasService = ventasService;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentasDTO venta)
        {
            var response = new Response<VentasDTO>();
            try
            {
                response.Status = true;
                response.Value = await _ventasService.Registrar(venta);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string? buscarPor, string? numVenta, string? fechaInicio, string? fechaFin)
        {
            var response = new Response<List<VentasDTO>>();
            numVenta = numVenta is null ? "" : numVenta;
            fechaFin = fechaInicio is null ? "" : fechaInicio;
            fechaFin = fechaFin is null ? "" : fechaFin;
                
            try
            {
                response.Status = true;
                response.Value = await _ventasService.Historial(buscarPor, numVenta, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            var response = new Response<List<ReporteDTO>>();
                
            try
            {
                response.Status = true;
                response.Value = await _ventasService.Reporte(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Msg = ex.Message;
            }
            return Ok(response);
        }


    }
}
