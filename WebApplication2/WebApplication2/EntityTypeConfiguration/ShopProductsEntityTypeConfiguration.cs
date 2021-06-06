using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Database;

namespace WebApplication2.EntityTypeConfiguration
{
    public class ShopProductsEntityTypeConfiguration : IEntityTypeConfiguration<ShopProducts>
    {
        public void Configure(EntityTypeBuilder<ShopProducts> builder)
        {
            builder.HasKey(k => new { k.shopID, k.productID });

            builder.HasOne(k => k.shop).WithMany(k => k.sells).HasForeignKey(k => k.shopID);
            builder.HasOne(k => k.product).WithMany(k => k.purchasedProducts).HasForeignKey(k => k.productID);
        }
    }
}
