using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Api.Migrations
{
    /// <inheritdoc />
    public partial class up9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local), 2 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local), 2 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Local), 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local), 1 });

            migrationBuilder.UpdateData(
                table: "AvailabilitySlots",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "DayOfWeek" },
                values: new object[] { new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local), 2 });
        }
    }
}
