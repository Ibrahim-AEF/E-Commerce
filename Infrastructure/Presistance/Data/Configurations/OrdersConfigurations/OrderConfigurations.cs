using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Data.Configurations.Order
{
    public class OrderConfigurations : IEntityTypeConfiguration<Domain.Entities.OrderEntities.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.OrderEntities.Order> builder)
        {
            #region Shiping Address
            builder.OwnsOne(p => p.ShipingAddress, P => P.WithOwner());
            #endregion
            #region Order Items
            builder.HasMany(o => o.OrderItems).WithOne();
            #endregion
            #region Payment Status
            builder.Property(p => p.PaymentStatus)
                .HasConversion(s => s.ToString(),
                s => Enum.Parse<OrderPaymentStatus>(s));
            #endregion
            #region DeliveryMethod
            builder.HasOne(p => p.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            #endregion
            #region SubTotal
            builder.Property(p => p.SubTotal)
                .HasColumnType("decimal(18,3)");
            #endregion
        }
    }
}
