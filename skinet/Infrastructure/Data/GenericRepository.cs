using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T>(StoreContext storeContext) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
        {
            // storeContext.Products.Add(product);
            storeContext.Set<T>().Add(entity);
        }
        public bool Exists(int id)
        {
            // return storeContext.Products.Any(x=> x.Id==id);
            return storeContext.Set<T>().Any(x=>x.Id==id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            // return await storeContext.Products.FindAsync(id);
            return await storeContext.Set<T>().FindAsync(id);
            
        }

        public async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification<TResult>(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            IReadOnlyList<T> result = await storeContext.Set<T>().ToListAsync();
            return result;
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification<TResult>(spec).ToListAsync();
        }

        public void Remove(T entity)
        {
            storeContext.Set<T>().Remove(entity);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await storeContext.SaveChangesAsync()>0;
        }
        public void Update(T entity)
        {
            storeContext.Set<T>().Attach(entity);
            storeContext.Entry(entity).State = EntityState.Modified;
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(storeContext.Set<T>().AsQueryable(), spec);
        }
        private IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery<T, TResult>(storeContext.Set<T>().AsQueryable(), spec);
        }
    }
}