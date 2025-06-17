using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Suitability.Consumer.Infrastructure.Ioc.Utils
{
    public class SwaggerConfiguration
    {
        public static void AddSwagger(IServiceCollection _collection)
        {
            _collection.AddSwaggerGen(options =>
            {

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Suitability.Api", Version = "v1" });
                options.ResolveConflictingActions(d => d.First());

            });
        }
    }
}