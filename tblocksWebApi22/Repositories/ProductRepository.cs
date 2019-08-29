using CoreWebApi22.Contracts;
using CoreWebApi22.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWebApi22.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private CoreWebApiTestDBContext _context;
        private IDistributedCache _cache;

        public ProductRepository(CoreWebApiTestDBContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> Add(Product product)
        {

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Find(int id)
        {
            var cachedProduct = await _cache.GetStringAsync(id.ToString());

            if (cachedProduct != null)
            {
                return JsonConvert.DeserializeObject<Product>(cachedProduct);
            }
            else
            {
                var dbProduct = await _context.Products.SingleOrDefaultAsync(a => a.ProductId == id);

                await _cache.SetStringAsync(id.ToString(), JsonConvert.SerializeObject(dbProduct));

                return dbProduct;
            }

        }

        public async Task<Product> Remove(int id)
        {
            var product = await _context.Products.SingleAsync(a => a.ProductId == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Products.AnyAsync(e => e.ProductId == id);
        }
    }
}