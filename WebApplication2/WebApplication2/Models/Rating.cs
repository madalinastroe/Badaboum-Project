using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;

namespace WebApplication2.Models
{
    public class Rating
    {
        public int productID { get; set; }
         
        public Product product { get; set; } 

        public int userID { get; set; }

        public User user { get; set; }

        public double RatingValue { get; set; }
    }
}
