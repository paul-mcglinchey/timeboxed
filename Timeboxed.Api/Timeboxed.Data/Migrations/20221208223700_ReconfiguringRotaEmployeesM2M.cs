using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class ReconfiguringRotaEmployeesM2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RotaEmployees",
                table: "RotaEmployees");

            migrationBuilder.DropIndex(
                name: "IX_RotaEmployees_RotaId",
                table: "RotaEmployees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RotaEmployees",
                table: "RotaEmployees",
                columns: new[] { "RotaId", "EmployeeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RotaEmployees",
                table: "RotaEmployees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RotaEmployees",
                table: "RotaEmployees",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RotaEmployees_RotaId",
                table: "RotaEmployees",
                column: "RotaId");
        }
    }
}
