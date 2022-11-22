using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Removing_GroupUserApplicationTableLogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUserApplicationRoles");

            migrationBuilder.DropTable(
                name: "GroupUserApplications");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_GroupUserId",
                table: "Applications",
                column: "GroupUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_GroupUsers_GroupUserId",
                table: "Applications",
                column: "GroupUserId",
                principalTable: "GroupUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_GroupUsers_GroupUserId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_GroupUserId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "GroupUserId",
                table: "Applications");

            migrationBuilder.CreateTable(
                name: "GroupUserApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    GroupUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupUserApplications_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserApplications_GroupUsers_GroupUserId",
                        column: x => x.GroupUserId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUserApplicationRoles",
                columns: table => new
                {
                    GroupUserApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserApplicationRoles", x => new { x.GroupUserApplicationId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_GroupUserApplicationRoles_GroupUserApplications_GroupUserApplicationId",
                        column: x => x.GroupUserApplicationId,
                        principalTable: "GroupUserApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserApplicationRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserApplicationRoles_RoleId",
                table: "GroupUserApplicationRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserApplications_ApplicationId",
                table: "GroupUserApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserApplications_GroupUserId",
                table: "GroupUserApplications",
                column: "GroupUserId");
        }
    }
}
