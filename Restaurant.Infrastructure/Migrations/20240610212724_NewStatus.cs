using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("684298a7-9ecb-4cf8-aaad-a2704662fdbe"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CookedDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Firstname", "IsEmailConfirmed", "Lastname", "Password", "Phone", "RoleId" },
                values: new object[] { new Guid("fdf8459c-8888-4777-8340-9b4dcd83b1ff"), "owner@gmail.com", "name", true, "lastname", "3302df19c4918e8271ef446321e66f3abdc0defe3d28cdf3798d9ddd7fb1a7eb", "+380698432576", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("fdf8459c-8888-4777-8340-9b4dcd83b1ff"));

            migrationBuilder.DropColumn(
                name: "CookedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Firstname", "IsEmailConfirmed", "Lastname", "Password", "Phone", "RoleId" },
                values: new object[] { new Guid("684298a7-9ecb-4cf8-aaad-a2704662fdbe"), "owner@gmail.com", "name", true, "lastname", "3302df19c4918e8271ef446321e66f3abdc0defe3d28cdf3798d9ddd7fb1a7eb", "+380698432576", 1 });
        }
    }
}
