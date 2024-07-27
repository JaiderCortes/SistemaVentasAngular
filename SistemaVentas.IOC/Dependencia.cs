using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaVentas.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVentas.DAL.Repositorios.Contrato;
using SistemaVentas.DAL.Repositorios;
using SistemaVentas.Utility;
using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.BLL.Servicios;

namespace SistemaVentas.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbVentasContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CadenaConexion"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentasRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IUsuariosService, UsuariosService>();
            services.AddScoped<ICategoriasService, CategoriasService>();
            services.AddScoped<IProductosService, ProductosService>();
            services.AddScoped<IVentasService, VentasService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IMenusService, MenusService>();
        }
    }
}
