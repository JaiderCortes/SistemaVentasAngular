using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService _productosService;

        public ProductosController(IProductosService productosService)
        {
            _productosService = productosService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<ProductosDTO>>();
            try
            {
                response.Status = true;
                response.Value = await _productosService.Lista();
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ProductosDTO producto)
        {
            var response = new Response<ProductosDTO>();
            try
            {
                response.Status = true;
                response.Value = await _productosService.Crear(producto);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ProductosDTO producto)
        {
            var response = new Response<bool>();
            try
            {
                response.Status = true;
                response.Value = await _productosService.Editar(producto);
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = new Response<bool>();
            try
            {
                response.Status = true;
                response.Value = await _productosService.Eliminar(id);
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
