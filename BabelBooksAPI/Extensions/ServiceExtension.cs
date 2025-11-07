using Microsoft.OpenApi.Models;

namespace BabelBooksAPI.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //
                // configuracion para leer comentarios XML
                //
                List<String> xmlFiles =
                    [.. Directory.GetFiles(AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly)];

                //
                //informacion de la API
                //
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Babel Books API",
                    Description = "API para la gestion de la tienda online Babel Books",
                    Contact = new OpenApiContact
                    {
                        Name = "Jamil / Babel Company",
                        Email = "jamilguzman202@gmail.com",
                    }
                });

                options.DescribeAllParametersInCamelCase();
                options.EnableAnnotations(); //esto sirve para las [SwaggerOperation]

                //
                // configuracion de Bearer Security para JWT
                //
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in the following format: Bearer {your_token_without_'{}'}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In =  ParameterLocation.Header
                        }, new List<string>()
                    }
                });
            });
        }

        
    }
}
