using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infastracture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfastracture(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

}
