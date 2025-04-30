using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AvailabilitySlots",
                columns: new[] { "Id", "Date", "DayOfWeek", "EndTime", "IsAvailable", "SlotType", "StartTime", "TherapistId", "TherapistProfileId" },
                values: new object[] { 4, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Local), 5, new TimeSpan(0, 11, 0, 0, 0), true, "IsPerson", new TimeSpan(0, 10, 0, 0, 0), "943a7e8a-3164-4c88-be8b-58711088b81b", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
