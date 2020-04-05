using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectHealthReport.Domains.Migrations
{
    public partial class RemoveYearAndWeekFieldInAllTableInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Week",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "QualityReports");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "QualityReports");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "BacklogItems");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "BacklogItems");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "AdditionalInfos");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AdditionalInfos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "Statuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Statuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "QualityReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "QualityReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "BacklogItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "BacklogItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "AdditionalInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AdditionalInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
