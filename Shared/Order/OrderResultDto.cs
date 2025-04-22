using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Order
{
    public record OrderResultDto
    {
        public Guid Id { get; init; }
        //User Email
        public string UserEmail { get; init; }
        //Shiping Address
        public ShipingAddressDto ShipingAddress { get; init; }
        //Order Items
        public ICollection<OrderItemDto> OrderItems { get; init; } = new List<OrderItemDto>();
        public string DeliveryMethod { get; init; }
        //Payment
        public string PaymentStatus { get; init; }
        public decimal SubTotal { get; init; }
        public decimal Total { get; init; }
        //Order Date
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.Now;
    }
}
