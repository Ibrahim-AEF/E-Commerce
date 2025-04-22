using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.Basket;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Domain.Exceptions.Product;
using Services.Abstractions;
using Services.Specifications;
using Shared.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IMapper mapper,IBasketRepository basketRepository,IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            //1- Shiping Address
            var ShipingAddress = mapper.Map<ShipingAddress>(request.ShipingAddress);
            //2- OrderItems
            var Basket = await basketRepository.GetBasketAsync(request.BasketId)
                ?? throw new BasketNotFoundException(request.BasketId);
            var OrderItems = new List<OrderItem>();
            foreach(var item in Basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>()
                    .GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                OrderItems.Add(CreateOrderItem(item, product));
            }
            //Delivery
            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(request.DeliveryMethodId) ??
                throw new DeliveryMethodExceptions(request.DeliveryMethodId);
            //subtotal
            var SubTotal = OrderItems.Sum(i => i.Price * i.Quantity);
            //save to database
            var Order = new Order(userEmail, ShipingAddress, OrderItems, DeliveryMethod, SubTotal);
            await unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<OrderResultDto>(Order);

        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
        => new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl),
            item.Quantity, item.Price);

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var Order = unitOfWork.GetRepository<Order, Guid>()
                .GetByIdWithSpecificationAsync(new OrderWithIncludeSpecifications(id))
                ?? throw new OrderNotFoundException(id);
            return mapper.Map<OrderResultDto>(Order);
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string Email)
        {
            var Orders = unitOfWork.GetRepository<Order, Guid>()
                .GetAllWithSpecificationAsync(new OrderWithIncludeSpecifications(Email));
            return mapper.Map<IEnumerable<OrderResultDto>>(Orders);
        }
    }
}
