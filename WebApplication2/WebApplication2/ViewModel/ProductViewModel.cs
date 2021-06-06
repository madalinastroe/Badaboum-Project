using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;
using WebApplication2.ViewModel;

namespace WebApplication2.Models
{
    public class ProductViewModel
    {
        public int productID { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string picture { get; set; }

        public IEnumerable<RatingViewModel> ratings { get; set; } //un produs poate avea mai multe rating-uri
        public IEnumerable<ShopProductViewModel> purchasedProducts { get; set; }

        public IEnumerable<OrderViewModel> ordersP { get; set; }




    }
}
