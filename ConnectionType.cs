using Newtonsoft.Json;

namespace Cantoss.Data.Framework
{
    /// <summary>
    /// Azure Services e.g Cosmosdb, azure table etc.
    /// </summary>
    public enum ConnectionType
    {
       AzureCosmosDb,
       AzureTable,
       AzureQueue,
       AzureKeyVault

    }
}
