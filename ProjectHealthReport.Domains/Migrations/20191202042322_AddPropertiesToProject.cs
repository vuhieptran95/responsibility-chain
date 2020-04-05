using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddPropertiesToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryResponsibleName",
                table: "Projects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Division",
                table: "Projects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JIRALink",
                table: "Projects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KeyAccountManager",
                table: "Projects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "NextMileStoneDate",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProjectEndDate",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProjectStartDate",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SourceCodeLink",
                table: "Projects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SprintEndDate",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SprintStartDate",
                table: "Projects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryResponsibleName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Division",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "JIRALink",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "KeyAccountManager",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "NextMileStoneDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectEndDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectStartDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SourceCodeLink",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SprintEndDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SprintStartDate",
                table: "Projects");
        }
    }
}
