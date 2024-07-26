using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.DTO;

namespace SistemaVentas.BLL.Servicios.Contrato
{
    public interface IUsuariosService
    {
        Task<List<UsuariosDTO>> Lista();

        Task<SesionDTO> ValidarCredenciales(string correo, string clave);

        Task<UsuariosDTO> Crear(UsuariosDTO modelo);

        Task<bool> Editar(UsuariosDTO modelo);

        Task<bool> Eliminar(int id);
    }
}
