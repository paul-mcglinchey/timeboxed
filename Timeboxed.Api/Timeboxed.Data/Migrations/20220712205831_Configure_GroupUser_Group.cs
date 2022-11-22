using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Configure_GroupUser_Group : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserId",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupUsers_GroupUserId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_GroupUserId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "GroupUserId",
                table: "Groups");
        }
    }
}
