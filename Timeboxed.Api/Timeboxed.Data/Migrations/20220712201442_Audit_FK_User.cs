using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Audit_FK_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audits_Users_User",
                table: "Audits");

            migrationBuilder.DropIndex(
                name: "IX_Audits_User",
                table: "Audits");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Audits",
                newName: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Audits",
                newName: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_User",
                table: "Audits",
                column: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_Audits_Users_User",
                table: "Audits",
                column: "User",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
