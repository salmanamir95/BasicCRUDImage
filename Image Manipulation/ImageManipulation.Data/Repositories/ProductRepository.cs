using ImageManipulation.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageManipulation.Data.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);

        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product?> FindProductByAsync(int id);

        Task DeleteProductByAsync(Product product);
        
    }

    public class ProductRepository(ApplicationDBContext context) : IProductRepository
    {
        public async Task<Product> AddProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductByAsync(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task<Product?> FindProductByAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await context.Products.ToListAsync();
        }


        public async Task<Product> UpdateProductAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return product;
        }


    }
}
