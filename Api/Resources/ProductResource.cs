using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Resources
{
    public class ProductResource
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Category { get; set; }
        
        [Required]
        public decimal? Price { get; set; }
    }
}
