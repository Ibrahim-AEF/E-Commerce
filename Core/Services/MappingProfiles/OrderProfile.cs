using AutoMapper;
using Domain.Entities.OrderEntities;
using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            #region Shiping Address
            CreateMap<ShipingAddress, ShipingAddressDto>().ReverseMap();
            #endregion
            #region Order Item
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(o => o.ProductId, p => p.MapFrom(p => p.Product.ProductId))
                 .ForMember(o => o.ProductName, p => p.MapFrom(p => p.Product.ProductName))
                  .ForMember(o => o.PictureUrl, p => p.MapFrom(p => p.Product.PictureUrl)).ReverseMap();
            #endregion
            #region Order
            CreateMap<Order, OrderResultDto>()
                .ForMember(o => o.PaymentStatus, p => p.MapFrom(p => p.ToString()))
                .ForMember(o => o.DeliveryMethod, p => p.MapFrom(p => p.DeliveryMethod.ShortName))
                .ForMember(o => o.Total, p => p.MapFrom(p => p.SubTotal + p.DeliveryMethod.Price));
            #endregion
            #region DeliveryMethod
            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
            #endregion
        }
    }
}
