using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Groups_1M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultGroup",
                table: "Users",
                newName: "DefaultGroupGroupId");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Audits",
                newName: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "AuditId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AuditId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImage",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundVideo",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Colour",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DefaultGroupGroupId",
                table: "Users",
                column: "DefaultGroupGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AuditId",
                table: "Permissions",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_UserId",
                table: "GroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_User",
                table: "Audits",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AuditId",
                table: "Applications",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_PermissionId",
                table: "Applications",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Audits_AuditId",
                table: "Applications",
                column: "AuditId",
                principalTable: "Audits",
                principalColumn: "AuditId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Permissions_PermissionId",
                table: "Applications",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audits_Users_User",
                table: "Audits",
                column: "User",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Users_UserId",
                table: "GroupUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Audits_AuditId",
                table: "Permissions",
                column: "AuditId",
                principalTable: "Audits",
                principalColumn: "AuditId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_Applications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Groups_DefaultGroupGroupId",
                table: "Users",
                column: "DefaultGroupGroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Audits_AuditId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Permissions_PermissionId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Audits_Users_User",
                table: "Audits");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Users_UserId",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Audits_AuditId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_Applications_ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Groups_DefaultGroupGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DefaultGroupGroupId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_AuditId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_UserId",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_Audits_User",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Applications_AuditId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PermissionId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AuditId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "AuditId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "BackgroundImage",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "BackgroundVideo",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Colour",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "DefaultGroupGroupId",
                table: "Users",
                newName: "DefaultGroup");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Audits",
                newName: "UpdatedBy");
        }
    }
}
