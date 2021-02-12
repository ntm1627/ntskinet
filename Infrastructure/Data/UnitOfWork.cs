using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext __context;
        private Hashtable _repositories;  //stores the repositories that will be created, but Dictionary is better for speed
        public UnitOfWork(StoreContext _context)
        {
            __context = _context;
        }

        public async Task<int> Complete()
        {
            return await __context.SaveChangesAsync();
        }

        public void Dispose()
        {
            __context.Dispose();
        }
        //A method to return repositores from the unit of work
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null) _repositories = new Hashtable(); //if there is not repository 

            var type = typeof(TEntity).Name; //If there is, find the type

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), __context);
                _repositories.Add(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[type];

            //suggested by a student 
            
            //The below code does the same job but it will be quicker because there is no boxing and unboxing.

            // Defined above -----private Dictionary<Type, object> repositories = new Dictionary<Type, object>() 
            /*
            public IRepository<T> Repository<T>() where T : class
            {
                if (repositories.Keys.Contains(typeof(T)) == true)
                {
                    return repositories[typeof(T)] as IRepository<T>;
                }
                IRepository<T> repo = new GenericRepository<T>(entities);
                repositories.Add(typeof(T), repo); return repo;
            }
            */

        }
    }
}