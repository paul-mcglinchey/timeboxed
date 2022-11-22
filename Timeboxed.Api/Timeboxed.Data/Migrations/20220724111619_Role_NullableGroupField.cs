using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Role_NullableGroupField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_GroupId",
                table: "Roles",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Groups_GroupId",
                table: "Roles",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Groups_GroupId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_GroupId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Roles");
        }
    }
}
