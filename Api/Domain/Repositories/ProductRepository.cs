using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(apitokendbContext context):base(context)
        {

        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> FindByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task RemoveProductAsync(int productId)
        {
            var product = await FindByIdAsync(productId);

            _context.Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
