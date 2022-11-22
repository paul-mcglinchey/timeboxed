using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class ApplicationPermission_M2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Audits_AuditId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Groups_GroupId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Permissions_PermissionId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Audits_AuditId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_Groups_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Audits_AuditId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_UserApplications_Permission",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_Applications_ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_AuditId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Permission",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_Groups_AuditId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Applications_AuditId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_GroupId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_PermissionId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "AuditId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Permission",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "AuditId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AuditId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "UserApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationGroup",
                columns: table => new
                {
                    ApplicationsApplicationId = table.Column<int>(type: "int", nullable: false),
                    GroupsGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationGroup", x => new { x.ApplicationsApplicationId, x.GroupsGroupId });
                    table.ForeignKey(
                        name: "FK_ApplicationGroup_Applications_ApplicationsApplicationId",
                        column: x => x.ApplicationsApplicationId,
                        principalTable: "Applications",
                        principalColumn: "ApplicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationGroup_Groups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_PermissionId",
                table: "UserApplications",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationGroup_GroupsGroupId",
                table: "ApplicationGroup",
                column: "GroupsGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_Permissions_PermissionId",
                table: "UserApplications",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "PermissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_Permissions_PermissionId",
                table: "UserApplications");

            migrationBuilder.DropTable(
                name: "ApplicationGroup");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_PermissionId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "UserApplications");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "UserApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AuditId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Permission",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "GroupUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuditId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AuditId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AuditId",
                table: "Permissions",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Permission",
                table: "Permissions",
                column: "Permission");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AuditId",
                table: "Groups",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AuditId",
                table: "Applications",
                column: "AuditId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_GroupId",
                table: "Applications",
                column: "GroupId");

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
                name: "FK_Applications_Groups_GroupId",
                table: "Applications",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Permissions_PermissionId",
                table: "Applications",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Audits_AuditId",
                table: "Groups",
                column: "AuditId",
                principalTable: "Audits",
                principalColumn: "AuditId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_Groups_GroupId",
                table: "GroupUsers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Audits_AuditId",
                table: "Permissions",
                column: "AuditId",
                principalTable: "Audits",
                principalColumn: "AuditId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_UserApplications_Permission",
                table: "Permissions",
                column: "Permission",
                principalTable: "UserApplications",
                principalColumn: "UserApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_Applications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "ApplicationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
