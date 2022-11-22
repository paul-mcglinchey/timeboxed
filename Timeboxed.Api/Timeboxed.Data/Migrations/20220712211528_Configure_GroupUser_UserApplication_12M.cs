using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Configure_GroupUser_UserApplication_12M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserId",
                table: "UserApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_GroupUserId",
                table: "UserApplications",
                column: "GroupUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserId",
                table: "UserApplications",
                column: "GroupUserId",
                principalTable: "GroupUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_GroupUsers_GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_GroupUserId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "GroupUserId",
                table: "UserApplications");
        }
    }
}
