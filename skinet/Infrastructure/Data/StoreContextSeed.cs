using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext)
        {
            var productData = await File.ReadAllTextAsync("../Infrastructure/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productData);

            if(products==null) return;

            storeContext.Products.AddRange(products);
            
            await storeContext.SaveChangesAsync();
        }
    }
    
}