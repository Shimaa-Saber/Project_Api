using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metadata",
                table: "Notifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Metadata",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1,
                column: "Metadata",
                value: "{\"AppointmentId\":101,\"Date\":\"2025-04-29\",\"Time\":\"14:00\"}");

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2,
                column: "Metadata",
                value: "{\"Sender\":\"support@clinic.com\",\"Urgent\":true}");

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3,
                column: "Metadata",
                value: "{\"Amount\":50.00,\"Method\":\"Credit Card\"}");

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 4,
                column: "Metadata",
                value: "{\"Start\":\"01:00\",\"End\":\"03:00\",\"Timezone\":\"UTC\"}");
        }
    }
}
