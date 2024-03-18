using CRUDinCoreMVC.Repository;
namespace CRUDinCoreMVC.UOW
{
    public interface IUnitOfWork
    {
        //Define the Specific Repositories
        EmployeeRepository Employees { get; }
        DepartmentRepository Departments { get; }

        //This Method will Start the database Transaction
        void CreateTransaction();

        //This Method will Commit the database Transaction
        void Commit();

        //This Method will Rollback the database Transaction
        void Rollback();

        //This Method will call the SaveChanges method
        Task Save();
    }
}