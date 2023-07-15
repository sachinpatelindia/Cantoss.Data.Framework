using Cantoss.Data.Framework.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Cantoss.Data.Framework
{
    /// <summary>
    /// Azure connection manager manages to connect different azure services
    /// </summary>
    public class AzureConnectionDependencyRegistrar
    {
        private Connection? _connection;
        public AzureConnectionDependencyRegistrar(IServiceCollection services, IConfiguration configuration)
        {
            _connection = this.GetConnection(configuration);
            this.RegisterDependency(services);
        }

        private void RegisterDependency(IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>(con =>
            {
                return new ConnectionFactory(_connection);
            });
            services.AddScoped(typeof(ICosmosDbHandler<>),typeof(CosmosDbHandler<>));
        }

        private Connection GetConnection(IConfiguration configuration)
        {
            return configuration.GetSection("Azure").Get<Connection>();

        }
    }
}

