using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class RelationShip_Adjustment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_UserApplications_UserApplicationId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_DefaultGroupGroupId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DefaultGroupGroupId",
                table: "Users",
                newName: "DefaultGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DefaultGroupGroupId",
                table: "Users",
                newName: "IX_Users_DefaultGroupId");

            migrationBuilder.RenameColumn(
                name: "UserApplicationId",
                table: "Permissions",
                newName: "Permission");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_UserApplicationId",
                table: "Permissions",
                newName: "IX_Permissions_Permission");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_UserApplications_Permission",
                table: "Permissions",
                column: "Permission",
                principalTable: "UserApplications",
                principalColumn: "UserApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_DefaultGroupId",
                table: "Users",
                column: "DefaultGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_UserApplications_Permission",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_DefaultGroupId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "DefaultGroupId",
                table: "Users",
                newName: "DefaultGroupGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_DefaultGroupId",
                table: "Users",
                newName: "IX_Users_DefaultGroupGroupId");

            migrationBuilder.RenameColumn(
                name: "Permission",
                table: "Permissions",
                newName: "UserApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissions_Permission",
                table: "Permissions",
                newName: "IX_Permissions_UserApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_UserApplications_UserApplicationId",
                table: "Permissions",
                column: "UserApplicationId",
                principalTable: "UserApplications",
                principalColumn: "UserApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_DefaultGroupGroupId",
                table: "Users",
                column: "DefaultGroupGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }
    }
}
