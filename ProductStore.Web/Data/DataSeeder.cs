using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProductStore.Core.Entities;
using ProductStore.Infrastructure;

namespace ProductStore.Web.Data
{
    public class DataSeeder
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ProductStoreDbContext>();

            if (context.Products.Any()) return;
            var productData = await File.ReadAllTextAsync("Data/products.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productData);

            var productEntities = products.Select((p, index) => new Product
            {
                //Id = index + 1,
                Name = p.Name,
                Category = p.Category,
                Description = p.Description,
                FileName = p.FileName,
                Height = p.Height,
                Width = p.Width,
                Price = p.Price,
                Rating = p.Rating
            }).ToList();

            context.Products.AddRange(productEntities);
            await context.SaveChangesAsync();
        }
    }
}
