using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Seed_ApplicationsPermissionsRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            migrationBuilder.CreateTable(
                name: "ApplicationRole",
                columns: table => new
                {
                    ApplicationsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRole", x => new { x.ApplicationsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ApplicationRole_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Id", "AuditId", "BackgroundImage", "BackgroundVideo", "Colour", "Description", "Icon", "Name", "Url" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/pmcglinchey/image/upload/v1656632136/smoulderedsignals_19201080_m0nwwf.png", "https://res.cloudinary.com/pmcglinchey/video/upload/v1656873263/smoulderingsignals_960540_looping_qu42se.mp4", "#6d28d9", "A first class environment for managing rotas & employees in your business.", null, "Rota Manager", "/rotas/dashboard" },
                    { 2, new Guid("00000000-0000-0000-0000-000000000000"), "https://res.cloudinary.com/pmcglinchey/image/upload/v1656621688/electricwaves_19201080_ll3sa9.png", "https://res.cloudinary.com/pmcglinchey/video/upload/v1656873057/electricwaves_960540_looping_lczpjp.mp4", "#e11d48", "A complete package for managing clients allowing you to spend more time where it really matters.", null, "Client Manager", "/clients/dashboard" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AuditId", "Description", "Language", "Name" },
                values: new object[,]
                {
                    { 1, new Guid("00000000-0000-0000-0000-000000000000"), "Grants access to a group.", "en-US", "Group Access" },
                    { 2, new Guid("00000000-0000-0000-0000-000000000000"), "Grants access to an application", "en-US", "Application Access" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2"), "Intended to be assigned to highest level group members.", "Group Admin" });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "ApplicationId", "PermissionId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "ApplicationPermissions",
                columns: new[] { "ApplicationId", "PermissionId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2") });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRole_RolesId",
                table: "ApplicationRole",
                column: "RolesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRole");

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ApplicationPermissions",
                keyColumns: new[] { "ApplicationId", "PermissionId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2") });

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1ba0c5a2-00a5-4b37-9e97-cc354ad6d9e2"));

            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => new { x.ApplicationId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationRoles_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoles_RoleId",
                table: "ApplicationRoles",
                column: "RoleId");
        }
    }
}
