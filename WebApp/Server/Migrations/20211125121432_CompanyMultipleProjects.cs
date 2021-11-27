using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Server.Migrations
{
    public partial class CompanyMultipleProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Projects_WorkingOnProjectProjectId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_WorkingOnProjectProjectId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "WorkingOnProjectProjectId",
                table: "Companies");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CompanyId",
                table: "Projects",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CompanyId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CompanyId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkingOnProjectProjectId",
                table: "Companies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_WorkingOnProjectProjectId",
                table: "Companies",
                column: "WorkingOnProjectProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Projects_WorkingOnProjectProjectId",
                table: "Companies",
                column: "WorkingOnProjectProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
