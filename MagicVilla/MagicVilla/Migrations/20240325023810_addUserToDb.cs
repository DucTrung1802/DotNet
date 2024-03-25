using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla.Migrations
{
    /// <inheritdoc />
    public partial class addUserToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7546), new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7555) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7558), new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7558) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7560), new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7561), new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7562) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7563), new DateTime(2024, 3, 25, 9, 38, 10, 550, DateTimeKind.Local).AddTicks(7563) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalUsers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9200), new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9211) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9213), new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9214) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9216), new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9216) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9218), new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9218) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9220), new DateTime(2024, 3, 21, 12, 27, 24, 238, DateTimeKind.Local).AddTicks(9220) });
        }
    }
}
