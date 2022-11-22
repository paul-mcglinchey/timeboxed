using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Roles_AddedApplicationProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AuditId", "Description", "Language", "Name" },
                values: new object[] { 102, new Guid("00000000-0000-0000-0000-000000000000"), "Grants admin access to an application.", "en-US", "Application Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ApplicationId", "Description", "GroupId", "Name" },
                values: new object[,]
                {
                    { new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230"), 2, "Intended to be assigned to base level client manager users.", null, "Client Manager User" },
                    { new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb"), 1, "Intended to be assigned to base level rota manager users.", null, "Rota Manager User" },
                    { new Guid("39a759e0-4991-498f-8612-4e2fc709a9b2"), null, "Intended to be assigned to base level group members.", null, "Group Member" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "ApplicationId", "PermissionId" },
                values: new object[,]
                {
                    { 1, 102 },
                    { 2, 102 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, new Guid("39a759e0-4991-498f-8612-4e2fc709a9b2") },
                    { 101, new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230") },
                    { 101, new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ApplicationId",
                table: "Roles",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Applications_ApplicationId",
                table: "Roles",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Applications_ApplicationId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ApplicationId",
                table: "Roles");

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 1, 102 });

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 2, 102 });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, new Guid("39a759e0-4991-498f-8612-4e2fc709a9b2") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 101, new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230") });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 101, new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb") });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("16befbf9-1ade-4fad-b0ed-0080f82e8230"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("24cda806-3a28-45f8-9f6d-d64a613385cb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("39a759e0-4991-498f-8612-4e2fc709a9b2"));

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Roles");
        }
    }
}
