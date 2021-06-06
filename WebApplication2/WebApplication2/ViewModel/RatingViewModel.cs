using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class RatingViewModel
    {

        public int productID { get; set; }

        public string productName { get; set; }

        public int userID { get; set; }

        public string userName { get; set; }

        public double RatingValue { get; set; }
    }
}
