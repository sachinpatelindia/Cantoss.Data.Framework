using Microsoft.Azure.Cosmos;

namespace Cantoss.Data.Framework.Operations
{
    public class CosmosDbHandler<T> : ICosmosDbHandler<T> where T : CommonEntity
    {
        private readonly IConnectionFactory _connectionFactory;
        private async Task<Container?> GetCosmosContainer()
        { 
            return await _connectionFactory.CreateConnection<Container>(ConnectionType.AzureCosmosDb) as Container;
        }

        public CosmosDbHandler(IConnectionFactory connectionFactory)
        {
              _connectionFactory = connectionFactory;
        }

        public async Task<IList<T>> GetMany<T>(T entity)
        {
            dynamic? baseEntity = entity as CommonEntity;
            var container = await GetCosmosContainer();

            var sqlQueryText = "SELECT * FROM c WHERE c.partitionKey = '" + baseEntity.PartitionKey + "'";

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            List<T> data = new List<T>();
            try
            {
                FeedIterator<T> queryResultSetIterator = container.GetItemQueryIterator<T>(queryDefinition);

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<T> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (T family in currentResultSet)
                    {
                        data.Add(family);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return data;
        }

        public async Task<T> GetOne<T>(T entity)
        {
            dynamic? baseEntity = entity as CommonEntity;
            var container = await GetCosmosContainer();
            ItemResponse<T> response = null;
            try
            {
                response = await container.ReadItemAsync<T>(baseEntity.Id, new PartitionKey(baseEntity.PartitionKey));
            }
            catch (Exception ex)
            {

            }
            return response;

        }

        public async Task<T> Insert<T>(T entity)
        {
            var result =await GetCosmosContainer();
            return await result.CreateItemAsync<T>(entity);
        }

        public Task<IList<T>> InsertMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> Modify<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> ModifyMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public Task<T> Remove<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> RemoveMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}