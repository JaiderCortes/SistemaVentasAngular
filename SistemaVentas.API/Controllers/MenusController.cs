using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.API.Utilidad;
using SistemaVentas.BLL.Servicios;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenusService _menusService;

        public MenusController(IMenusService menusService)
        {
            _menusService = menusService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            var response = new Response<List<MenusDTO>>();
            try
            {
                response.Status = true;
                response.Value = await _menusService.Lista(idUsuario);
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
