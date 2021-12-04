using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Server.Migrations
{
    public partial class projectCompanyIDAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CompanyID",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Projects",
                newName: "companyID");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CompanyID",
                table: "Projects",
                newName: "IX_Projects_companyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_companyID",
                table: "Projects",
                column: "companyID",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_companyID",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "companyID",
                table: "Projects",
                newName: "CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_companyID",
                table: "Projects",
                newName: "IX_Projects_CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CompanyID",
                table: "Projects",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
