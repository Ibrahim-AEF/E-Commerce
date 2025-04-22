using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketServices> _basketService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IOrderService> _orderService;
        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper,IBasketRepository basketRepository,UserManager<User> userManager,IOptions<Jwtoptions> options,IConfiguration configuration)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketServices>(() => new BasketService(basketRepository, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,configuration,options));
            _orderService = new Lazy<IOrderService>(() => new OrderService(mapper, basketRepository, unitOfWork));
        }
        public IProductService ProductService => _productService.Value;

        public IBasketServices BasketServices => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
