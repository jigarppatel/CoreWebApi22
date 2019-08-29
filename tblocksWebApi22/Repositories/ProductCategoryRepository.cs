using CoreWebApi22.Contracts;
using CoreWebApi22.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreWebApi22.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private CoreWebApiTestDBContext _context;
        private IDistributedCache _cache;

        public ProductCategoryRepository(CoreWebApiTestDBContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<ProductCategory>> GetAll()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> Add(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<ProductCategory> Find(int id)
        {
            var cachedProductCategory = await _cache.GetStringAsync(id.ToString());

            if (cachedProductCategory != null)
            {
                return JsonConvert.DeserializeObject<ProductCategory>(cachedProductCategory);
            }
            else
            {
                var dbProductCategory = await _context.ProductCategories.SingleOrDefaultAsync(a => a.ProductCategoryId == id);

                await _cache.SetStringAsync(id.ToString(), JsonConvert.SerializeObject(dbProductCategory));

                return dbProductCategory;
            }

        }

        public async Task<ProductCategory> Remove(int id)
        {
            var productCategory = await _context.ProductCategories.SingleAsync(a => a.ProductCategoryId == id);
            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<ProductCategory> Update(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.ProductCategories.AnyAsync(e => e.ProductCategoryId == id);
        }
    }
}