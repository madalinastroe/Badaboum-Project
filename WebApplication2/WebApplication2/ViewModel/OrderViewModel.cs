using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.ViewModel
{
    public class OrderViewModel
    {
        public int orderID { get; set; }
        public int shopID { get; set; }

        public string shopName { get; set; }
        public int productID { get; set; }

        public string productName { get; set; }

        public int userID { get; set; }

        public string userName { get; set; }
        public string status { get; set; }
        public double price { get; set; }


    }
}
