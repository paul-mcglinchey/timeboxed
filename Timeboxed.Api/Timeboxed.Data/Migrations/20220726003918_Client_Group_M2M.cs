using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Client_Group_M2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Clients_GroupId",
                table: "Clients",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Groups_GroupId",
                table: "Clients",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Groups_GroupId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_GroupId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Clients");
        }
    }
}
