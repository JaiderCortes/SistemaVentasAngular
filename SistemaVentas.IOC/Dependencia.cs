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
        }
    }
}
