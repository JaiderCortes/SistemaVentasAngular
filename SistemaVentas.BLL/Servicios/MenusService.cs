using System;
using System.Collections.Generic;
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
    public class MenusService : IMenusService
    {
        private readonly IGenericRepository<Usuarios> _usuariosRepositorio;
        private readonly IGenericRepository<MenuRol> _menuRolRepositorio;
        private readonly IGenericRepository<Menus> _menusRepositorio;
        private readonly IMapper _mapper;

        public MenusService(IGenericRepository<Usuarios> usuariosRepositorio, IGenericRepository<MenuRol> menuRolRepositorio, IGenericRepository<Menus> menusRepositorio, IMapper mapper)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _menuRolRepositorio = menuRolRepositorio;
            _menusRepositorio = menusRepositorio;
            _mapper = mapper;
        }

        public async Task<List<MenusDTO>> Lista(int id)
        {
            IQueryable<Usuarios> tbUsuarios = await _usuariosRepositorio.Consultar(u => u.IdUsuario == id);
            IQueryable<MenuRol> tbMenuRol = await _menuRolRepositorio.Consultar();
            IQueryable<Menus> tbMenus = await _menusRepositorio.Consultar();

            try
            {
                IQueryable<Menus> tbResultado = (from u in tbUsuarios
                    join mr in tbMenuRol on u.IdRol equals mr.IdRol
                    join m in tbMenus on mr.IdMenu equals m.IdMenu
                    select m).AsQueryable();
                var listaMenus = tbResultado.ToList();
                return _mapper.Map<List<MenusDTO>>(listaMenus);
            }
            catch
            {
                throw;
            }
        }
    }
}
