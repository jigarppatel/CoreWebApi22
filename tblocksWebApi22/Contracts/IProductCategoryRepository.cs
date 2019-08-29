using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApi22.Models;

namespace CoreWebApi22.Contracts
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory> Add(ProductCategory item);

        Task<IEnumerable<ProductCategory>> GetAll();

        Task<ProductCategory> Find(int id);

        Task<ProductCategory> Remove(int id);

        Task<ProductCategory> Update(ProductCategory item);

        Task<bool> Exists(int id);
    }
}
