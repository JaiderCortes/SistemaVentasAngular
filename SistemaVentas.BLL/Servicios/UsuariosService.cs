using System;
using System.Collections.Generic;
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
    public class UsuariosService : IUsuariosService
    {
        private readonly IGenericRepository<Usuarios> _usuariosRepositorio;
        private readonly IMapper _mapper;

        public UsuariosService(IGenericRepository<Usuarios> usuariosRepositorio, IMapper mapper)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _mapper = mapper;
        }

        public async Task<List<UsuariosDTO>> Lista()
        {
            try
            {
                var queryUsuarios = await _usuariosRepositorio.Consultar();
                var listaUsuarios = queryUsuarios.Include(rol => rol.IdRolNavigation).ToList();
                return _mapper.Map<List<UsuariosDTO>>(listaUsuarios);
            }
            catch{
                throw;
            }
        }

        public async Task<SesionDTO> ValidarCredenciales(string correo, string clave)
        {
            try
            {
                var queryUsuarios = await _usuariosRepositorio.Consultar(u => u.Correo == correo && u.Clave == clave);
                if (queryUsuarios.FirstOrDefault() == null)
                {
                    throw new TaskCanceledException("El usuario no existe.");
                }
                Usuarios usuarioEncontrado = queryUsuarios.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<SesionDTO>(usuarioEncontrado);
            }catch{
                throw;
            }
        }

        public async Task<UsuariosDTO> Crear(UsuariosDTO modelo)
        {
            try
            {
                var usuarioCreado = await _usuariosRepositorio.Crear(_mapper.Map<Usuarios>(modelo));
                if (usuarioCreado.IdUsuario == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el usuario.");
                }
                var query = await _usuariosRepositorio.Consultar(u => u.IdUsuario == usuarioCreado.IdUsuario);
                usuarioCreado = query.Include(rol => rol.IdRolNavigation).First();
                return _mapper.Map<UsuariosDTO>(usuarioCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(UsuariosDTO modelo)
        {
            try
            {
                var usuarioModelo = _mapper.Map<Usuarios>(modelo);
                var usuarioEncontrado = await _usuariosRepositorio.Obtener(u => u.IdUsuario == usuarioModelo.IdUsuario);
                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("El usuario no existe.");
                }
                usuarioEncontrado.NombreCompleto = usuarioModelo.NombreCompleto;
                usuarioEncontrado.Correo = usuarioModelo.Correo;
                usuarioEncontrado.IdRol = usuarioModelo.IdRol;
                usuarioEncontrado.Clave = usuarioModelo.Clave;
                usuarioEncontrado.Activo = usuarioModelo.Activo;

                bool respuesta =  await _usuariosRepositorio.Editar(usuarioEncontrado);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo guardar la información del usuario.");
                }
                return respuesta;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var usuarioEncontrado = await _usuariosRepositorio.Obtener(u => u.IdUsuario == id);
                if (usuarioEncontrado == null)
                {
                    throw new TaskCanceledException("El usuario no existe.");
                }

                bool respuesta = await _usuariosRepositorio.Eliminar(usuarioEncontrado);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo eliminar el usuario.");
                }
                return respuesta;
            }
            catch
            {
                throw;
            }
        }
    }
}
