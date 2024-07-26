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
    public class RolesService : IRolesService
    {
        private readonly IGenericRepository<Roles> _rolesRepositorio;
        private readonly IMapper _mapper;

        public RolesService(IGenericRepository<Roles> rolesRepositorio, IMapper mapper)
        {
            _rolesRepositorio = rolesRepositorio;
            _mapper = mapper;
        }

        public async Task<List<RolesDTO>> Lista()
        {
            try
            {
                var listaRoles = await _rolesRepositorio.Consultar();
                return _mapper.Map<List<RolesDTO>>(listaRoles.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
