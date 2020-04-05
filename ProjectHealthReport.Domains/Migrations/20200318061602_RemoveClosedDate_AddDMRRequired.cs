using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveClosedDate_AddDMRRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosedDate",
                table: "Projects");

            migrationBuilder.AddColumn<bool>(
                name: "DmrRequired",
                table: "Projects",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DmrRequiredFrom",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DmrRequiredTo",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DmrRequired",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DmrRequiredFrom",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DmrRequiredTo",
                table: "Projects");

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedDate",
                table: "Projects",
                type: "datetime2",
                nullable: true);
        }
    }
}
