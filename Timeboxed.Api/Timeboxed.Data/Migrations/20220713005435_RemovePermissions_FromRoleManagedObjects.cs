using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class RemovePermissions_FromRoleManagedObjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserApplications_Permissions_PermissionId",
                table: "GroupUserApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Permissions_PermissionId",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_PermissionId",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_GroupUserApplications_PermissionId",
                table: "GroupUserApplications");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "GroupUserApplications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "GroupUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "GroupUserApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_PermissionId",
                table: "GroupUsers",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserApplications_PermissionId",
                table: "GroupUserApplications",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserApplications_Permissions_PermissionId",
                table: "GroupUserApplications",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Permissions_PermissionId",
                table: "GroupUsers",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }
    }
}
