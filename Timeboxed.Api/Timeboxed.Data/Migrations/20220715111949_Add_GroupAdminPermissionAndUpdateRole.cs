using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Add_GroupAdminPermissionAndUpdateRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Grants admin access to a group", "Group Admin Access" });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AuditId", "Description", "Language", "Name" },
                values: new object[] { 101, new Guid("00000000-0000-0000-0000-000000000000"), "Grants access to an application.", "en-US", "Application Access" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 2, new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2") });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "ApplicationId", "PermissionId" },
                values: new object[] { 1, 101 });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "ApplicationId", "PermissionId" },
                values: new object[] { 2, 101 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 1, 101 });

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 2, 101 });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 2, new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "ApplicationId", "PermissionId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "ApplicationId", "PermissionId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Grants access to an application", "Application Access" });
        }
    }
}
