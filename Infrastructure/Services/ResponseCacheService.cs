using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        //This is a method to cache the data
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)       //response is value from the database e.g list of products
            {
                return;
            }

            //change to to json format
            var options= new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var serializedResponse = JsonSerializer.Serialize(response, options);

            await _database.StringSetAsync(cacheKey,serializedResponse,timeToLive);

        }

        //This is to get the cached data
        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
           var cachedResponse= await _database.StringGetAsync(cacheKey);

           if(cachedResponse.IsNullOrEmpty)
           {
               return  null;
           }
           return cachedResponse;
        }
    }
}