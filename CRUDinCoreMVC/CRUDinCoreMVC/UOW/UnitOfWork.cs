using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using CRUDinCoreMVC.Models;
using CRUDinCoreMVC.Repository;

namespace CRUDinCoreMVC.UOW
{
    //Generic UnitOfWork Class
    //Implementing the IUnitOfWork and IDisposable Interfaces
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        //The following varibale will hold DbContext Instance
        public EFCoreDbContext Context = null;

        //The following varibale will hold the Transaction Instance
        private IDbContextTransaction? _objTran = null;

        //Using the following variable we can call the Operations of GenericRepository and EmployeeRepository
        public EmployeeRepository Employees { get; private set; }

        //Using the following variable we can call the Operations of GenericRepository and DepartmentRepository
        public DepartmentRepository Departments { get; private set; }

        //Initializing the Context, Employees, and Departments objects
        public UnitOfWork(EFCoreDbContext _Context)
        {
            Context = _Context;
            Employees = new EmployeeRepository(Context);
            Departments = new DepartmentRepository(Context);
        }

        //The CreateTransaction() method will create a database Transaction so that we can do database operations
        //by applying do everything and do nothing principle
        public void CreateTransaction()
        {
            //It will Begin the transaction on the underlying connection
            _objTran = Context.Database.BeginTransaction();
        }

        //If all the Transactions are completed successfully then we need to call this Commit() 
        //method to Save the changes permanently in the database
        public void Commit()
        {
            //Commits the underlying store transaction
            _objTran?.Commit();
        }

        //If at least one of the Transaction is Failed then we need to call this Rollback() 
        //method to Rollback the database changes to its previous state
        public void Rollback()
        {
            //Rolls back the underlying store transaction
            _objTran?.Rollback();

            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            _objTran?.Dispose();
        }

        //The Save() Method will Call the DbContext Class SaveChanges method 
        //So whenever we do a transaction we need to call this Save() method 
        //so that it will make the changes in the database permanently
        public async Task Save()
        {
            try
            {
                //Calling DbContext Class SaveChanges method 
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception, possibly logging the details
                // The InnerException often contains more specific details
                throw new Exception(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}