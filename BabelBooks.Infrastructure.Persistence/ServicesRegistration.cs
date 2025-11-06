using BabelBooks.Core.Application.ReadModels.ProductReadModels;
using BabelBooks.Infrastructure.Persistence.Projections;
using JasperFx.Events.Projections;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BabelBooks.Infrastructure.Persistence
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Host=localhost;Port=5432;Database=marten_demo;Username=postgres;Password=postgres";

            services.AddMarten(options =>
            {
                //usa la bd en appsetings.json de la API
                options.Connection(connectionString);

                //asegura que el id del modelo de lectura sea igual al agregado (stream)
                options.Schema.For<ProductReadModel>().Identity(x => x.Id);

                //actualiza el modelo de lectura inmediatamente despues de guardar el evento
                options.Projections.Add<ProductProjection>(ProjectionLifecycle.Inline); //se puede usar async() para una alta concurrencia
            });

            return services;
        }
    }
}