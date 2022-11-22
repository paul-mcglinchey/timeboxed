using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Timeboxed.Data.Migrations
{
    public partial class Creating_M2M_GroupUserApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_GroupUsers_GroupUserId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_GroupUserId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "GroupUserId",
                table: "Applications");

            migrationBuilder.CreateTable(
                name: "GroupUserApplications",
                columns: table => new
                {
                    GroupUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUserApplications", x => new { x.ApplicationId, x.GroupUserId });
                    table.ForeignKey(
                        name: "FK_GroupUserApplications_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUserApplications_GroupUsers_GroupUserId",
                        column: x => x.GroupUserId,
                        principalTable: "GroupUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUserApplications_GroupUserId",
                table: "GroupUserApplications",
                column: "GroupUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUserApplications");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupUserId",
                table: "Applications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_GroupUserId",
                table: "Applications",
                column: "GroupUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_GroupUsers_GroupUserId",
                table: "Applications",
                column: "GroupUserId",
                principalTable: "GroupUsers",
                principalColumn: "Id");
        }
    }
}
