using CRUDinCoreMVC.GenericRepository;
using CRUDinCoreMVC.Models;

namespace CRUDinCoreMVC.Repository
{
    //Note: if you want to add some methods specific to the Department Entity, you can define here
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
    }

    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EFCoreDbContext context) : base(context) { }
    }
}