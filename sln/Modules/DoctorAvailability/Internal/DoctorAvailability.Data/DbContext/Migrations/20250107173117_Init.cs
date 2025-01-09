using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAvailability.Data.DbContext.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DoctorAvailability");

            migrationBuilder.CreateTable(
                name: "DoctorSlots",
                schema: "DoctorAvailability",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DoctorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DoctorName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsReserved = table.Column<bool>(type: "INTEGER", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSlots", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorSlots",
                schema: "DoctorAvailability");
        }
    }
}
