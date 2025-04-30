using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "CreatedAt", "IsRead", "Message", "RelatedId", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Your session with Dr. Smith is confirmed for tomorrow at 2 PM", 0, "Appointment Confirmed", "Appointment", "cbb35a0f-83a4-4427-8647-bb9cab7aa8b2" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "You have 1 new message in your inbox", 0, "New Message Received", "Message", "cbb35a0f-83a4-4427-8647-bb9cab7aa8b2" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Your payment of $50.00 was completed successfully", 0, "Payment Processed", "Payment", "cbb35a0f-83a4-4427-8647-bb9cab7aa8b2" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Scheduled maintenance tonight from 1AM to 3AM", 0, "System Maintenance", "System", "cbb35a0f-83a4-4427-8647-bb9cab7aa8b2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
