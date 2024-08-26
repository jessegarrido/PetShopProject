using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; } = 0;
        public int? Quantity { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }

}
