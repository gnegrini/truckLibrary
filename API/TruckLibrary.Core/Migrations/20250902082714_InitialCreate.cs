using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TruckLibrary.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false),
                    ManufacturingYear = table.Column<int>(type: "INTEGER", nullable: false),
                    ChassisCode = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ManufacturingPlant = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "ChassisCode", "Color", "ManufacturingPlant", "ManufacturingYear", "Model" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "FH1234567", "Red", "Brazil", 2025, "FH" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "FM2345678", "Blue", "Sweden", 2025, "FM" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "VM3456789", "White", "United States", 2025, "VM" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks");
        }
    }
}
