using MagicVilla.Models;

namespace MagicVilla.Repositories
{
    public interface IVillaRepository : IGenericRepository<Villa>
    {
        // Here, you need to define the operations which are specific to Villa Entity
        Task<IEnumerable<Villa>> GetAllVillasAsync();
        Task<Villa?> GetVillaByIdAsync(int VillaID);
        Task<Villa?> GetVillaByNameAsync(string Name);
        Task<IEnumerable<Villa>> GetVillaHigherRateAsync(int RateThreshold);
    }
}
