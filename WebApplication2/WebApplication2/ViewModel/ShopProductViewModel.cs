using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class ShopProductViewModel
    {
        public int shopID { get; set; }

        public string shopName { get; set; }

        public int productID { get; set; }

        public string productName { get; set; }

        public string productPicture { get; set; }

        public int quantity { get; set; }

        public int stock { get; set; }

        public double price { get; set; }



    }
}
