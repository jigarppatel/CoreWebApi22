
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreWebApi22.Models;

namespace CoreWebApi22.Contracts
{
    public interface IProductRepository
    {
        Task<Product> Add(Product item);

        Task<IEnumerable<Product>> GetAll();

        Task<Product> Find(int id);

        Task<Product> Remove(int id);

        Task<Product> Update(Product item);

        Task<bool> Exists(int id);
    }
}