﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }
        public IBasketServices BasketServices { get; }
        public IAuthenticationService AuthenticationService { get; }
        public IOrderService OrderService { get; }
    }
}
