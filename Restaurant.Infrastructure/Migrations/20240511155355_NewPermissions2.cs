using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewPermissions2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("ec0f0ff1-ec81-42af-9880-910682cdd04f"));

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 19, "CreateRole" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Firstname", "IsEmailConfirmed", "Lastname", "Password", "Phone", "RoleId" },
                values: new object[] { new Guid("17cfd1ef-6089-4ffc-9643-6907a90a98ea"), "owner@gmail.com", "name", true, "lastname", "3302df19c4918e8271ef446321e66f3abdc0defe3d28cdf3798d9ddd7fb1a7eb", "+380698432576", 1 });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 19, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 19, 1 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("17cfd1ef-6089-4ffc-9643-6907a90a98ea"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Firstname", "IsEmailConfirmed", "Lastname", "Password", "Phone", "RoleId" },
                values: new object[] { new Guid("ec0f0ff1-ec81-42af-9880-910682cdd04f"), "owner@gmail.com", "name", true, "lastname", "3302df19c4918e8271ef446321e66f3abdc0defe3d28cdf3798d9ddd7fb1a7eb", "+380698432576", 1 });
        }
    }
}
