using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using SistemaVentas.DTO;
using SistemaVentas.Model;

namespace SistemaVentas.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Roles
            CreateMap<Roles, RolesDTO>().ReverseMap();
            #endregion Roles

            #region Menus
            CreateMap<Menus, MenusDTO>().ReverseMap();
            #endregion Menus

            #region Usuarios
            CreateMap<Usuarios, UsuariosDTO>().ForMember(destino =>
                destino.DescripcionRol,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
            ).ForMember(destino =>
                destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == true ? 1 : 0)
            );

            CreateMap<Usuarios, SesionDTO>().ForMember(destino =>
                destino.DescripcionRol,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
            );

            CreateMap<UsuariosDTO, Usuarios>().ForMember(destino =>
                destino.IdRolNavigation,
                opt => opt.Ignore()
            ).ForMember(destino =>
                destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == 1)
            );
            #endregion Usuarios

            #region Categorias
            CreateMap<Categorias, CategoriasDTO>().ReverseMap();
            #endregion Categorias

            #region Productos
            CreateMap<Productos, ProductosDTO>().ForMember(destino =>
                destino.DescripcionCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Nombre)
            ).ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-CO")))
            ).ForMember(destino =>
                destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == true ? 1 : 0)
            );

            CreateMap<ProductosDTO, Productos>().ForMember(destino =>
                destino.IdCategoriaNavigation,
                opt => opt.Ignore()
            ).ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-CO")))
            ).ForMember(destino =>
                destino.Activo,
                opt => opt.MapFrom(origen => origen.Activo == 1)
            );
            #endregion Productos

            #region Ventas
            CreateMap<Ventas, VentasDTO>().ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO")))
            ).ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            );

            CreateMap<VentasDTO, Ventas>().ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-CO")))
            );
            #endregion Ventas

            #region DetalleVenta
            CreateMap<DetalleVenta, DetalleVentaDTO>().ForMember(destino =>
                destino.DescripcionProducto,
                opt => opt.MapFrom(origin => origin.IdProductoNavigation.Nombre)
            ).ForMember(destino =>
                destino.PrecioTexto,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Precio.Value, new CultureInfo("es-CO")))
            ).ForMember(destino =>
                destino.TotalTexto,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO")))
            );

            CreateMap<DetalleVentaDTO, DetalleVenta>().ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.PrecioTexto, new CultureInfo("es-CO")))
            ).ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TotalTexto, new CultureInfo("es-CO")))
            );

            #endregion DetalleVenta

            #region Reporte
            CreateMap<DetalleVenta, ReporteDTO>().ForMember(destino =>
                destino.FechaRegistro,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            ).ForMember(destino =>
                destino.NumeroDocumento,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.NumeroDocumento)
            ).ForMember(destino =>
                destino.TipoPago,
                opt => opt.MapFrom(origen => origen.IdVentaNavigation.TipoPago)
            ).ForMember(destino =>
                destino.TotalVenta,
                opt => opt.MapFrom(origin => Convert.ToString(origin.IdVentaNavigation.Total.Value, new CultureInfo("es-CO")))
            ).ForMember(destino =>
                destino.Producto,
                opt => opt.MapFrom(origen => origen.IdProductoNavigation.Nombre)
            ).ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Precio.Value, new CultureInfo("es-CO")))
            ).ForMember(destino =>
                destino.Total,
                opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-CO")))
            );
            #endregion Reporte
        }
    }
}
