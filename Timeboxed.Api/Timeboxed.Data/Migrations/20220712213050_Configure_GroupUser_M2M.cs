using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Configure_GroupUser_M2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserPermission_GroupUsers_GroupUsersId",
                table: "GroupUserPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUserPermission",
                table: "GroupUserPermission");

            migrationBuilder.DropIndex(
                name: "IX_GroupUserPermission_PermissionsId",
                table: "GroupUserPermission");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroupUsers");

            migrationBuilder.RenameColumn(
                name: "GroupUserId",
                table: "UserApplications",
                newName: "GroupUserUserId");

            migrationBuilder.RenameColumn(
                name: "GroupUsersId",
                table: "GroupUserPermission",
                newName: "GroupUsersUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserGroupId",
                table: "UserApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUsersGroupId",
                table: "GroupUserPermission",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUserPermission",
                table: "GroupUserPermission",
                columns: new[] { "PermissionsId", "GroupUsersGroupId", "GroupUsersUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_GroupUserGroupId_GroupUserUserId",
                table: "UserApplications",
                columns: new[] { "GroupUserGroupId", "GroupUserUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserPermission_GroupUsersGroupId_GroupUsersUserId",
                table: "GroupUserPermission",
                columns: new[] { "GroupUsersGroupId", "GroupUsersUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserPermission_GroupUsers_GroupUsersGroupId_GroupUsersUserId",
                table: "GroupUserPermission",
                columns: new[] { "GroupUsersGroupId", "GroupUsersUserId" },
                principalTable: "GroupUsers",
                principalColumns: new[] { "GroupId", "UserId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserGroupId_GroupUserUserId",
                table: "UserApplications",
                columns: new[] { "GroupUserGroupId", "GroupUserUserId" },
                principalTable: "GroupUsers",
                principalColumns: new[] { "GroupId", "UserId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUserPermission_GroupUsers_GroupUsersGroupId_GroupUsersUserId",
                table: "GroupUserPermission");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserGroupId_GroupUserUserId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_GroupUserGroupId_GroupUserUserId",
                table: "UserApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupUserPermission",
                table: "GroupUserPermission");

            migrationBuilder.DropIndex(
                name: "IX_GroupUserPermission_GroupUsersGroupId_GroupUsersUserId",
                table: "GroupUserPermission");

            migrationBuilder.DropColumn(
                name: "GroupUserGroupId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "GroupUsersGroupId",
                table: "GroupUserPermission");

            migrationBuilder.RenameColumn(
                name: "GroupUserUserId",
                table: "UserApplications",
                newName: "GroupUserId");

            migrationBuilder.RenameColumn(
                name: "GroupUsersUserId",
                table: "GroupUserPermission",
                newName: "GroupUsersId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GroupUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUsers",
                table: "GroupUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupUserPermission",
                table: "GroupUserPermission",
                columns: new[] { "GroupUsersId", "PermissionsId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_GroupUserId",
                table: "UserApplications",
                column: "GroupUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserPermission_PermissionsId",
                table: "GroupUserPermission",
                column: "PermissionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUserPermission_GroupUsers_GroupUsersId",
                table: "GroupUserPermission",
                column: "GroupUsersId",
                principalTable: "GroupUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserId",
                table: "UserApplications",
                column: "GroupUserId",
                principalTable: "GroupUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
