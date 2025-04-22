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
        #region For Filteration And Sorting
        public Expression<Func<T,object>> OrderBy { get; private set; }
        public Expression<Func<T,object>> OrderByDescending { get; private set; }
        #endregion
        #region For Paginations
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginated { get; set; }
        
        #endregion
        protected void AddInclude(Expression<Func<T, object>> expression)
            => IncludeExpressions.Add(expression);
        protected void SetOrderBy(Expression<Func<T, object>> expression)
            => OrderBy = expression;
        protected void SetOrderByDescending(Expression<Func<T, object>> expression)
            => OrderByDescending = expression;
        protected void ApplyPagination(int PageIndex,int PageSize)
        {
            IsPaginated = true;
            Take=PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
    }
}
