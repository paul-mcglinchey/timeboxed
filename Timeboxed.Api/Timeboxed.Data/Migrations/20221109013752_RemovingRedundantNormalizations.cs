using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class RemovingRedundantNormalizations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Addresses_AddressId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_ContactInfos_ContactInfoId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Names_NameId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_ContactInfos_ContactInfoId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePreferences_Employees_EmployeeId",
                table: "EmployeePreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Addresses_AddressId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ContactInfos_ContactInfoId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Names_NameId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_ContactInfos_ContactInfoId",
                table: "PhoneNumbers");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Names");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AddressId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ContactInfoId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_NameId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePreferences_EmployeeId",
                table: "EmployeePreferences");

            migrationBuilder.DropIndex(
                name: "IX_Clients_AddressId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ContactInfoId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_NameId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "ContactInfoId",
                table: "PhoneNumbers",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_ContactInfoId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_EmployeeId");

            migrationBuilder.RenameColumn(
                name: "ReportsTo",
                table: "Employees",
                newName: "ReportsToId");

            migrationBuilder.RenameColumn(
                name: "LinkedUser",
                table: "Employees",
                newName: "LinkedUserId");

            migrationBuilder.RenameColumn(
                name: "ContactInfoId",
                table: "Emails",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_ContactInfoId",
                table: "Emails",
                newName: "IX_Emails_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "PhoneNumbers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "EmployeeUnavailableDays",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstLine",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxHours",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNames",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinHours",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryEmailAddress",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhoneNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondLine",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdLine",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Emails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstLine",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MiddleNames",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryEmailAddress",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhoneNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondLine",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ThirdLine",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_ClientId",
                table: "PhoneNumbers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeUnavailableDays_EmployeeId",
                table: "EmployeeUnavailableDays",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LinkedUserId",
                table: "Employees",
                column: "LinkedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReportsToId",
                table: "Employees",
                column: "ReportsToId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_ClientId",
                table: "Emails",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Clients_ClientId",
                table: "Emails",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Employees_EmployeeId",
                table: "Emails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ReportsToId",
                table: "Employees",
                column: "ReportsToId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_LinkedUserId",
                table: "Employees",
                column: "LinkedUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeUnavailableDays_Employees_EmployeeId",
                table: "EmployeeUnavailableDays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Clients_ClientId",
                table: "PhoneNumbers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Employees_EmployeeId",
                table: "PhoneNumbers",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Clients_ClientId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Employees_EmployeeId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ReportsToId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_LinkedUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeUnavailableDays_Employees_EmployeeId",
                table: "EmployeeUnavailableDays");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Clients_ClientId",
                table: "PhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Employees_EmployeeId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_ClientId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeUnavailableDays_EmployeeId",
                table: "EmployeeUnavailableDays");

            migrationBuilder.DropIndex(
                name: "IX_Employees_LinkedUserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ReportsToId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Emails_ClientId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeeUnavailableDays");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FirstLine",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MaxHours",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MiddleNames",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MinHours",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PrimaryEmailAddress",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SecondLine",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ThirdLine",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FirstLine",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "MiddleNames",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PrimaryEmailAddress",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneNumber",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SecondLine",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ThirdLine",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "PhoneNumbers",
                newName: "ContactInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumbers_EmployeeId",
                table: "PhoneNumbers",
                newName: "IX_PhoneNumbers_ContactInfoId");

            migrationBuilder.RenameColumn(
                name: "ReportsToId",
                table: "Employees",
                newName: "ReportsTo");

            migrationBuilder.RenameColumn(
                name: "LinkedUserId",
                table: "Employees",
                newName: "LinkedUser");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Emails",
                newName: "ContactInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_EmployeeId",
                table: "Emails",
                newName: "IX_Emails_ContactInfoId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Names",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleNames = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Names", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressId",
                table: "Employees",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ContactInfoId",
                table: "Employees",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NameId",
                table: "Employees",
                column: "NameId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePreferences_EmployeeId",
                table: "EmployeePreferences",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AddressId",
                table: "Clients",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ContactInfoId",
                table: "Clients",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_NameId",
                table: "Clients",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Addresses_AddressId",
                table: "Clients",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_ContactInfos_ContactInfoId",
                table: "Clients",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Names_NameId",
                table: "Clients",
                column: "NameId",
                principalTable: "Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_ContactInfos_ContactInfoId",
                table: "Emails",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePreferences_Employees_EmployeeId",
                table: "EmployeePreferences",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Addresses_AddressId",
                table: "Employees",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ContactInfos_ContactInfoId",
                table: "Employees",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Names_NameId",
                table: "Employees",
                column: "NameId",
                principalTable: "Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_ContactInfos_ContactInfoId",
                table: "PhoneNumbers",
                column: "ContactInfoId",
                principalTable: "ContactInfos",
                principalColumn: "Id");
        }
    }
}
