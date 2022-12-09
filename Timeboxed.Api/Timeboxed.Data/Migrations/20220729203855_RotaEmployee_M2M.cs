using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class RotaEmployee_M2M : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ReportsToId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Rotas_RotaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ReportsToId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RotaId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "RotaId",
                table: "Employees",
                newName: "ReportsTo");

            migrationBuilder.RenameColumn(
                name: "ReportsToId",
                table: "Employees",
                newName: "LinkedUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Groups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RotaEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RotaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RotaEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RotaEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RotaEmployees_Rotas_RotaId",
                        column: x => x.RotaId,
                        principalTable: "Rotas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RotaEmployees_EmployeeId",
                table: "RotaEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RotaEmployees_RotaId",
                table: "RotaEmployees",
                column: "RotaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RotaEmployees");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "ReportsTo",
                table: "Employees",
                newName: "RotaId");

            migrationBuilder.RenameColumn(
                name: "LinkedUser",
                table: "Employees",
                newName: "ReportsToId");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReportsToId",
                table: "Employees",
                column: "ReportsToId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RotaId",
                table: "Employees",
                column: "RotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ReportsToId",
                table: "Employees",
                column: "ReportsToId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Rotas_RotaId",
                table: "Employees",
                column: "RotaId",
                principalTable: "Rotas",
                principalColumn: "Id");
        }
    }
}
