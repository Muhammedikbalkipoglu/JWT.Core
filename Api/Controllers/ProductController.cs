using Api.Domain;
using Api.Domain.Responses;
using Api.Domain.Services;
using Api.Extensions;
using Api.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            ProductListResponse productListResponse = await _productService.ListAsync();

            if (productListResponse.Success)
            {
                return Ok(productListResponse.ProductList);
            }
            else
            {
                return BadRequest(productListResponse.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            ProductResponse productResponse = await _productService.FindByIdAsync(id);

            if (productResponse.Success)
            {
                return Ok(productResponse.Product);
            }
            else
            {
                return BadRequest(productResponse.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = _mapper.Map<ProductResource, Product>(productResource);

                var Response = await _productService.AddProduct(product);

                if (Response.Success)
                {
                    return Ok(Response.Product);
                }
                else
                {
                    return BadRequest(Response.Message);
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(ProductResource productResource, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = _mapper.Map<ProductResource, Product>(productResource);

                var Response = await _productService.UpdateProduct(product, id);

                if (Response.Success)
                {
                    return Ok(Response.Product);
                }

                else
                {
                    return BadRequest(Response.Message);
                }
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            ProductResponse response = await _productService.RemoveProduct(id);

            if (response.Success)
            {
                return Ok(response.Product);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        //[HttpDelete("{name}/{category}")]
        //public async Task<IActionResult> RemoveProduct(string name, string category)
        //{
        //    ProductResponse response = await _productService.RemoveProduct(id);

        //    if (response.Success)
        //    {
        //        return Ok(response.Product);
        //    }
        //    else
        //    {
        //        return BadRequest(response.Message);
        //    }
        //}


    }
}
