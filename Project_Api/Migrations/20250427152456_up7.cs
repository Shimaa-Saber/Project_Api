using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AvailabilitySlots",
                columns: new[] { "Id", "Date", "DayOfWeek", "EndTime", "IsBooked", "SlotType", "StartTime", "TherapistId", "TherapistProfileId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), 1, new TimeSpan(0, 10, 0, 0, 0), false, "Video", new TimeSpan(0, 9, 0, 0, 0), "d6570062-9ae7-4109-84bb-19770cb70d08", null },
                    { 2, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), 1, new TimeSpan(0, 15, 0, 0, 0), false, "InPerson", new TimeSpan(0, 14, 0, 0, 0), "d6570062-9ae7-4109-84bb-19770cb70d08", null },
                    { 3, new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local), 2, new TimeSpan(0, 11, 0, 0, 0), false, "Video", new TimeSpan(0, 10, 0, 0, 0), "d6570062-9ae7-4109-84bb-19770cb70d08", null }
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
