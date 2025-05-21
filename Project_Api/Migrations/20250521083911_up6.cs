using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "TherapistReviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseAt",
                table: "TherapistReviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TherapistResponse",
                table: "TherapistReviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "TherapistProfiles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), 4 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), 4 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), 5 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 24, 0, 0, 0, 0, DateTimeKind.Local), 5 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Local), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_TherapistReviews_ClientId",
                table: "TherapistReviews",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_TherapistReviews_AspNetUsers_ClientId",
                table: "TherapistReviews",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TherapistReviews_AspNetUsers_ClientId",
                table: "TherapistReviews");

            migrationBuilder.DropIndex(
                name: "IX_TherapistReviews_ClientId",
                table: "TherapistReviews");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "TherapistReviews");

            migrationBuilder.DropColumn(
                name: "ResponseAt",
                table: "TherapistReviews");

            migrationBuilder.DropColumn(
                name: "TherapistResponse",
                table: "TherapistReviews");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "TherapistProfiles");

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

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Local), 1 });
        }
    }
}
