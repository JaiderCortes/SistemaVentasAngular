using SistemaVentas.IOC;

namespace SistemaVentas.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.InyectarDependencias(builder.Configuration);

            builder.Services.AddCors(options => {
                options.AddPolicy("PoliticaAcceso", app => {
                    app.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("PoliticaAcceso");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
