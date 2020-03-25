using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.Models.Configurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Apples",
                    Quantity = 100
                },
                new Product
                {
                    Id = 2,
                    Name = "Bananas",
                    Quantity = 150
                },
                new Product
                {
                    Id = 3,
                    Name = "Cakes",
                    Quantity = 200
                });
        }
    }
}
