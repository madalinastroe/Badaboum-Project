using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.EntityTypeConfiguration
{
    public class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(k => new { k.productID, k.userID });

            builder.HasOne(k => k.product).WithMany(k => k.ratings).HasForeignKey(k => k.productID);
            builder.HasOne(k => k.user).WithMany(k => k.ratedProducts).HasForeignKey(k => k.userID);
        }
    }
}
