using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBasketServices
    {
        public Task<BasketDto?> GetBasketAsync(string id);
        public Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto);
        public Task<bool> DeleteBasketAsync(string id);
    }
}
