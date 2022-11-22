using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class GroupUser_Applications_M21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserApplicationId",
                table: "GroupUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupUserApplicationId",
                table: "GroupUsers",
                column: "GroupUserApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupUsers_GroupUserApplications_GroupUserApplicationId",
                table: "GroupUsers",
                column: "GroupUserApplicationId",
                principalTable: "GroupUserApplications",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupUsers_GroupUserApplications_GroupUserApplicationId",
                table: "GroupUsers");

            migrationBuilder.DropIndex(
                name: "IX_GroupUsers_GroupUserApplicationId",
                table: "GroupUsers");

            migrationBuilder.DropColumn(
                name: "GroupUserApplicationId",
                table: "GroupUsers");
        }
    }
}
