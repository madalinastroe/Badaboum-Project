using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;

namespace WebApplication2.ViewModel
{
    public class ShopViewModel
    {
        public int shopID { get; set; }

        public string name { get; set; }

        public IEnumerable<ShopProductViewModel> sells { get; set; }

        public IEnumerable<OrderViewModel> ordersS { get; set; }



    }
}
