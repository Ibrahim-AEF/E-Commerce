using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public class Order:BaseEntity<Guid>
    {
        public Order()
        {

        }
        public Order(string _UserEmail,ShipingAddress _shipingAddress, ICollection<OrderItem> _orderItems,DeliveryMethod _deliveryMethod,decimal _SubTotal)
        {
            Id = Guid.NewGuid();
            UserEmail = _UserEmail;
            ShipingAddress = _shipingAddress;
            OrderItems = _orderItems;
            DeliveryMethod = _deliveryMethod;
            SubTotal = _SubTotal;
        }

        //User Email
        public string UserEmail { get; set; }
        //Shiping Address
        public ShipingAddress ShipingAddress { get; set; }
        //Order Items
        public ICollection<OrderItem> OrderItems { get; set; }
        //Payment
        public OrderPaymentStatus PaymentStatus { get; set; }
        //Delivery
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        //SubTotal => items.Q * Price
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        //Order Date
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    }
}
