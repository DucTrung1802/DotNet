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
            // 1 District => 1 - Many Communes
            modelBuilder.Entity<District>().HasMany(e => e.Communes).WithOne(e => e.District).HasForeignKey(e => e.DistrictID).IsRequired();

            // 1 Commune => 1 - Many Villages
            modelBuilder.Entity<Commune>().HasMany(e => e.Villages).WithOne(e => e.Commune).HasForeignKey(e => e.CommuneID).IsRequired();
        }


        public DbSet<District> Districts { get; set; }
        public DbSet<Commune> Communes { get; set; }
        public DbSet<Village> Villages { get; set; }
    }
}
