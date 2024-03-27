using Microsoft.EntityFrameworkCore;

namespace FU_House_Finder.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            // Uncomment to OVERRIDE current table
            //Database.EnsureDeleted();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // 01 | 1 District => 1 -> many Communes | FK: DistrictID
            modelBuilder.Entity<District>().HasMany(e => e.Communes).WithOne(e => e.District).HasForeignKey(e => e.DistrictID).IsRequired();

            // 02 | 1 Commune => 1 -> many Villages | FK: CommuneID
            modelBuilder.Entity<Commune>().HasMany(e => e.Villages).WithOne(e => e.Commune).HasForeignKey(e => e.CommuneID).IsRequired();

            // 03 | 1 Village => 0 -> many Houses | FK: VillageID
            modelBuilder.Entity<Village>().HasMany(e => e.Houses).WithOne(e => e.Village).HasForeignKey(e => e.VillageID).IsRequired();

            // 04 | 1 Campus => 0 - many Houses | FK: CampusID
            modelBuilder.Entity<Campus>().HasMany(e => e.Houses).WithOne(e => e.Campus).HasForeignKey(e => e.VillageID).IsRequired();

            // 05 | 1 Address => 1 - many Campuses | FK: AddressID
            modelBuilder.Entity<Address>().HasMany(e => e.Campuses).WithOne(e => e.Address).HasForeignKey(e => e.AdressID).IsRequired();

            // 06 | 1 Adress => 1 House | FK: AddressID
            modelBuilder.Entity<Address>().HasOne(e => e.House).WithOne(e => e.Address).HasForeignKey<House>(e => e.AddressID).OnDelete(DeleteBehavior.NoAction);
        }

        // DbSet<T>
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Commune> Communes { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<HouseImage> HouseImages { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomHistory> RoomHistories { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Village> Villages { get; set; }
    }
}
