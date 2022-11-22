using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Remove_IdPrefixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationGroup_Applications_ApplicationsApplicationId",
                table: "ApplicationGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationGroup_Groups_GroupsGroupId",
                table: "ApplicationGroup");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserApplicationId",
                table: "UserApplications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "Permissions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GroupUserId",
                table: "GroupUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Groups",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AuditId",
                table: "Audits",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Applications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GroupsGroupId",
                table: "ApplicationGroup",
                newName: "GroupsId");

            migrationBuilder.RenameColumn(
                name: "ApplicationsApplicationId",
                table: "ApplicationGroup",
                newName: "ApplicationsId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationGroup_GroupsGroupId",
                table: "ApplicationGroup",
                newName: "IX_ApplicationGroup_GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationGroup_Applications_ApplicationsId",
                table: "ApplicationGroup",
                column: "ApplicationsId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationGroup_Groups_GroupsId",
                table: "ApplicationGroup",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationGroup_Applications_ApplicationsId",
                table: "ApplicationGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationGroup_Groups_GroupsId",
                table: "ApplicationGroup");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserApplications",
                newName: "UserApplicationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Permissions",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "GroupUsers",
                newName: "GroupUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Groups",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Audits",
                newName: "AuditId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Applications",
                newName: "ApplicationId");

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "ApplicationGroup",
                newName: "GroupsGroupId");

            migrationBuilder.RenameColumn(
                name: "ApplicationsId",
                table: "ApplicationGroup",
                newName: "ApplicationsApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationGroup_GroupsId",
                table: "ApplicationGroup",
                newName: "IX_ApplicationGroup_GroupsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationGroup_Applications_ApplicationsApplicationId",
                table: "ApplicationGroup",
                column: "ApplicationsApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationGroup_Groups_GroupsGroupId",
                table: "ApplicationGroup",
                column: "GroupsGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
