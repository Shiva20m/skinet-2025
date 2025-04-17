using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        public Task<T>GetByIdAsync(int id);
        public Task<IReadOnlyList<T>>ListAllAsync();
        public Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        public Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        public Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
        public Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T,TResult> spec);
        public void Add(T entity);
        public void Update(T entity);
        public void Remove(T entity);
        public bool Exists(int id);
        public Task<bool>SaveAllAsync();
    }
    
}