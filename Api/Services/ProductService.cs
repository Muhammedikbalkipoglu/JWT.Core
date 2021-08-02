using Api.Domain;
using Api.Domain.Repositories;
using Api.Domain.Responses;
using Api.Domain.Services;
using Api.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ProductResponse> AddProduct(Product product)
        {
            try
            {
                await _productRepository.AddProductAsync(product);
                await _unitOfWork.ComplateAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {

                return new ProductResponse($"I have an exception While Product add::{ex.Message}");
            }
        }

        public async Task<ProductResponse> FindByIdAsync(int productId)
        {
            try
            {
                Product product = await _productRepository.FindByIdAsync(productId);

                if (product == null)
                {
                    return new ProductResponse("Product is not found");
                }

                return new ProductResponse(product);
            }
            catch (Exception ex)
            {

                return new ProductResponse($"I have an exception While Product found::{ex.Message}");

            }
        }

        public async Task<ProductListResponse> ListAsync()
        {
            try
            {
                IEnumerable<Product> products = await _productRepository.ListAsync();
                return new ProductListResponse(products);
            }
            catch (Exception ex)
            {
                return new ProductListResponse($"I have an exception While Product listining::{ex.Message}");

            }
        }

        public async Task<ProductResponse> RemoveProduct(int productId)
        {
            try
            {
                Product product = await _productRepository.FindByIdAsync(productId);

                if (product == null)
                {
                    return new ProductResponse("This product not found");
                }
                else
                {
                    await _productRepository.RemoveProductAsync(productId);
                    await _unitOfWork.ComplateAsync();
                    return new ProductResponse(product);
                }
            }
            catch (Exception ex)
            {
                return new ProductResponse($"I have an exception While Product removing::{ex.Message}");
            }

        }

        public async Task<ProductResponse> UpdateProduct(Product product, int productId)
        {
            try
            {
                var firstproduct = await _productRepository.FindByIdAsync(productId);

                if (firstproduct == null)
                {
                    return new ProductResponse("This product is not found");
                }

                firstproduct.Name = product.Name;
                firstproduct.Category = product.Category;
                firstproduct.Price = product.Price;

                _productRepository.UpdateProduct(firstproduct);

                await _unitOfWork.ComplateAsync();

                return new ProductResponse(firstproduct);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"I have an exception While Product updateing::{ex.Message}");
            }
        }
    }
}
