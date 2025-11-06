
using BabelBooks.Core.Application;
using BabelBooks.Infrastructure.Persistence;
using BabelBooks.Core.Application.Features.ProductsCQRS.Commands; // (O BabelBooks.Core.Application.Comandos)
using BabelBooks.Core.Application.Features.ProductsCQRS.Commands.Create;
using Microsoft.AspNetCore.Mvc;

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

            //
            // ENDPOINTS de la API
            //

           


            app.Run();
        }
    }
}
