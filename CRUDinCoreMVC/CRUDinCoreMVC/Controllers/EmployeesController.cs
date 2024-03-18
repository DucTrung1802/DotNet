using CRUDinCoreMVC.Models;
using CRUDinCoreMVC.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRUDinCoreMVC.Controllers
{
    public class EmployeesController : Controller
    {
        // Other than Employee Entity
        private readonly EFCoreDbContext _context;

        // For Employee Entity
        private readonly IGenericRepository<Employee> _repository;

        public EmployeesController(IGenericRepository<Employee> repository, EFCoreDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = from emp in await _repository.GetAllAsync()
                            join dept in _context.Departments.ToList()
                            on emp.DepartmentId equals dept.DepartmentId
                            into EmEmployeeDepartmentGroup
                            from departments in EmEmployeeDepartmentGroup.DefaultIfEmpty()
                            select new Employee
                            {
                                EmployeeId = emp.EmployeeId,
                                DepartmentId = emp.DepartmentId,
                                Name = emp.Name,
                                Email = emp.Email,
                                Position = emp.Position,
                                Department = departments
                            };
            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repository.GetByIdAsync(Convert.ToInt32(id));

            if (employee == null)
            {
                return NotFound();
            }

            employee.Department = await _context.Departments.FindAsync(employee.DepartmentId);

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Email,Position,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _repository.InsertAsync(employee);
                await _repository.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repository.GetByIdAsync(Convert.ToInt32(id));
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Email,Position,DepartmentId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(employee);
                    await _repository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var emp = await _repository.GetByIdAsync(employee.EmployeeId);
                    if (emp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "DepartmentId", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repository.GetByIdAsync(Convert.ToInt32(id));
            if (employee == null)
            {
                return NotFound();
            }

            employee.Department = await _context.Departments.FindAsync(employee.DepartmentId);

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _repository.GetByIdAsync(Convert.ToInt32(id));
            if (employee != null)
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
