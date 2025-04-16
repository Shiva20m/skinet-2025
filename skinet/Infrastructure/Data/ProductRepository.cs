using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository(StoreContext storeContext) : IProductRepository
    {
        // primary constructor remove the need of
        // private readonly StoreContext _storeContext;

        // public ProductRepository(StoreContext storeContext)
        // {
        //     _storeContext=storeContext;
        // }
        // private readonly StoreContext _storeContext = storeContext;

        public void AddProduct(Product product)
        {
            storeContext.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            storeContext.Products.Remove(product);
        }

        public async Task<IReadOnlyList<string>> GetBrandAsync()
        {
            return await storeContext.Products.Select(x=> x.Brand)
            .Distinct()
            .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
           return await storeContext.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
        {
            var query = storeContext.Products.AsQueryable();
            if(!string.IsNullOrWhiteSpace(brand))
            {
                query = query.Where(x=>x.Brand==brand);
            }
            if(!string.IsNullOrWhiteSpace(type))
            {
                query=query.Where(x=>x.Type==type);

            }
            if(!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                         query = query.OrderBy(x=>x.Price);
                         break;
                    case "priceDesc":
                         query = query.OrderByDescending(x=>x.Price);
                         break;
                    
                    default:
                         query = query.OrderBy(x=>x.Name);
                         break;
                }
            }
            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetTypesAsyn()
        {
            return await storeContext.Products.Select(x=> x.Type)
            .Distinct()
            .ToListAsync();
        }

        public bool ProductExists(int id)
        {
            return storeContext.Products.Any(x=> x.Id==id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await storeContext.SaveChangesAsync()>0;
        }

        public void UpdateProduct(Product product)
        {
            storeContext.Entry(product).State = EntityState.Modified;
        }
        
    }

}