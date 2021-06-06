using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;
using WebApplication2.Models;

namespace WebApplication2.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string email { get; set; }
        public string address { get; set; }
        public string password { get; set; }

        public string avatar { get; set; }

        public string role { get; set; } = "user";

        public IEnumerable<RatingViewModel> ratedProducts { get; set; }
        public IEnumerable<OrderViewModel> ordersU { get; set; }


    }
}
