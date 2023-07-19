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

        public async Task<IList<T>> GetMany<T>(object partitionKey)
        {
            var container = await GetCosmosContainer();

            var sqlQueryText = "SELECT * FROM c WHERE c.partitionKey = '" + partitionKey + "'";

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
                throw;
            }
            return data;
        }

        public async Task<T> GetOne<T>(T entity)
        {
            dynamic? baseEntity = entity as CommonEntity;
            var container = await GetCosmosContainer();
            try
            {
                entity = await container.ReadItemAsync<T>(baseEntity.Id, new PartitionKey(baseEntity.PartitionKey)) as ItemResponse<T>;
            }
            catch (Exception)
            {
                throw;
            }
            return entity;

        }

        public async Task<T> Insert<T>(T entity)
        {
            var container = await GetCosmosContainer();
            try
            {
                entity=await container.CreateItemAsync<T>(entity);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        public Task<IList<T>> InsertMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Modify<T>(T entity)
        {
            var container = await GetCosmosContainer();
            try
            {
                entity = await container.UpsertItemAsync<T>(entity);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        public Task<IList<T>> ModifyMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Remove<T>(T entity)
        {
            var container = await GetCosmosContainer();
            var deleteItem = entity as CommonEntity;
            PartitionKey key = new(deleteItem.PartitionKey);
            try
            {
                entity = await container.DeleteItemAsync<T>(deleteItem.Id, key);
            }
            catch (Exception)
            {
                throw;
            }
            return entity;
        }

        public Task<IList<T>> RemoveMany<T>(IList<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}