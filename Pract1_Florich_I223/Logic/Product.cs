using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract1_Florich_I223
{
    public class Product : IProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; internal set; }
        public string Description { get; internal set; }
    };
}
