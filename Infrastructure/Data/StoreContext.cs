
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        //the base is passting the options to the partent class DbContect class
         public StoreContext(DbContextOptions<StoreContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}