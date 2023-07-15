using Microsoft.Azure.Cosmos;


namespace Cantoss.Data.Framework
{

    /// <summary>
    /// Connection factory
    /// </summary>
    public class ConnectionFactory:IConnectionFactory
    {

        private CosmosClient? cosmosClient;
        private Database? database;
        private Container? container;
        private readonly Connection? _connection;
        public ConnectionFactory(Connection connection)
        {
            _connection = connection;
        }
        public async Task<T?> CreateConnection<T>(ConnectionType connectionType) where T : class 
        {
            switch (connectionType)
            {
                case ConnectionType.AzureCosmosDb:
                    {
                        var cosmos = await CosmosDbSetup();
                        return cosmos as T;
                    }
                default:
                    {
                        throw new NotImplementedException(nameof(connectionType));
                    }
            }
        }

        private async Task<Container?> CosmosDbSetup()
        {
            var cosemoDbConnection = _connection.CosmosDb;
            this.cosmosClient = new CosmosClient(cosemoDbConnection.EndpointUri, cosemoDbConnection.PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
            this.database = await cosmosClient.CreateDatabaseIfNotExistsAsync("database");
            this.container = await database.CreateContainerIfNotExistsAsync("container", "/partitionKey");
            return this.container;
        }
    }
}
