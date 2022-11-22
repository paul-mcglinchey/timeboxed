using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Adding_SeedRolesPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Grants admin access to a group.");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AuditId", "Description", "Language", "Name" },
                values: new object[,]
                {
                    { 201, new Guid("00000000-0000-0000-0000-000000000000"), "Grants view access to rotas in the group.", "en-US", "View Rotas" },
                    { 202, new Guid("00000000-0000-0000-0000-000000000000"), "Grants add, edit & delete access to rotas in a group.", "en-US", "Add, Edit & Delete Rotas" },
                    { 301, new Guid("00000000-0000-0000-0000-000000000000"), "Grants view access to clients in a group.", "en-US", "View Clients" },
                    { 302, new Guid("00000000-0000-0000-0000-000000000000"), "Grants add, edit & delete access to clients in a group", "en-US", "Add, Edit & Delete Clients" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ApplicationId", "Description", "GroupId", "Name" },
                values: new object[,]
                {
                    { new Guid("78114707-a61a-46e7-9914-f918de3f89fa"), 1, "Intended to be assigned to highest level rota manager users.", null, "Rota Manager Admin" },
                    { new Guid("dcd622b3-400c-45e2-9451-23af77b7f835"), 2, "Intended to be assigned to highest level client manager users.", null, "Client Manager Admin" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "ApplicationId", "PermissionId" },
                values: new object[,]
                {
                    { 1, 201 },
                    { 1, 202 },
                    { 2, 301 },
                    { 2, 302 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 101, new Guid("78114707-a61a-46e7-9914-f918de3f89fa") },
                    { 101, new Guid("dcd622b3-400c-45e2-9451-23af77b7f835") },
                    { 201, new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb") },
                    { 201, new Guid("78114707-a61a-46e7-9914-f918de3f89fa") },
                    { 202, new Guid("78114707-a61a-46e7-9914-f918de3f89fa") },
                    { 301, new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230") },
                    { 301, new Guid("dcd622b3-400c-45e2-9451-23af77b7f835") },
                    { 302, new Guid("dcd622b3-400c-45e2-9451-23af77b7f835") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 1, 201 });

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 1, 202 });

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 2, 301 });

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 2, 302 });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 101, new Guid("78114707-a61a-46e7-9914-f918de3f89fa") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 101, new Guid("dcd622b3-400c-45e2-9451-23af77b7f835") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 201, new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 201, new Guid("78114707-a61a-46e7-9914-f918de3f89fa") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 202, new Guid("78114707-a61a-46e7-9914-f918de3f89fa") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 301, new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 301, new Guid("dcd622b3-400c-45e2-9451-23af77b7f835") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 302, new Guid("dcd622b3-400c-45e2-9451-23af77b7f835") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("78114707-a61a-46e7-9914-f918de3f89fa"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("dcd622b3-400c-45e2-9451-23af77b7f835"));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Grants admin access to a group");
        }
    }
}
