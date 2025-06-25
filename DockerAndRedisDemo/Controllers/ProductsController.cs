using DockerAndRedisDemo.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace DockerAndRedisDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DockerAndRedisDemoDbContext _dbContext;
        private readonly IDistributedCache _cache;

        public ProductsController(DockerAndRedisDemoDbContext context, IDistributedCache cache)
        {
            _dbContext = context;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var cacheKey = $"product:{id}";
            var cached = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cached))
            {
                var productFromCache = JsonSerializer.Deserialize<Product>(cached);
                return Ok(productFromCache);
            }

            var productFromDb = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (productFromDb is null)
            {
                return NotFound();
            }

            var timeToExpire = TimeSpan.FromMinutes(2);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToExpire
            };

            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(productFromDb), options);

            return Ok(productFromDb);
        }
    }
}