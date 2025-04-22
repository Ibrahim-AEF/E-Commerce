using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Data.Configurations.Order
{
    public class OrderItemsConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(d => d.Price).HasColumnType("decimal(18,3)");
            builder.OwnsOne(d => d.Product, p => p.WithOwner());
        }
    }
}
