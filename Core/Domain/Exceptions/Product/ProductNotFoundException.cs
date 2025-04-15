using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.Product
{
    public class ProductNotFoundException:NotFoundException
    {
        public ProductNotFoundException(int id)
            :base($"Product With {id} Not Found")
        {

        }
    }
}
