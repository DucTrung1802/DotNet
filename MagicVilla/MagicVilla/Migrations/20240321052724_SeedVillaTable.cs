using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Private pool, Jacuzzi, BBQ area", new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9200), "A luxurious villa with breathtaking views.", "https://example.com/luxury-villa-image1.jpg", "Luxury Retreat Villa", 8, 500.0, 3500, new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9211) },
                    { 2, "Direct beach access, Outdoor dining area", new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9213), "Experience tranquility by the sea in this beautiful villa.", "https://example.com/seaside-villa-image1.jpg", "Seaside Serenity Villa", 6, 400.0, 2800, new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9214) },
                    { 3, "Fireplace, Hiking trails nearby", new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9216), "Escape to the mountains in this cozy villa surrounded by nature.", "https://example.com/mountain-villa-image1.jpg", "Mountain Escape Villa", 4, 300.0, 2000, new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9216) },
                    { 4, "Garden, Outdoor seating area", new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9218), "Experience rustic charm in this countryside villa.", "https://example.com/rustic-villa-image1.jpg", "Rustic Charm Villa", 5, 250.0, 1800, new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9218) },
                    { 5, "Rooftop terrace, Gym facilities", new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9220), "An oasis in the heart of the city, offering luxury and convenience.", "https://example.com/urban-villa-image1.jpg", "Urban Oasis Villa", 7, 600.0, 3200, new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9220) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
