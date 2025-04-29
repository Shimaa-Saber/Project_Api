using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "CreatedAt", "IsRead", "Message", "Metadata", "RelatedId", "Title", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Your session with Dr. Smith is confirmed for tomorrow at 2 PM", "{\"AppointmentId\":101,\"Date\":\"2025-04-29\",\"Time\":\"14:00\"}", 0, "Appointment Confirmed", "Appointment", "12b0710c-c16a-4ce8-a17b-b725104ef749" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "You have 1 new message in your inbox", "{\"Sender\":\"support@clinic.com\",\"Urgent\":true}", 0, "New Message Received", "Message", "12b0710c-c16a-4ce8-a17b-b725104ef749" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Your payment of $50.00 was completed successfully", "{\"Amount\":50.00,\"Method\":\"Credit Card\"}", 0, "Payment Processed", "Payment", "12b0710c-c16a-4ce8-a17b-b725104ef749" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Scheduled maintenance tonight from 1AM to 3AM", "{\"Start\":\"01:00\",\"End\":\"03:00\",\"Timezone\":\"UTC\"}", 0, "System Maintenance", "System", "12b0710c-c16a-4ce8-a17b-b725104ef749" }
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
