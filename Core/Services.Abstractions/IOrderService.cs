using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        //Get Order By Id
        public Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        //Get Orders For Email
        public Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string Email);
        //Create Order
        public Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string userEmail);
        //Get All DeliveryMethods
        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
