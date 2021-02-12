using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where  T: BaseEntity   // the baseentity class help the id to be shared by all
    {
         Task<T> GetByIdAsync(int id);
         Task<IReadOnlyList<T>> ListAllAsync();
         Task<T> GetEntityWithSpec(ISpecification<T> spec);
         Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

         Task<int> CountAsync(ISpecification<T> spec);  //this is to hold the count of the type T/e.g product to be used for pagination
         
         //Add, Update and Delete are not async as they are not directly related to the database but help in tracking while
         //these actions are happing in the memory because saving is left for unit of work
         void Add(T entity);  
         void Update(T entity);
         void Delete(T entity);
         
    }
}