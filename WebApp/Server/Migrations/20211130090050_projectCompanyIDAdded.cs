using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Server.Migrations
{
    public partial class projectCompanyIDAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_assignedCompanyForProjectCompanyId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_assignedCompanyForProjectCompanyId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "assignedCompanyForProjectCompanyId",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyID",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyID",
                table: "Projects",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CompanyID",
                table: "Projects",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CompanyID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CompanyID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "assignedCompanyForProjectCompanyId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_assignedCompanyForProjectCompanyId",
                table: "Projects",
                column: "assignedCompanyForProjectCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_assignedCompanyForProjectCompanyId",
                table: "Projects",
                column: "assignedCompanyForProjectCompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
