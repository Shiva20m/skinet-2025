using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class CartService(IConnectionMultiplexer redis) : ICartService
    {
        private readonly IDatabase _databse = redis.GetDatabase();
        public async Task<bool> DeleteCartAsync(string key)
        {
            return await _databse.KeyDeleteAsync(key);
        }

        public async Task<ShopingCart?> GetCartAsync(string key)
        {
            var data = await _databse.StringGetAsync(key);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShopingCart>(data!);
        }

        public async Task<ShopingCart?> SetCartAsync(ShopingCart cart)
        {
            var created = await _databse.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if(!created) return null;
            return await GetCartAsync(cart.Id);
        }
    }

}