using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Order
{
    public record OrderRequest
    {
        public string BasketId { get; init; }
        public ShipingAddressDto ShipingAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
