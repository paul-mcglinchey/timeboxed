using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Configure_GroupUser_Permissions_M2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_Permissions_PermissionId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_PermissionId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "UserApplications");

            migrationBuilder.CreateTable(
                name: "GroupUserPermission",
                columns: table => new
                {
                    GroupUsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserPermission", x => new { x.GroupUsersId, x.PermissionsId });
                    table.ForeignKey(
                        name: "FK_GroupUserPermission_GroupUsers_GroupUsersId",
                        column: x => x.GroupUsersId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserPermission_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserPermission_PermissionsId",
                table: "GroupUserPermission",
                column: "PermissionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUserPermission");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "UserApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_PermissionId",
                table: "UserApplications",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_Permissions_PermissionId",
                table: "UserApplications",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }
    }
}
