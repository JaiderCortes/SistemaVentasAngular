using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            _usuariosService = usuariosService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var response = new Response<List<UsuariosDTO>>();
            try
            {
                response.Status = true;
                response.Value = await _usuariosService.Lista();
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Msg = ex.Message;
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] LoginDTO login)
        {
            var response = new Response<SesionDTO>();
            try
            {
                response.Status = true;
                response.Value = await _usuariosService.ValidarCredenciales(login.Correo, login.Clave);
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
        public async Task<IActionResult> Guardar([FromBody] UsuariosDTO usuario)
        {
            var response = new Response<UsuariosDTO>();
            try
            {
                response.Status = true;
                response.Value = await _usuariosService.Crear(usuario);
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
        public async Task<IActionResult> Editar([FromBody] UsuariosDTO usuario)
        {
            var response = new Response<bool>();
            try
            {
                response.Status = true;
                response.Value = await _usuariosService.Editar(usuario);
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
                response.Value = await _usuariosService.Eliminar(id);
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
