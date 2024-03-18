using CRUDinCoreMVC.Models;
using Microsoft.EntityFrameworkCore;
namespace CRUDinCoreMVC.GenericRepository
{
    //The following GenericRepository class Implement the IGenericRepository Interface
    //And Here T is going to be a class
    //While Creating an Instance of the GenericRepository type, we need to specify the Class Name
    //That is we need to specify the actual Entity Nae for the type T
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //The following variable is going to hold the EFCoreDbContext instance
        protected readonly EFCoreDbContext _context;

        //The following Variable is going to hold the DbSet Entity
        protected readonly DbSet<T> _dbSet;

        //we are initializing the context object and DbSet variable
        public GenericRepository(EFCoreDbContext context)
        {
            _context = context;

            //Whatever Entity name we specify while creating the instance of GenericRepository
            //That Entity name  will be stored in the DbSet<T> variable
            _dbSet = context.Set<T>();
        }

        //This method will return all the Records from the table
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        //This method will return the specified record from the table
        //based on the Id which it received as an argument
        public async Task<T?> GetByIdAsync(object Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        //This method will Insert one object into the table
        //It will receive the object as an argument which needs to be inserted into the database
        public async Task InsertAsync(T Entity)
        {
            //It will mark the Entity state as Added
            await _dbSet.AddAsync(Entity);
        }

        //This method is going to update the record in the table
        //It will receive the object as an argument
        public async Task UpdateAsync(T Entity)
        {
            //It will mark the Entity state as Modified
            _dbSet.Update(Entity);
        }

        //This method is going to remove the record from the table
        //It will receive the primary key value as an argument whose information needs to be removed from the table
        public async Task DeleteAsync(object Id)
        {
            //First, fetch the record from the table
            var entity = await _dbSet.FindAsync(Id);
            if (entity != null)
            {
                //This will mark the Entity State as Deleted
                _dbSet.Remove(entity);
            }
        }

        //This method will make the changes permanent in the database
        //That means once we call InsertAsync, UpdateAsync, and DeleteAsync Methods, 
        //Then we need to call the SaveAsync method to make the changes permanent in the database
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}