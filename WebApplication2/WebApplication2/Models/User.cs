using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Database
{
    public class User
    {
        //Id property functions as the unique key in a relational database

        public User(int id, string firstName, string role)
        {
            Id = id;
            this.firstName = firstName;
            this.role = role;
        }



        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string email { get; set; }
        public string address { get; set; }
        public string password { get; set; }

        public string avatar { get; set; }

        public string role { get; set; } = "user";

        public IEnumerable<Rating> ratedProducts { get; set; }
        public IEnumerable<Order> ordersU { get; set; }

    }
}
