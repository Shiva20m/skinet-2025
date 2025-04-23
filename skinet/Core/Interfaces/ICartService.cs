using Core.Entities;

namespace Core.Interfaces
{
    public interface ICartService
    {
        Task<ShopingCart?> GetCartAsync(string key);
        Task<ShopingCart?> SetCartAsync(ShopingCart cart);
        Task<bool> DeleteCartAsync(string key);


    }
    
}