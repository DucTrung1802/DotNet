using Microsoft.EntityFrameworkCore;
namespace MagicVilla.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Luxury Retreat Villa",
                    Details = "A luxurious villa with breathtaking views.",
                    Rate = 500.00,
                    Sqft = 3500,
                    Occupancy = 8,
                    ImageUrl = "https://example.com/luxury-villa-image1.jpg",
                    Amenity = "Private pool, Jacuzzi, BBQ area",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Name = "Seaside Serenity Villa",
                    Details = "Experience tranquility by the sea in this beautiful villa.",
                    Rate = 400.00,
                    Sqft = 2800,
                    Occupancy = 6,
                    ImageUrl = "https://example.com/seaside-villa-image1.jpg",
                    Amenity = "Direct beach access, Outdoor dining area",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 3,
                    Name = "Mountain Escape Villa",
                    Details = "Escape to the mountains in this cozy villa surrounded by nature.",
                    Rate = 300.00,
                    Sqft = 2000,
                    Occupancy = 4,
                    ImageUrl = "https://example.com/mountain-villa-image1.jpg",
                    Amenity = "Fireplace, Hiking trails nearby",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 4,
                    Name = "Rustic Charm Villa",
                    Details = "Experience rustic charm in this countryside villa.",
                    Rate = 250.00,
                    Sqft = 1800,
                    Occupancy = 5,
                    ImageUrl = "https://example.com/rustic-villa-image1.jpg",
                    Amenity = "Garden, Outdoor seating area",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 5,
                    Name = "Urban Oasis Villa",
                    Details = "An oasis in the heart of the city, offering luxury and convenience.",
                    Rate = 600.00,
                    Sqft = 3200,
                    Occupancy = 7,
                    ImageUrl = "https://example.com/urban-villa-image1.jpg",
                    Amenity = "Rooftop terrace, Gym facilities",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
                );
        }

        public DbSet<Villa> Villas { get; set; }
    }
}
