using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using ProductStore.Core.DTOs;
using ProductStore.Core.Entities;

namespace ProductStore.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductStoreDbContext _context;

        public ProductRepository(ProductStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(ProductQueryModel request)
        {
            int pageSize = Convert.ToInt32(request.Length);
            int skip = Convert.ToInt32(request.Start);

            var data = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchValue))
            {
                data = data.Where(m => m.Name.Contains(request.SearchValue)
                                       || m.Category.Contains(request.SearchValue)
                                       || m.Description.Contains(request.SearchValue)
                                       || m.Height.ToString().Contains(request.SearchValue)
                                       || m.Width.ToString().Contains(request.SearchValue)
                                       || m.Price.ToString().Contains(request.SearchValue)
                                       || m.Rating.ToString().Contains(request.SearchValue));
            }

            if (!string.IsNullOrEmpty(request.SortColumn) && !string.IsNullOrEmpty(request.SortColumnDirection))
            {
                data = data.OrderBy($"{request.SortColumn} {request.SortColumnDirection}");
            }

            return await data.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetTotalRecordsAsync(string searchValue)
        {
            var data = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(m => m.Name.Contains(searchValue)
                                       || m.Category.Contains(searchValue)
                                       || m.Description.Contains(searchValue)
                                       || m.Height.ToString().Contains(searchValue)
                                       || m.Width.ToString().Contains(searchValue)
                                       || m.Price.ToString().Contains(searchValue)
                                       || m.Rating.ToString().Contains(searchValue));
            }

            return await data.CountAsync();
        }
    }
}
