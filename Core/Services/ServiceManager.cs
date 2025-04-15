using AutoMapper;
using Domain.Contracts;
using Services.Abstractions;
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
        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper,IBasketRepository basketRepository)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));
            _basketService = new Lazy<IBasketServices>(() => new BasketService(basketRepository, mapper));
        }
        public IProductService ProductService => _productService.Value;

        public IBasketServices BasketServices => _basketService.Value;
    }
}
