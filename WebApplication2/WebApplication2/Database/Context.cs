using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.EntityTypeConfiguration;
using WebApplication2.Models;

namespace WebApplication2.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
           : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Shop> Shop { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<ShopProducts> ShopProducts { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RatingEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ShopProductsEntityTypeConfiguration());
        }
    }
}
