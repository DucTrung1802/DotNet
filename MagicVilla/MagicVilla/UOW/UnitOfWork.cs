using MagicVilla.Models;
using MagicVilla.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MagicVilla.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        //The following varibale will hold DbContext Instance
        public ApplicationDbContext? _context = null;

        //The following varibale will hold the Transaction Instance
        private IDbContextTransaction? _objTran = null;

        public VillaRepository Villas { get; private set; }

        public UnitOfWork(ApplicationDbContext? context)
        {
            if (context == null) throw new ArgumentNullException("context must not be null");

            _context = context;
            Villas = new VillaRepository(context);
        }

        public void CreateTransaction()
        {
            _objTran = _context!.Database.BeginTransaction();

        }

        public void Commit()
        {
            _objTran?.Commit();
        }

        public void Rollback()
        {
            _objTran?.Rollback();
            _objTran?.Dispose();
        }

        public async Task Save()
        {
            try
            {
                await _context!.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            _context!.Dispose();
        }
    }
}
