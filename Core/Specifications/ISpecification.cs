using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
         Expression<Func<T, bool>> Criteria {get;} //Criteria could be where for example id=1//get prod with id 1

         List<Expression<Func<T, object>>> Includes {get; }
         Expression<Func<T,object>> OrderBy {get;}
         Expression<Func<T,object>> OrderByDescending {get;}

        //The following three are for pagination for eg Take 5 elements and display in the second page so skip skip 5 from the first page
        int Take {get;}
        int Skip {get;}
        bool IsPagingEnabled {get;}
    }
}