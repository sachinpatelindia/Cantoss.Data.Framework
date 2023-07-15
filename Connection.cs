using Microsoft.Extensions.Options;

namespace Cantoss.Data.Framework
{
    /// <summary>
    /// Azure connectionstring value
    /// </summary>
    public class Connection
    {
        public CosmosDb? CosmosDb { get; set; }
        public ConnectionType ConnectionType { get; set; }
    }
    /// <summary>
    /// Cosmosdb connection value
    /// </summary>
    public class CosmosDb
    {
        public required string EndpointUri { get; set; }
        public required string PrimaryKey { get; set; }
    }
}
