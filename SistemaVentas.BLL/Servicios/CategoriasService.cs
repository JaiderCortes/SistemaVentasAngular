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
    public class CategoriasService : ICategoriasService
    {
        private readonly IGenericRepository<Categorias> _categoriasRepositorio;
        private readonly IMapper _mapper;

        public CategoriasService(IGenericRepository<Categorias> categoriasRepositorio, IMapper mapper)
        {
            _categoriasRepositorio = categoriasRepositorio;
            _mapper = mapper;
        }

        public async Task<List<CategoriasDTO>> Lista()
        {
            try
            {
                var listaCategorias = await _categoriasRepositorio.Consultar();
                return _mapper.Map<List<CategoriasDTO>>(listaCategorias.ToList());
            }
            catch
            {
                throw;
            }
        }
    }
}
