using Core.Entities;
using Core.Interface;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Intrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public Task<bool> DeleteBasketAsync(string basketId)
        {
            return _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basketItem)
        {
            var create = await _database.StringSetAsync(basketItem.Id, JsonSerializer.Serialize(basketItem), TimeSpan.FromDays(30));
            if (!create)
            {
                return null;
            }
            else
            {
                return await GetBasketAsync(basketItem.Id);
            }
        }
    }
}
