using Api.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Services
{
    public interface IProductService
    {
        Task<ProductListResponse> ListAsync();
        Task<ProductResponse> AddProduct(Product product);
        Task<ProductResponse> RemoveProduct(int productId);
        Task<ProductResponse> UpdateProduct(Product product, int productId);
        Task<ProductResponse> FindByIdAsync(int productId);
    }
}
