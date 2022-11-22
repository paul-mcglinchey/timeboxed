using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Configure_GroupUser_12M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupUsers_GroupUserId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_GroupUsers_GroupUserId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_DefaultGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DefaultGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_GroupUserId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "DefaultGroupId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "GroupUserId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "GroupUserId",
                table: "Groups");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "GroupUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Groups_GroupId",
                table: "GroupUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Groups_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "DefaultGroupId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserId",
                table: "UserApplications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DefaultGroupId",
                table: "Users",
                column: "DefaultGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_GroupUserId",
                table: "UserApplications",
                column: "GroupUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_GroupUserId",
                table: "Permissions",
                column: "GroupUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_GroupUserId",
                table: "Groups",
                column: "GroupUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupUsers_GroupUserId",
                table: "Groups",
                column: "GroupUserId",
                principalTable: "GroupUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_GroupUsers_GroupUserId",
                table: "Permissions",
                column: "GroupUserId",
                principalTable: "GroupUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserId",
                table: "UserApplications",
                column: "GroupUserId",
                principalTable: "GroupUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_DefaultGroupId",
                table: "Users",
                column: "DefaultGroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
