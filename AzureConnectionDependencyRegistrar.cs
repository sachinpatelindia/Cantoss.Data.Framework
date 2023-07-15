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
            _connection = this.GetConnection(services,configuration);
            this.RegisterDependency(services);
        }

        private void RegisterDependency(IServiceCollection services)
        {
            _ = services.AddSingleton<IConnectionFactory, ConnectionFactory>(con =>
            {
                return new ConnectionFactory(_connection);
            });
            services.AddScoped(typeof(ICosmosDbHandler<>),typeof(CosmosDbHandler<>));
        }

        private Connection GetConnection(IServiceCollection services,IConfiguration configuration)
        {
            var connection = new Connection();
            configuration.GetSection("Azure").Bind(connection);
            return connection;
        }
    }
}

