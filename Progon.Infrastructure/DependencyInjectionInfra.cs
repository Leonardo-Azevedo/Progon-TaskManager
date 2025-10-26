using Microsoft.Extensions.DependencyInjection;
using Progon.Infrastructure.Interfaces;
using Progon.Infrastructure.Data;
using Progon.Infrastructure.Repositories;


namespace Progon.Infrastructure
{
    public static class DependencyInjectionInfra
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<DbConnection>(provider =>
            {
                // Cria uma instância de DbConnection já configurada com a string do SQL Server
                return new DbConnection(connectionString);
            });

            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
