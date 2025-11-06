using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BabelBooks.Core.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //configurar MediatR para que encuentre todos los Handlers en la capa de Application (BabelBooks.Core.Application)
            services.AddMediatR(cfg =>
                                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
