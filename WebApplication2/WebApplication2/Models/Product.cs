using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Database
{
    public class Product
    {
        [Key]
        public int productID { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string picture { get; set; }

        public IEnumerable<Rating> ratings {get; set;} //un produs poate avea mai multe rating-uri
        public IEnumerable<ShopProducts> purchasedProducts { get; set; }  //un produs poate fi cumparat de mai multe ori - in limita stocului
        public IEnumerable<Order> ordersP { get; set; }

    }
}
