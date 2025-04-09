using Domain.Contracts;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<T>GetQuery<T>
            (
            IQueryable<T>InputQuery,
            Specifications<T> specifications
            ) where T:class
        {
            //step 1 : Get The DbSet
            var query = InputQuery;
            //step 2 : Criteria
            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);
            //step 3 : Includes
            //foreach(var item in specifications.IncludeExpressions)
            //{
            //    query = query.Include(item);
            //}
            query = specifications.IncludeExpressions.Aggregate
                (query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));
            //step 5 : retyrn query
            return query;
        }
    }
}
