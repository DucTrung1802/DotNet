using MagicVilla.Repositories;

namespace MagicVilla.UOW
{
    public interface IUnitOfWork
    {
        VillaRepository Villas { get; }

        void CreateTransaction();
        void Commit();
        void Rollback();
        Task Save();
    }
}
