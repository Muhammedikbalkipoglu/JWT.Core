using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Responses
{
    public class ProductListResponse:BaseResponse
    {
        public IEnumerable<Product> ProductList { get; set; }

        private ProductListResponse(bool success, string message, IEnumerable<Product> productList) : base(success, message)
        {
            this.ProductList = productList;
        }

        //başarılı ise
        public ProductListResponse(IEnumerable<Product> productlist):this(true,string.Empty, productlist)
        {

        }

        //başarısız ise

        public ProductListResponse(string message):this(false,message,null)
        {

        }
    }
}
