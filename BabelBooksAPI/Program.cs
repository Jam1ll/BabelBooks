using BabelBooks.Core.Application;
using BabelBooks.Infrastructure.Persistence;

namespace BabelBooksAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //
            //agregar servicios al contenedor (inyeccion de dependencias)
            //

            //servicios de API y Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //servicio de API Controllers (no se usara Minimal API)
            builder.Services.AddControllers();

            //servicios de otras capas
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            //construir la app
            var app = builder.Build();

            //
            // configurar el pipeline HTTP
            //

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
