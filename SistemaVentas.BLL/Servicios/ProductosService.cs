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
    public class ProductosService : IProductosService
    {
        private readonly IGenericRepository<Productos> _productosRepositorio;
        private readonly IMapper _mapper;

        public ProductosService(IGenericRepository<Productos> productosRepositorio, IMapper mapper)
        {
            _productosRepositorio = productosRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductosDTO>> Lista()
        {
            try
            {
                var queryProducto = await _productosRepositorio.Consultar();
                var listaProductos = queryProducto.Include(cat => cat.IdCategoriaNavigation).ToList();
                return _mapper.Map<List<ProductosDTO>>(listaProductos).ToList();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ProductosDTO> Crear(ProductosDTO modelo)
        {
            try
            {
                var productoCreado = await _productosRepositorio.Crear(_mapper.Map<Productos>(modelo));
                if (productoCreado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el producto");
                }
                return _mapper.Map<ProductosDTO>(productoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(ProductosDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Productos>(modelo);
                var productoEncontrado = await _productosRepositorio.Obtener(u =>
                    u.IdProducto == productoModelo.IdProducto
                ) ?? throw new TaskCanceledException("El producto no existe.");

                productoEncontrado.Nombre = productoModelo.Nombre;
                productoEncontrado.IdCategoria = productoModelo.IdCategoria;
                productoEncontrado.Stock = productoModelo.Stock;
                productoEncontrado.Precio = productoModelo.Precio;
                productoEncontrado.Activo = productoModelo.Activo;

                bool respuesta = await _productosRepositorio.Editar(productoEncontrado);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo guardar la información del producto.");
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
                var productoEncontrado = await _productosRepositorio.Obtener(p => p.IdProducto == id);
                if (productoEncontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe.");
                }
                bool respuesta = await _productosRepositorio.Eliminar(productoEncontrado);
                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo eliminar el producto.");
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
