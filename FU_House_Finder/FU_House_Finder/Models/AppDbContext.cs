using Microsoft.EntityFrameworkCore;

namespace FU_House_Finder.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>().HasMany(e => e.Communes).WithOne(e => e.District).HasForeignKey(e => e.DistrictID).IsRequired();
        }

        public DbSet<District> Districts { get; set; }
        public DbSet<Commune> Communes { get; set; }
    }
}
