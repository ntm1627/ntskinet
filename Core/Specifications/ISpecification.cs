using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
         Expression<Func<T, bool>> Criteria {get;} //Criteria could be where for example id=1//get prod with id 1

         List<Expression<Func<T, object>>> Includes {get; }

    }
}