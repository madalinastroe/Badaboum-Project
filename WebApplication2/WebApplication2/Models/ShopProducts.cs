using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Database
{
    public class ShopProducts
    {
        [Key]
        public int shopID { get; set; }

        public Shop shop { get; set; }

        public int productID { get; set; }

        public Product product { get; set; }

        public int quantity { get; set; }

        public int stock { get; set; }

        public double price { get; set; }
    }
}
