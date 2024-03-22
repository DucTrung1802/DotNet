using MagicVilla.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Repositories
{
    public class VillaRepository : GenericRepository<Villa>, IVillaRepository
    {
        public VillaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Villa>> GetAllVillasAsync()
        {
            return await _context.Villas.ToListAsync
                ();
        }

        public async Task<Villa?> GetVillaByIdAsync(int VillaID)
        {
            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == VillaID);

            return villa;
        }

        public async Task<Villa?> GetVillaByNameAsync(string Name)
        {
            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Name.ToLower() == Name.ToLower());

            return villa;
        }

        public async Task<IEnumerable<Villa>> GetVillaHigherRateAsync(int RateThreshold)
        {
            var qualified_villas = await _context.Villas.Where(v => v.Rate >= RateThreshold).ToListAsync();

            return qualified_villas;
        }
    }
}
