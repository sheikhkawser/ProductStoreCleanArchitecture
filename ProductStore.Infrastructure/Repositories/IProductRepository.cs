using System.Collections.Generic;
using System.Threading.Tasks;
using ProductStore.Core.DTOs;
using ProductStore.Core.Entities;

namespace ProductStore.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(ProductQueryModel request);
        Task<int> GetTotalRecordsAsync(string searchValue);
    }
}
