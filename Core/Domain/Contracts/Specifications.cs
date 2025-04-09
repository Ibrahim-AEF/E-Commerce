using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T:class
    {
        public Specifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria=criteria;
        }
        //Where(P=>P.id==id)=>Where Condition
        public Expression<Func<T,bool>>? Criteria { get; private set; }
        //include(P=>P.ProductType).include(P=>P.ProductBrand)
        public List<Expression<Func<T, object>>> IncludeExpressions { get; private set; } = new();
        protected void AddInclude(Expression<Func<T, object>> expression)
            => IncludeExpressions.Add(expression);
    }
}
