using CRUDinCoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDinCoreMVC.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //The following variable is going to hold the EmployeeDBContext instance
        private readonly EFCoreDbContext _context;

        //Initializing the EmployeeDBContext instance which it received as an argument
        //MVC Framework DI Container will provide the EFCoreDbContext instance
        public EmployeeRepository(EFCoreDbContext context)
        {
            _context = context;
        }

        //Returns all employees from the database.
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.AsNoTracking().Include(e => e.Department).ToListAsync();
        }
  

        //Retrieves a single employee by their ID.
        public async Task<Employee?> GetByIdAsync(int EmployeeID)
        {
            var employee = await _context.Employees.AsNoTracking().Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == EmployeeID);

            return employee;
        }

        //Adds a new employee to the database.
        public async Task InsertAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        //Updates an existing employee's details.
        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        //Deletes an employee from the database
        public async Task DeleteAsync(int EmployeeID)
        {
            var employee = await _context.Employees.FindAsync(EmployeeID);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }

        //InsertAsync, UpdateAsync, and DeleteAsync methods,
        //remember to call SaveAsync to commit the changes to the database.
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
