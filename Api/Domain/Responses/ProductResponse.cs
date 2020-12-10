using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Responses
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; set; }
        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            this.Product = product;
        }

        //başarılı olursa
        public ProductResponse(Product product) : this(true, string.Empty, product)
        {

        }

        // başarısız olursa

        public ProductResponse(string message) : this(false, message, null)
        {

        }
    }
}
