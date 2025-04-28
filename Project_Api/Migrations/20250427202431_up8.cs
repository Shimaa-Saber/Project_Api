using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBooked",
                table: "AvailabilitySlots",
                newName: "IsAvailable");

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsAvailable",
                value: true);

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsAvailable",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "AvailabilitySlots",
                newName: "IsBooked");

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsBooked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsBooked",
                value: false);

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsBooked",
                value: false);
        }
    }
}
