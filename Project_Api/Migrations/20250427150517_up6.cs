using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "seed-therapist-1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "IsVerified", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "seed-therapist-1", 0, "01cf4143-32fc-4efb-b3b2-458bdcd66d8b", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "therapist1@test.com", true, "Dr. Test Therapist", "Male", true, true, null, "THERAPIST1@TEST.COM", "TESTTHERAPIST1", "AQAAAAIAAYagAAAAEHyipAH1XW2HJm98N2RvAOJjRlWjMIBtYtTpQnRAT+FfFJ36wZGIT5LhUjJZaPKX2Q==", "1234567890", false, "1e3d7a79-60cb-47b1-9351-6fc1da7c9d53", false, "testtherapist1" });

            migrationBuilder.InsertData(
                table: "AvailabilitySlots",
                columns: new[] { "Id", "Date", "DayOfWeek", "EndTime", "IsBooked", "SlotType", "StartTime", "TherapistId", "TherapistProfileId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), 1, new TimeSpan(0, 10, 0, 0, 0), false, "Video", new TimeSpan(0, 9, 0, 0, 0), "seed-therapist-1", null },
                    { 2, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), 1, new TimeSpan(0, 15, 0, 0, 0), false, "InPerson", new TimeSpan(0, 14, 0, 0, 0), "seed-therapist-1", null },
                    { 3, new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local), 2, new TimeSpan(0, 11, 0, 0, 0), false, "Video", new TimeSpan(0, 10, 0, 0, 0), "seed-therapist-1", null }
                });
        }
    }
}
