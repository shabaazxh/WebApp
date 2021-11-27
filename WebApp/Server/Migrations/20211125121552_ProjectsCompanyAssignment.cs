using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Server.Migrations
{
    public partial class ProjectsCompanyAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CompanyId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Projects",
                newName: "assignedCompanyForProjectCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects",
                newName: "IX_Projects_assignedCompanyForProjectCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_assignedCompanyForProjectCompanyId",
                table: "Projects",
                column: "assignedCompanyForProjectCompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_assignedCompanyForProjectCompanyId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "assignedCompanyForProjectCompanyId",
                table: "Projects",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_assignedCompanyForProjectCompanyId",
                table: "Projects",
                newName: "IX_Projects_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CompanyId",
                table: "Projects",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
