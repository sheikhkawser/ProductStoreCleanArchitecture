using ProductStore.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductStore.Core.DTOs;
using ProductStore.Core.Entities;

namespace ProductStore.Application.UseCases
{
    public class GetProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<(IEnumerable<Product>, int)> ExecuteAsync(ProductQueryModel request)
        {
            var products = await _productRepository.GetProductsAsync(request);
            var totalRecords = await _productRepository.GetTotalRecordsAsync(request.SearchValue);

            return (products, totalRecords);
        }
    }
}
