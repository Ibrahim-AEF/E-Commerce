using Domain.Contracts;
using Domain.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications:Specifications<Order>
    {
        //For Id
        public OrderWithIncludeSpecifications(Guid id)
            :base(o=>o.Id==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }
        //For Email
        public OrderWithIncludeSpecifications(string email)
            : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
            SetOrderBy(o => o.OrderDate);
        }
    }
}
