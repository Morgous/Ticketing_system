namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity?>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        void Create(TEntity? entity);
        void Update(TEntity? entity);
        void Delete(int id);
        void Delete(TEntity? entity);
    }
}
