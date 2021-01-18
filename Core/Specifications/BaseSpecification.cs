using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    //The whole purpose of this class is   just to replace the multiple include statements we write in chaining our entities (eg. as in ProductRepository)
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria {get; }

        public List<Expression<Func<T, object>>> Includes {get; }= 
        new List<Expression<Func<T, object>>>();  //initialization to null list of include statements by default
        protected void AddInclude(Expression<Func<T, object>> includeExpression) //this method is just to include list to the above Includes list
        {
            Includes.Add(includeExpression);
        }

    }
}