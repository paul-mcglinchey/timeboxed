using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Configure_ApplicationPermission_UserApplication_12M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserApplication");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "UserApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PermissionUserApplication",
                columns: table => new
                {
                    PermissionsId = table.Column<int>(type: "int", nullable: false),
                    UserApplicationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionUserApplication", x => new { x.PermissionsId, x.UserApplicationsId });
                    table.ForeignKey(
                        name: "FK_PermissionUserApplication_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionUserApplication_UserApplications_UserApplicationsId",
                        column: x => x.UserApplicationsId,
                        principalTable: "UserApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionUserApplication_UserApplicationsId",
                table: "PermissionUserApplication",
                column: "UserApplicationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_Applications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_Applications_ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropTable(
                name: "PermissionUserApplication");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "UserApplications");

            migrationBuilder.CreateTable(
                name: "ApplicationUserApplication",
                columns: table => new
                {
                    ApplicationsId = table.Column<int>(type: "int", nullable: false),
                    UserApplicationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserApplication", x => new { x.ApplicationsId, x.UserApplicationsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplication_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplication_UserApplications_UserApplicationsId",
                        column: x => x.UserApplicationsId,
                        principalTable: "UserApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserApplication_UserApplicationsId",
                table: "ApplicationUserApplication",
                column: "UserApplicationsId");
        }
    }
}
