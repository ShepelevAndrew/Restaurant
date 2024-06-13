using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewRoleDefaultRuleForCourier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("fdf8459c-8888-4777-8340-9b4dcd83b1ff"));

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 7, 4 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Firstname", "IsEmailConfirmed", "Lastname", "Password", "Phone", "RoleId" },
                values: new object[] { new Guid("b7d74805-6d9f-40ff-8ac2-5c7ab7f40db0"), "owner@gmail.com", "name", true, "lastname", "3302df19c4918e8271ef446321e66f3abdc0defe3d28cdf3798d9ddd7fb1a7eb", "+380698432576", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("b7d74805-6d9f-40ff-8ac2-5c7ab7f40db0"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Firstname", "IsEmailConfirmed", "Lastname", "Password", "Phone", "RoleId" },
                values: new object[] { new Guid("fdf8459c-8888-4777-8340-9b4dcd83b1ff"), "owner@gmail.com", "name", true, "lastname", "3302df19c4918e8271ef446321e66f3abdc0defe3d28cdf3798d9ddd7fb1a7eb", "+380698432576", 1 });
        }
    }
}
