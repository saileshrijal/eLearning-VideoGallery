namespace eLearning.Repository.Interface
{
    public interface IUnitOfWork
    {
        //create, update, delete
        Task CreateAsync<T>(T entity) where T : class;
        Task CreateRangeAsync<T>(List<T> entities) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;

        //save changes
        Task<int> CommitAsync();
    }
}