using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class AddEmployeePreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PreferencesId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PreferencesId",
                table: "Employees",
                column: "PreferencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeePreferences_PreferencesId",
                table: "Employees",
                column: "PreferencesId",
                principalTable: "EmployeePreferences",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeePreferences_PreferencesId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PreferencesId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PreferencesId",
                table: "Employees");
        }
    }
}
