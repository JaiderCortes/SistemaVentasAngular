using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService _categoriasService;

        public CategoriasController(ICategoriasService categoriasService)
        {
            _categoriasService = categoriasService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<CategoriasDTO>>();
            try
            {
                response.Status = true;
                response.Value = await _categoriasService.Lista();
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
