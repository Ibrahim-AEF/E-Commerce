using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliveryMethodExceptions(int id):NotFoundException($"The DeliveryMethod With Id {id} Not Found")
    {

    }
}
