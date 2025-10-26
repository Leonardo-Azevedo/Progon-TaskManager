

using Microsoft.Extensions.DependencyInjection;
using Progon.Application.Interfaces;
using Progon.Application.Services;

namespace Progon.Application
{
    public static class DependencyInjectionAp
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
