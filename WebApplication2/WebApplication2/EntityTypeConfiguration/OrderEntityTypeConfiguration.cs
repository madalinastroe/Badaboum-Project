using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;

namespace WebApplication2.EntityTypeConfiguration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(k => new { k.orderID});

            builder.HasOne(k => k.shop).WithMany(k => k.ordersS).HasForeignKey(k => k.shopID);
            builder.HasOne(k => k.product).WithMany(k => k.ordersP).HasForeignKey(k => k.productID);
            builder.HasOne(k => k.user).WithMany(k => k.ordersU).HasForeignKey(k => k.userID);
        }
    }
}
