using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Application.UseCases;
using ProductStore.Core.DTOs;
using ProductStore.Web.Models;

namespace ProductStore.Web.Controllers.API_Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly GetProductsUseCase _getProductsUseCase;
        public ProductController(GetProductsUseCase getProductsUseCase)
        {
            _getProductsUseCase = getProductsUseCase;
        }


        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] ProductQueryModel query)
        {
            var (products, recordsTotal) = await _getProductsUseCase.ExecuteAsync(query);

            var jsonData = new
            {
                draw = query.Draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = products
            };
            return Ok(jsonData);
        }
    }
}
