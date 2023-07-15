namespace Cantoss.Data.Framework.Operations
{
    public interface ICosmosDbHandler<T> where T : CommonEntity
    {
        Task<T> GetOne<T>(T entity);
        Task<IList<T>> GetMany<T>(T entity);
        Task<T> Insert<T>(T entity);
        Task<IList<T>> InsertMany<T>(IList<T> entities);
        Task<T> Modify<T>(T entity);
        Task<IList<T>> ModifyMany<T>(IList<T> entities);
        Task<T> Remove<T>(T entity);
        Task<IList<T>> RemoveMany<T>(IList<T> entities);
    }
}
