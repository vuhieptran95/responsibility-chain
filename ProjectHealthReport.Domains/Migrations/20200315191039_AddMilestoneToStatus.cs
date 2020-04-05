using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class AddMilestoneToStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Milestone",
                table: "Statuses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MilestoneDate",
                table: "Statuses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Milestone",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "MilestoneDate",
                table: "Statuses");
        }
    }
}
