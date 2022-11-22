using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Update_RotaEmployees_FKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "EmployeePreferences",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePreferences_EmployeeId",
                table: "EmployeePreferences",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePreferences_Employees_EmployeeId",
                table: "EmployeePreferences",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePreferences_Employees_EmployeeId",
                table: "EmployeePreferences");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePreferences_EmployeeId",
                table: "EmployeePreferences");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeePreferences");

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
    }
}
