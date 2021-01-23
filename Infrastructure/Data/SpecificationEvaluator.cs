using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, //static to access it without creating the object
        ISpecification<TEntity> spec)
        {
            var query = inputQuery;  //at this point dbset is will be coming from 

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); //e.g p=>p.ProductTypeId==id
            }

            //Apply ordering if expressins are set
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy); //e.g p=>p.OrderByID
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending); //e.g p=>p.ProductTypeId==id
            }
            //The order maters paging should come after the filtering operator
            if (spec.IsPagingEnabled)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }

            //this does the job of all the chained include statements  in ProductRepository (aggregate the two includes)
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}