using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Local), 5 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Local), 5 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Local), 6 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Local), 6 });

            migrationBuilder.InsertData(
                table: "AvailabilitySlots",
                columns: new[] { "Id", "Date", "DayOfWeek", "EndTime", "IsAvailable", "SlotType", "StartTime", "TherapistId", "TherapistProfileId" },
                values: new object[] { 5, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Local), 1, new TimeSpan(0, 10, 0, 0, 0), true, "Video", new TimeSpan(0, 10, 0, 0, 0), "943a7e8a-3164-4c88-be8b-58711088b81b", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), 4 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Local), 4 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Local), 5 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Local), 5 });
        }
    }
}
