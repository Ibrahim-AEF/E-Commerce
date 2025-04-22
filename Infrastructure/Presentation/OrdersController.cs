using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager):ApiController
    {
        #region CreateOrder
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrder(OrderRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await serviceManager.OrderService.CreateOrderAsync(request, email);
            return Ok(Order);
        }
        #endregion
        #region GetOrderById
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetOrder(Guid id)
        {
            var Orders = await serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(Orders);
        }
        #endregion
        #region GetOrderByEmail
        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Orders = await serviceManager.OrderService.GetOrdersByEmailAsync(email);
            return Ok(Orders);
        }
        #endregion
        #region GetAllDeliveryMethod
        [AllowAnonymous]
        [HttpGet("deliverymethod")]
        public async Task<ActionResult<DeliveryMethodDto>> GetDeliveryMethods()
        {
            var DeliverMethods = await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(DeliverMethods);
        }
        #endregion
    }
}
