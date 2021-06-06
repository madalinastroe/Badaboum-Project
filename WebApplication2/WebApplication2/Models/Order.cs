using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.ViewModel;

namespace WebApplication2.Database
{
    public class Order
    {
        public int orderID { get; set; }
        public int shopID { get; set; }
        public Shop  shop { get; set; }
        public int productID { get; set; }

        public Product product { get; set; }

        public int userID { get; set; }

        public User user { get; set; }

        public string status { get; set; }
        public double price { get; set; }

    }
}
