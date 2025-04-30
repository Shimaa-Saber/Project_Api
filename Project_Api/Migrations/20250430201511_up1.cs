using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AvailabilitySlots",
                columns: new[] { "Id", "Date", "DayOfWeek", "EndTime", "IsAvailable", "SlotType", "StartTime", "TherapistId", "TherapistProfileId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), 4, new TimeSpan(0, 10, 0, 0, 0), true, "Video", new TimeSpan(0, 9, 0, 0, 0), "943a7e8a-3164-4c88-be8b-58711088b81b", null },
                    { 2, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), 4, new TimeSpan(0, 15, 0, 0, 0), true, "InPerson", new TimeSpan(0, 14, 0, 0, 0), "943a7e8a-3164-4c88-be8b-58711088b81b", null },
                    { 3, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Local), 5, new TimeSpan(0, 11, 0, 0, 0), true, "Video", new TimeSpan(0, 10, 0, 0, 0), "943a7e8a-3164-4c88-be8b-58711088b81b", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
