using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.ViewModel;

namespace WebApplication2.Database
{
    public class Shop
    {
        public int shopID { get; set; }
        public string name { get; set; }

        public IEnumerable<ShopProducts> sells { get; set; }

        public IEnumerable<Order> ordersS { get; set; }

    }
}
